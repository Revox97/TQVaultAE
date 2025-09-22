namespace TQVaultAE.Models.EventArgs
{
	public class CellWidthChangedEventArgs(double newWidthHeight, double newHeaderWidthHeight)
	{
		public double NewWidthHeight { get; } = newWidthHeight;
		public double NewHeaderWidthHeight { get; } = newHeaderWidthHeight;
	}
}
