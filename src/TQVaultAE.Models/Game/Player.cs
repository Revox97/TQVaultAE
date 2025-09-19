namespace TQVaultAE.Models.Game
{
	public class Player
	{
		public int Id { get; set; }
		
		public string Name { get; set; } = string.Empty;

		public PlayerStatistics Statistics { get; set; }
	}
}
