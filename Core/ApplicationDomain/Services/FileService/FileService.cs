using AutoMapper;
using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Contracts.Providers.FileSystemProvider;
using CloudIn.Core.ApplicationDomain.Services.FileService.Interfaces;

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

    public Task<FileEntity> CreateFileAsync(ICreateFilePayload createFilePayload)
    {
        throw new NotImplementedException();
    }

    public Task<FileEntity> WriteFileContentAsync(ICreateFilePayload createFilePayload)
    {
        throw new NotImplementedException();
    }
}
