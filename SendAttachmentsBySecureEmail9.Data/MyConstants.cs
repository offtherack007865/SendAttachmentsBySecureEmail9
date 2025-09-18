namespace SendAttachmentsBySecureEmail9.Data
{
    public class MyConstants
    {
        public enum BodyType
        {
            PlainText,
            RTF,
            HTML
        };

        public const string AppSettingsFile = "appsettings.json";
        public const string ConnectionString = "ConnectionString";
        public const string ConfigOptionsBaseWebUrl = "ConfigOptionsBaseWebUrl";
    }
}
