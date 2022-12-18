using CloudIn.Core.Domain.Contracts.Repositories;
using CloudIn.Core.Domain.Entities;
using CloudIn.Core.WebApi.GraphQl.Schema.FilterTypes;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Queries;

[ExtendObjectType(typeof(BaseQuery))]
public class UserQuery
{
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering(typeof(UserFilterType))]
    public IQueryable<UserEntity> GetUser([Service] IUserRepository userRepository)
    {
        return userRepository.GetAll().AsQueryable();
    }

    [UseProjection]
    [UseFiltering(typeof(UserFilterType))]
    [UseSorting]
    public IQueryable<UserEntity> GetUsers([Service] IUserRepository userRepository)
    {
        return userRepository.GetAll().AsQueryable();
    }
}
