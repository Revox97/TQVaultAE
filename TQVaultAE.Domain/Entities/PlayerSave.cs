using System.Drawing;
using System.IO;
using TQVaultAE.Domain.Contracts.Services;

namespace TQVaultAE.Domain.Entities
{
	public class PlayerSave : IDisposable
	{
		public FileSystemWatcher PlayerSaveWatcher { get; set; }
		public FileSystemWatcher PlayerStashWatcher { get; set; }

		public readonly Dictionary<string, Color> Tags = [];

		public string Folder { get; set; }
		public readonly string Name; 
		public bool IsArchived { get; set; }
		public readonly bool IsCustom;
		public readonly string CustomMap;
		public readonly string CustomMapName;
		private readonly ITranslationService _translate;
		public PlayerInfo Info { get; set; }
		public readonly bool IsImmortalThrone;

		public PlayerSave(string folder, bool isImmortalThrone, bool isArchived, bool isCustom, string customMap, ITranslationService translate)
		{
			Folder = folder;
			// Copy the names over without the '_' and strip out the path information.
			Name = Path.GetFileName(folder).Substring(1);
			IsCustom = isCustom;
			CustomMap = customMap;
			CustomMapName = Path.GetFileName(customMap);
			_translate = translate;
			IsImmortalThrone = isImmortalThrone;
			IsArchived = isArchived;
		}

		public override string ToString()
		{
			return string.Join(string.Empty, new[] {
				Info is null ? Name : $"{Name}"
				, Info?.Class is null ? string.Empty : $", {_translate.TranslateXTag(Info.Class, true, true)}"
				, !string.IsNullOrWhiteSpace(Info?.Class) && Info?.CurrentLevel is not null ?  " -" : string.Empty
				, Info?.CurrentLevel is null ? string.Empty : $" {_translate.TranslateXTag("tagMenuImport05")} : {Info.CurrentLevel}"
				, IsImmortalThrone ? string.Empty : " (TQ)"
				//, IsCustom ? $", IsCustom" : string.Empty // CustomMap is not specificaly related to this character
			});
		}

		public void Dispose()
		{
			PlayerSaveWatcher?.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
