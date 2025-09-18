namespace TQVaultAE.Domain.Exceptions
{
	/// <summary>
	/// Raised when game path is not found
	/// </summary>
	public class GamePathNotFoundException : ApplicationException
	{
		public GamePathNotFoundException() : base() { }
		public GamePathNotFoundException(string message) : base(message) { }
		public GamePathNotFoundException(string message, Exception innerException) : base(message, innerException) { }
	}
}
