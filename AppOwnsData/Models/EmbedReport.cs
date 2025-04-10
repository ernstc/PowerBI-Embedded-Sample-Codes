// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// ----------------------------------------------------------------------------

namespace AppOwnsData.Models
{
    using System;

    public class EmbedReport
    {
        // Id of Power BI report to be embedded
        public Guid ReportId { get; set; }

        // Id of Power BI report to be embedded
        public Guid DatasetId { get; set; }

        // Name of the report
        public string ReportName { get; set; } = null!;

        // Embed URL for the Power BI report
        public string EmbedUrl { get; set; } = null!;
    }
}
