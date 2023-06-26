using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ELMS.Models;

public class Users
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string UserID { get; set; }
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Password { get; set; } = null!;
}