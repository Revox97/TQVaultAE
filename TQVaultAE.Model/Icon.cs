namespace TQVaultAE.Model
{
    public class Icon(string id, Uri uri)
    {
        public string Id { get; set; } = id;

        public Uri Uri { get; set; } = uri;
    }
}
