using TQVaultAE.Domain.Entities;

namespace TQVaultAE.Domain.Results
{
	public class LoadPlayerStashResult
	{
		public Stash? Stash { get; set; }
		public string? StashFile { get; set; }
	}
}
