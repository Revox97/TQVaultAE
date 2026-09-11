using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

// TODO split in multiple files and remove magic numbers
// TODO Have reading strategies for different version, I already feel the pain...

namespace TQVaultAE.FileFormats.Tex
{
    /// <summary>
    /// TEX texture file.
    ///
    /// Layout:
    ///   char[3]   "TEX"
    ///   byte      version
    ///   uint32    framesPerSecond
    ///   byte      hasAlpha
    ///   ImageFrame[]
    /// </summary>
    public sealed class TexFile
    {
        public const string FormatIdentifier = "TEX";

        public byte Version { get; set; }

        public uint FramesPerSecond { get; set; }

        public bool HasAlpha { get; set; }

        public List<ImageFrame> Frames { get; } = [];

        public static TexFile Load(string path)
        {
            using FileStream stream = File.OpenRead(path);
            return Read(stream);
        }

        public static TexFile Read(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);

            using BinaryReader reader = new(stream, Encoding.ASCII, leaveOpen: true);

            string tag = Encoding.ASCII.GetString(reader.ReadBytes(3));

            if (tag != FormatIdentifier)
                throw new InvalidDataException($"Invalid TEX identifier. Expected '{FormatIdentifier}', got '{tag}'.");

            byte version = reader.ReadByte();

            // TODO Other version might be supported
            // There seem to be some in version 1, maybe different handling is needed
            //if (version != 2)
            //    throw new InvalidDataException($"Unsupported TEX version: {version}");

            TexFile tex = new()
            {
                Version = version,
                FramesPerSecond = reader.ReadUInt32(),
                HasAlpha = reader.ReadByte() != 0
            };

            // There is no frame count. Read frames until EOF.
            while (stream.Position < stream.Length)
                tex.Frames.Add(ImageFrame.Read(reader));

            Debug.WriteLine("Read tex");
            return tex;
        }

        // TODO check for linux support
        [SupportedOSPlatform("windows")]
        public Bitmap GetToBitmap()
        {
            byte[] pixelData = Frames[0].MipMaps[0].Data;
            int width = Frames[0].DdsSurface.Width;
            int height = Frames[0].DdsSurface.Height;

            if (pixelData.Length < width * height * 4)
                throw new ArgumentException("Not enough pixel data.");

            Bitmap bitmap = new(width, height, PixelFormat.Format32bppArgb);

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            try
            {
                Marshal.Copy(pixelData, 0, data.Scan0, width * height * 4);
            }
            finally
            {
                bitmap.UnlockBits(data);
            }

            return bitmap;
        }
    }
}
