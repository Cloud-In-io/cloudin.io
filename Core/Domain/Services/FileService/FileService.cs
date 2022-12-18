using MimeTypes;
using AutoMapper;
using CloudIn.Core.Domain.Entities;
using CloudIn.Core.Domain.Contracts.Repositories;
using CloudIn.Core.Domain.Contracts.Providers.FileSystemProvider;
using CloudIn.Core.Domain.Services.FileService.Interfaces;

namespace CloudIn.Core.Domain.Services.FileService;

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

    public async Task<FileEntity> WriteFileAsync(IWriteFilePayload filePayload)
    {
        var ownerUser = await _userRepository.GetByIdAsync(filePayload.OwnerUserId);
        if (ownerUser == null)
        {
            throw new Exception("Owner user not found or not exists");
        }

        var parentFolder = await _folderRepository.GetByIdAsync(filePayload.ParentFolderId);
        if (parentFolder == null)
        {
            throw new Exception("Parent folder not found or not exists");
        }

        var file = _mapper.Map<FileEntity>(filePayload);

        if (file == null)
        {
            throw new Exception("File registry not found or not exists");
        }

        var fileExtension = MimeTypeMap.GetExtension(filePayload.MimeType);

        IWriteFileValues writeFileValues =
            new()
            {
                Extension = fileExtension,
                FileName = file.Id.ToString(),
                FilePath = ownerUser.Id.ToString(),
                Content = filePayload.Content,
            };

        var virtualPath = await _fileSystemProvider.WriteAsync(writeFileValues);

        file.FilePath = virtualPath;
        await _fileRepository.AddAsync(file);

        file.MoveToFolder(parentFolder);
        parentFolder.AddFile(file);

        var wasFileSaved = await _fileRepository.SaveChangesAsync();

        if (!wasFileSaved)
        {
            throw new Exception("Could not save this file");
        }

        return file;
    }
}
