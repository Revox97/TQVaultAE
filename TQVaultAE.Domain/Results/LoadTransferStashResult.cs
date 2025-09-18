using TQVaultAE.Domain.Entities;

namespace TQVaultAE.Domain.Results
{
	public class LoadTransferStashResult
	{
		public string? TransferStashFile { get; set; }
		public Stash? Stash { get; set; }
	}
}
