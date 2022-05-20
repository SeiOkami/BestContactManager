namespace Contacts.WebClient.Services
{
  public class IdentityServerSettings
  {
    public string DiscoveryUrl { get; set; } = String.Empty;
    public string ClientName { get; set; } = String.Empty;
    public string ClientPassword { get; set; } = String.Empty;
    public bool UseHttps { get; set; }
    public bool AlwaysIncludeUserClaimsInIdToken { get ; set; }
  }
}