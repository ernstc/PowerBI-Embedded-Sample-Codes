namespace AppOwnsData.Models
{
    public class PowerBI
    {
        // Workspace Id for which Embed token needs to be generated
        public string WorkspaceId { get; set; } = null!;

        // Report Id for which Embed token needs to be generated
        public string ReportId { get; set; } = null!;

        // Dataset Id for which Embed token needs to be generated
        public string? DatasetId { get; set; }
    }
}
