using System.Buffers;
using System.IO.Compression;

namespace TQVaultAE.TitanQuestDataProviders
{
    internal static class Compression
    {
        private const int DefaultOutputSize = 8192;

        internal static byte[] DecompressZlib(ReadOnlySpan<byte> data)
        {
            if (data.Length < 2)
                return [];

            // Check for zlib header (CMF + FLG bytes)
            // 0x78 0x01, 0x78 0x9C, or 0x78 0xDA are valid zlib headers // TODO: Yay magic numbers again, look them up
            bool skipZlibHeader = data.Length > 6 && data[0] == 0x78 && (data[1] == 0x01 || data[1] == 0x9C || data[1] == 0xDA);
            ReadOnlySpan<byte> compressedData = skipZlibHeader ? data[2..] : data;

            try
            {
                // Use pooled buffer for output to reduce GC pressure
                byte[] outputBuffer = ArrayPool<byte>.Shared.Rent(DefaultOutputSize);
                int totalWritten = 0;
                int bufferLength = outputBuffer.Length;

                try
                {
                    using MemoryStream compressedStream = new(compressedData.ToArray());

                    // Use DeflateStream for decompression
                    // DeflateStream expects raw deflate format (zlib header should be stripped)
                    using (DeflateStream deflate = new(compressedStream, CompressionMode.Decompress))
                    {
                        int bytesRead;

                        while ((bytesRead = deflate.Read(outputBuffer, totalWritten, bufferLength - totalWritten)) > 0)
                        {
                            totalWritten += bytesRead;

                            // Expand buffer if needed
                            if (totalWritten >= bufferLength)
                            {
                                byte[] newBuffer = ArrayPool<byte>.Shared.Rent(bufferLength * 2);
                                Array.Copy(outputBuffer, newBuffer, bufferLength);
                                ArrayPool<byte>.Shared.Return(outputBuffer, clearArray: true);
                                outputBuffer = newBuffer;
                                bufferLength = newBuffer.Length;
                            }
                        }
                    }

                    byte[] result = new byte[totalWritten];
                    Array.Copy(outputBuffer, result, totalWritten);
                    return result;
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(outputBuffer, clearArray: true);
                }
            }
            catch (Exception ex)
            {
                // TODO log exception
                return [];
            }
        }
    }
}
