using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Courses.Models
{
    public class Courses
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string CourseID { get; set; } = null!;
        public string CourseName { get; set; } = null!;

        public string CourseTitle { get; set; } = null!;

        public string CourseText { get; set; } = null!;

        public string Instructor { get; set; } = null!;
    }
}
