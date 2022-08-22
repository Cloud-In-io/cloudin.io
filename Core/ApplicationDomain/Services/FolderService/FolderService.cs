using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Services.FolderService.Interfaces;

namespace CloudIn.Core.ApplicationDomain.Services.FolderService;

public class FolderService : IFolderService
{
    private readonly IMapper _mapper;

    private readonly IFolderRepository _folderRepository;
    private readonly IUserRepository _userRepository;

    public FolderService(
        IMapper mapper,
        IFolderRepository folderRepository,
        IUserRepository userRepository
    )
    {
        _mapper = mapper;
        _folderRepository = folderRepository;
        _userRepository = userRepository;
    }

    public async Task<FolderEntity> CreateFolderAsync(ICreateFolderPayload createFolderPayload)
    {
        var ctx = new ValidationContext(createFolderPayload);
        Validator.ValidateObject(
            instance: createFolderPayload,
            validationContext: ctx,
            validateAllProperties: true
        );

        var ownerUser = await _userRepository.GetByIdAsync(createFolderPayload.OwnerUserId);
        if (ownerUser == null)
        {
            throw new Exception("Owner user not found or not exists");
        }

        var parentFolder = await _folderRepository.GetByIdAsync(createFolderPayload.ParentFolderId);
        if (parentFolder == null)
        {
            throw new Exception("Parent folder not found or not exists");
        }

        var folder = _mapper.Map<FolderEntity>(createFolderPayload);

        folder.SetAsChildFolder(parentFolder, ownerUser);

        await _folderRepository.AddAsync(folder);

        var wasFolderSaved = await _folderRepository.SaveChangesAsync();

        if (!wasFolderSaved)
        {
            throw new Exception("Could not save this folder");
        }

        return folder;
    }
}
