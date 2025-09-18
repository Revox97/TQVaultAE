using TQVaultAE.Domain.Entities;

namespace TQVaultAE.Domain.Results
{
	public class LoadRelicVaultStashResult
	{
		public string? RelicVaultStashFile { get; set; }
		public Stash? Stash { get; set; }
	}
}
