using JwtAuthenticationManager.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Authentication.Services;

public class UserService
{
    private readonly IMongoCollection<Users> _UserCollection;

    public UserService()
    {
        var mongoClient = new MongoClient("mongodb://localhost:27017");

        var mongoDatabase = mongoClient.GetDatabase("ELMS");

        _UserCollection = mongoDatabase.GetCollection<Users>("Users");
    }
            
        public async Task<Users?> AuthenticateAsync(string id , string pass) =>
            await _UserCollection.Find(x =>  x.UserID == id && x.Password == pass ).FirstOrDefaultAsync();

       
}