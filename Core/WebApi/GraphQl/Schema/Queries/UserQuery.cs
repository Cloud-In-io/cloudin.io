using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Queries;

[ExtendObjectType(typeof(BaseQuery))]
public class UserQuery
{
    [UseProjection]
    public IEnumerable<UserEntity> GetUsers([Service] IUserRepository userRepository)
    {
        return userRepository.GetAll().AsQueryable();
    }
}
