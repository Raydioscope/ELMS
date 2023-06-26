namespace ELMS.Models
{
    public class ELMSDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string EnrollmentsCollectionName { get; set; } = null!;
    }
}
