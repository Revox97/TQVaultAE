namespace TQVaultAE.Domain.Results
{
	public class GamePathEntry(string path, string displayName)
	{
		public readonly string Path = path;
		public readonly string DisplayName = displayName;

		public override string ToString() => DisplayName ?? Path ?? "Empty";
	}
}
