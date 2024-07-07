using Users.Demo.API.EndpointHandlers;

namespace Users.Demo.API.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void RegisterUsersEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var userEndpoints = endpointRouteBuilder.MapGroup("/users").WithOpenApi();
        userEndpoints.MapGet("", UsersHandlers.ListUsersAsync);
       // userEndpoints.MapPost("", UsersHandlers.AddUserAsync).Accepts<UserInfoDTO>("application/json");
        //userEndpoints.MapPut("/{userId}", UsersHandlers.UpdateUserAsync).Accepts<UserInfoDTO>("application/json");
        //userEndpoints.MapDelete("/{userId}", UsersHandlers.DeleteUserAsync);

    }
}