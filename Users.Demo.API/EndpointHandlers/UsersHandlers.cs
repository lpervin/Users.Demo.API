using Microsoft.AspNetCore.Http.HttpResults;
using Users.Application.DTOs;
using Users.Application.Repositories;
using Users.Demo.API.Models.Requests;

namespace Users.Demo.API.EndpointHandlers;

public static class UsersHandlers
{
    public static async Task<Results<BadRequest, Ok<PagedResults<UserDto>>>> ListUsersAsync(PagingRequest paging, IUserRepository userRepository)
    {
        if (paging.CurrentPageNumber == 0)
            return TypedResults.BadRequest();
        
        var users = await userRepository.ListUsersAsync(paging.CurrentPageNumber, paging.PageSize, paging.OrderBy.OrderByFieldName);
        return TypedResults.Ok(users);
    }
}