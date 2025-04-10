namespace AppOwnsData.Models
{
    public class AzureAd
    {
        // Can be set to 'MasterUser' or 'ServicePrincipal'
        public string AuthenticationMode { get; set; } = null!;

        // URL used for initiating authorization request
        public string AuthorityUrl { get; set; } = null!;

        // Client Id (Application Id) of the AAD app
        public string? ClientId { get; set; }

        // Id of the Azure tenant in which AAD app is hosted. Required only for Service Principal authentication mode.
        public string TenantId { get; set; } = null!;

        // ScopeBase of AAD app. Use the below configuration to use all the permissions provided in the AAD app through Azure portal.
        public string[] ScopeBase { get; set; } = null!;

        // Master user email address. Required only for MasterUser authentication mode.
        public string? PbiUsername { get; set; }

        // Master user email password. Required only for MasterUser authentication mode.
        public string? PbiPassword { get; set; }

        // Client Secret (App Secret) of the AAD app. Required only for ServicePrincipal authentication mode.
        public string? ClientSecret { get; set; }
    }
}
