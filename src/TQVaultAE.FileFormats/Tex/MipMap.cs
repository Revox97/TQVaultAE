namespace TQVaultAE.FileFormats.Tex
{
    public sealed class MipMap
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public byte[] Data { get; set; } = [];
    }
}
