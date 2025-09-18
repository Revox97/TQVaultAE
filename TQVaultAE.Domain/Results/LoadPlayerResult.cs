using TQVaultAE.Domain.Entities;

namespace TQVaultAE.Domain.Results
{
	public class LoadPlayerResult
	{
		public string? PlayerFile { get; set; }
		public PlayerCollection? Player { get; set; }
	}
}
