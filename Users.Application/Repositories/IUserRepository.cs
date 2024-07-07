using Users.Application.DTOs;
using Users.Core.Entities;

namespace Users.Application.Repositories;

public interface IUserRepository
{
 
    Task<PagedResults<UserDto>> ListUsersAsync(int pageNumber, int pageSize, string sortField);
}