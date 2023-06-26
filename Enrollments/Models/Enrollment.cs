using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Enrollments.Models
{
    public class Enrollment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string EnrollmentID { get; set; } = null!;

        public string CourseID { get; set; } = null!;

        public string Learner { get; set; } = null!;

        public string CompletionPercentage { get; set; } = null!;

        public string TargetDate { get; set; } = null!;
    }
}
