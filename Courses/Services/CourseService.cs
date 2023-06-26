using Courses.Models;
using ELMS.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Courses.Services
{
    public class CourseService
    {
        private readonly IMongoCollection<Courses.Models.Courses> _CourseCollection;
        public CourseService(IOptions<ELMSDatabaseSettings> CoursesDatabaseSettings) {
            var mongoClient = new MongoClient(
           CoursesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                CoursesDatabaseSettings.Value.DatabaseName);

            _CourseCollection = mongoDatabase.GetCollection<Courses.Models.Courses>(
                CoursesDatabaseSettings.Value.CourseCollectionName);
        }
        public async Task<List<Courses.Models.Courses>> GetAsync() =>
        await _CourseCollection.Find(_ => true).ToListAsync();

        public async Task<Courses.Models.Courses?> GetAsync(string id) =>
            await _CourseCollection.Find(x => x.CourseID == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Courses.Models.Courses newCourse) =>
            await _CourseCollection.InsertOneAsync(newCourse);

        public async Task UpdateAsync(string id, Courses.Models.Courses updatedUser) =>
            await _CourseCollection.ReplaceOneAsync(x => x.CourseID == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _CourseCollection.DeleteOneAsync(x => x.CourseID == id);

        public async Task<List<Courses.Models.Courses>> GetCourseByInstructorAsync(string inst) =>
            await _CourseCollection.Find(x => x.Instructor == inst).ToListAsync();
    }
}
