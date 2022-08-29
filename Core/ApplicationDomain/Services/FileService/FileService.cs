using AutoMapper;
using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Contracts.Providers.FileSystemProvider;
using CloudIn.Core.ApplicationDomain.Services.FileService.Interfaces;
using System.ComponentModel.DataAnnotations;
using MimeTypes;

namespace CloudIn.Core.ApplicationDomain.Services.FileService;

public class FileService : IFileService
{
    private readonly IMapper _mapper;
    private readonly IFileSystemProvider _fileSystemProvider;
    private readonly IFileRepository _fileRepository;
    private readonly IFolderRepository _folderRepository;
    private readonly IUserRepository _userRepository;

    public FileService(
        IMapper mapper,
        IFileSystemProvider fileSystemProvider,
        IFileRepository fileRepository,
        IFolderRepository folderRepository,
        IUserRepository userRepository
    )
    {
        _mapper = mapper;
        _fileSystemProvider = fileSystemProvider;
        _fileRepository = fileRepository;
        _folderRepository = folderRepository;
        _userRepository = userRepository;
    }

    public async Task<FileEntity> CreateFileAsync(ICreateFilePayload createFilePayload)
    {
        var ctx = new ValidationContext(createFilePayload);
        Validator.ValidateObject(
            instance: createFilePayload,
            validationContext: ctx,
            validateAllProperties: true
        );

        var ownerUser = await _userRepository.GetByIdAsync(createFilePayload.OwnerUserId);
        if (ownerUser == null)
        {
            throw new Exception("Owner user not found or not exists");
        }

        var parentFolder = await _folderRepository.GetByIdAsync(createFilePayload.ParentFolderId);
        if (parentFolder == null)
        {
            throw new Exception("Parent folder not found or not exists");
        }

        var file = _mapper.Map<FileEntity>(createFilePayload);

        file.MoveToFolder(parentFolder);
        parentFolder.AddFile(file);

        await _fileRepository.AddAsync(file);

        var wasFileSaved = await _fileRepository.SaveChangesAsync();

        if (!wasFileSaved)
        {
            throw new Exception("Could not save this file");
        }

        return file;
    }

    public async Task<FileEntity> WriteFileContentAsync(IWriteFileContentPayload fileContentPayload)
    {
        var ownerUser = await _userRepository.GetByIdAsync(fileContentPayload.OwnerUserId);
        if (ownerUser == null)
        {
            throw new Exception("Owner user not found or not exists");
        }

        var file = await _fileRepository.GetByIdAsync(fileContentPayload.FileId);

        if (file == null)
        {
            throw new Exception("File registry not found or not exists");
        }

        var fileExtension = MimeTypeMap.GetExtension(fileContentPayload.MimeType);

        IWriteFileValues writeFileValues =
            new()
            {
                Extension = fileExtension,
                FileName = file.Id.ToString(),
                FilePath = ownerUser.Id.ToString(),
                Content = fileContentPayload.Content,
            };

        var physicalPath = await _fileSystemProvider.WriteAsync(writeFileValues);

        file.PhysicalPath = physicalPath;
        file.MimeType = fileContentPayload.MimeType;

        var wasFileSaved = await _fileRepository.SaveChangesAsync();

        if (!wasFileSaved)
        {
            throw new Exception("Could not save this file");
        }

        return file;
    }
}
