using TQVaultAE.Domain.Entities;

namespace TQVaultAE.Domain.Results
{
	public class LoadVaultResult
	{
		public PlayerCollection? Vault { get; set; }
		public string? Filename { get; set; }
		public bool? VaultLoaded { get; set; }
		public ArgumentException? ArgumentException { get; set; }
	}
}
