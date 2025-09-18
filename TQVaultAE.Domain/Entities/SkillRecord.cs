using System.Text;

namespace TQVaultAE.Domain.Entities
{
	public class SkillRecord
	{
		private const string BeginBlock = "begin_block";
		private const string EndBlock = "end_block";

		internal static readonly Encoding Encoding1252 = Encoding.GetEncoding(1252);

		public string SkillName { get; set; }

		public int SkillLevel { get; set; }

		public int SkillEnabled { get; set; }

		public int SkillSubLevel { get; set; }

		public int SkillActive { get; set; }

		public int SkillTransition { get; set; }

		/// <summary>
		/// Binary serialize
		/// </summary>
		/// <returns></returns>
		public byte[] ToBinary(int beginBlockValue, int endBlockValue)
		{
			return [.. new[] {

				BitConverter.GetBytes(BeginBlock.Length),
				Encoding1252.GetBytes(BeginBlock),
				BitConverter.GetBytes(beginBlockValue),

				BitConverter.GetBytes(nameof(SkillName).Length),
				Encoding1252.GetBytes(nameof(SkillName)),
				BitConverter.GetBytes(SkillName.Length),
				Encoding1252.GetBytes(SkillName),

				BitConverter.GetBytes(nameof(SkillLevel).Length),
				Encoding1252.GetBytes(nameof(SkillLevel)),
				BitConverter.GetBytes(SkillLevel),

				BitConverter.GetBytes(nameof(SkillEnabled).Length),
				Encoding1252.GetBytes(nameof(SkillEnabled)),
				BitConverter.GetBytes(SkillEnabled),

				BitConverter.GetBytes(nameof(SkillSubLevel).Length),
				Encoding1252.GetBytes(nameof(SkillSubLevel)),
				BitConverter.GetBytes(SkillSubLevel),

				BitConverter.GetBytes(nameof(SkillActive).Length),
				Encoding1252.GetBytes(nameof(SkillActive)),
				BitConverter.GetBytes(SkillActive),

				BitConverter.GetBytes(nameof(SkillTransition).Length),
				Encoding1252.GetBytes(nameof(SkillTransition)),
				BitConverter.GetBytes(SkillTransition),

				BitConverter.GetBytes(EndBlock.Length),
				Encoding1252.GetBytes(EndBlock),
				BitConverter.GetBytes(endBlockValue),

			}.SelectMany(arr => arr)];
		}
	}
}
