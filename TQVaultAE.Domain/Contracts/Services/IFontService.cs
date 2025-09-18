namespace TQVaultAE.Domain.Contracts.Services
{
	public interface IFontService
	{
		IAddFontToOS FontLoader { get; }

		// TODO add back in later
		//Font GetFont(float fontSize, float? scale = null);
		//Font GetFont(float fontSize, FontStyle fontStyle, float? scale = null);
		//Font GetFont(float fontSize, FontStyle fontStyle, GraphicsUnit unit);
		//Font GetFont(float fontSize, FontStyle fontStyle, GraphicsUnit unit, byte b);
		//Font GetFont(float fontSize, GraphicsUnit unit);
		//Font GetFontLight(float fontSize);
		//Font GetFontLight(float fontSize, float? scale = null);
		//Font GetFontLight(float fontSize, FontStyle fontStyle, float? scale = null);
		//Font GetFontLight(float fontSize, FontStyle fontStyle, GraphicsUnit unit);
		//Font GetFontLight(float fontSize, FontStyle fontStyle, GraphicsUnit unit, byte b);
		//Font GetFontLight(float fontSize, GraphicsUnit unit);
	}
}