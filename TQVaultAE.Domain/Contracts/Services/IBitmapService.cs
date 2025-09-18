using System.Windows.Media.Imaging;

namespace TQVaultAE.Domain.Contracts.Services
{

	/// <summary>
	/// Loads Titan Quest textures and converts them into bitmaps.
	/// </summary>
	public interface IBitmapService
	{
		/// <summary>
		/// Loads a .tex from memory and converts to bitmap
		/// </summary>
		/// <param name="data">raw tex data array</param>
		/// <param name="offset">offset into the array</param>
		/// <param name="count">number of bytes</param>
		/// <returns>bitmap of tex file.</returns>

		// Maybe make this async for performance
		BitmapImage? LoadFromTexMemory(byte[] data, int offset, int count);
	}
}