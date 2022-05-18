namespace Contacts.WebClient.Services
{
    public class WebAPIServiceSettings
    {
        public string MainURL { get; set; } = String.Empty;

        public string ListMethodName { get; set; } = String.Empty;
        public string DetailsMethodName { get; set; } = String.Empty;
        public string CreateMethodName { get; set; } = String.Empty;
        public string UpdateMethodName { get; set; } = String.Empty;
        public string DeleteMethodName { get; set; } = String.Empty;
        public string ClearMethodName { get; set; } = String.Empty;
        public string GenerateMethodName { get; set; } = String.Empty;
        public string ImportMethodName { get; set; } = String.Empty;

        public string ListMethodURL { get => MainURL + ListMethodName; }
        public string DetailsMethodURL { get => MainURL + DetailsMethodName; }
        public string CreateMethodURL { get => MainURL + CreateMethodName; }
        public string UpdateMethodURL { get => MainURL + UpdateMethodName; }
        public string DeleteMethodURL { get => MainURL + DeleteMethodName; }
        public string ClearMethodURL { get => MainURL + ClearMethodName; }
        public string GenerateMethodURL { get => MainURL + GenerateMethodName; }
        public string ImportMethodURL { get => MainURL + ImportMethodName; }

    }
}
