using ELMS.Models;
using Enrollments.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Enrollments.Services
{
    public class EnrollmentService
    {
        private readonly IMongoCollection<Enrollment> _EnrollmentCollection;

        public EnrollmentService(
        IOptions<ELMSDatabaseSettings> EnrollmentsDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                EnrollmentsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                EnrollmentsDatabaseSettings.Value.DatabaseName);

            _EnrollmentCollection = mongoDatabase.GetCollection<Enrollment>(
                EnrollmentsDatabaseSettings.Value.EnrollmentsCollectionName);
        }
        public async Task<List<Enrollment>> GetAsync() =>
        await _EnrollmentCollection.Find(_ => true).ToListAsync();

        public async Task<Enrollment?> GetAsync(string id) =>
            await _EnrollmentCollection.Find(x => x.EnrollmentID == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Enrollment newEnrollment) =>
            await _EnrollmentCollection.InsertOneAsync(newEnrollment);

        public async Task UpdateAsync(string id, Enrollment updatedEnrollment) =>
            await _EnrollmentCollection.ReplaceOneAsync(x => x.EnrollmentID == id, updatedEnrollment);

        public async Task RemoveAsync(string id) =>
            await _EnrollmentCollection.DeleteOneAsync(x => x.EnrollmentID == id);

        public async Task<List<Enrollment>> GetEnrollmentByLearnerAsync(string learner) =>
            await _EnrollmentCollection.Find(x => x.Learner == learner).ToListAsync();
    }
}
