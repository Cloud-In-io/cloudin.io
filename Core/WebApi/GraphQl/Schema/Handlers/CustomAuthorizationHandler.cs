using HotChocolate.Resolvers;
using HotChocolate.AspNetCore.Authorization;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Handlers;

public class CustomAuthorizationHandler : IAuthorizationHandler
{
    public async ValueTask<AuthorizeResult> AuthorizeAsync(IMiddlewareContext context, AuthorizeDirective directive)
    {
        var hotChocolateHandler = new DefaultAuthorizationHandler();

        if(directive.Roles?.Any(role => role == "annonymous") ?? false)
        {
            return AuthorizeResult.Allowed;
        }

        return await hotChocolateHandler.AuthorizeAsync(context, directive);
    }
}
