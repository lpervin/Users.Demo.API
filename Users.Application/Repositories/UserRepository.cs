using MongoDB.Bson;
using MongoDB.Driver;
using Users.Application.DTOs;
using Users.Core.Entities;

namespace Users.Application.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<User>("Users");
    }
    public async Task<PagedResults<UserDto>> ListUsersAsync(int pageNumber, int pageSize, string sortField)
    {
        var (users, totalRecordsCount, recordsOnCurrentPageCount, pagesCount) =  GetUsersByPage(_collection, pageNumber, pageSize, sortField);
        
        return new PagedResults<UserDto>()
        {
            Results = users.Select(u => new UserDto() { Id = u.Id, Email = u.Email, Name = u.Name, Age = u.Age}).ToList(),
            PagesCount = pagesCount,
            PageSize = pageSize,
            CurrentPageNumber = pageNumber,
            TotalRecordsCount = totalRecordsCount,
            CurrentPageRecordsCount = recordsOnCurrentPageCount
        };
    }
    private (List<User>, long, int, int) GetUsersByPage(IMongoCollection<User> collection, int pageNumber, int pageSize, string sortField)
    {
        if (pageNumber <= 0)
        {
            throw new ArgumentException("Page number must be greater than 0.");
        }

        var totalRecordsCount = collection.CountDocuments(new BsonDocument());
        var pagesCount = (int)Math.Ceiling((double)totalRecordsCount / pageSize);

        if (pageNumber > pagesCount)
        {
            throw new ArgumentException($"Page number {pageNumber} exceeds total number of pages ({pagesCount}).");
        }

        var skip = (pageNumber - 1) * pageSize;
        var sort = Builders<User>.Sort.Ascending(sortField);

        var users = collection.Find(new BsonDocument())
            .Sort(sort)
            .Skip(skip)
            .Limit(pageSize)
            .ToList();

        var recordsOnCurrentPageCount = users.Count;

        return (users, totalRecordsCount, recordsOnCurrentPageCount, pagesCount);
    }

}