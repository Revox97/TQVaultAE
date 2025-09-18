using System.Collections.ObjectModel;
using TQVaultAE.Domain.Helpers;

namespace TQVaultAE.Domain.Entities;

public partial class RecordId : IEquatable<RecordId>, IComparable, IComparable<RecordId>
{
	public readonly string Raw;
	public readonly string Normalized;

	private RecordId(string rawRecordId)
	{
		rawRecordId ??= string.Empty;
		rawRecordId = rawRecordId.Trim();

		Raw = rawRecordId;
		Normalized = rawRecordId;

		if (Raw != string.Empty)
			Normalized = rawRecordId.NormalizeRecordPath();
	}

	/// <summary>
	/// Factory
	/// </summary>
	/// <param name="rawRecordId"></param>
	/// <returns></returns>
	public static RecordId Create(string rawRecordId) => new RecordId(rawRecordId);

	public override string ToString() => Normalized;

	private GameDlc? _dlc;

	/// <summary>
	/// Resolve the Dlc that <see cref="RecordId"/> belongs to.
	/// </summary>
	public GameDlc Dlc
	{
		get
		{
			if (_dlc is null)
				_dlc = Normalized switch
				{
					var x when x.Contains(@"\XPACK4\") || this.IsHardCoreDungeonEE => GameDlc.EternalEmbers,
					var x when x.Contains(@"\XPACK3\") => GameDlc.Atlantis,
					var x when x.Contains(@"\XPACK2\") => GameDlc.Ragnarok,
					var x when x.Contains(@"\XPACK\") => GameDlc.ImmortalThrone,
					_ => GameDlc.TitanQuest
				};

			return _dlc.Value;
		}
	}

	private bool? _IsOld;

	/// <summary>
	/// This <see cref="RecordId"/> leads to old, obsolete content.
	/// </summary>
	public bool IsOld
	{
		get
		{
			_IsOld ??= Normalized.Contains(@"\OLD\");
			return _IsOld.Value;
		}
	}

	public static readonly RecordId Empty = Create(string.Empty);

	public bool IsEmpty => Raw == string.Empty;
	
	public static bool IsNullOrEmpty(RecordId Id) => Id is null || Id.IsEmpty;

	private string _prettyFileName;

	public string PrettyFileName
	{
		get
		{
			if (IsEmpty) 
				return string.Empty;

			_prettyFileName ??= Raw.PrettyFileName();
			return _prettyFileName;
		}
	}

	(string PrettyFileName, string Effect, string Number, bool IsMatch) _PrettyFileNameExploded = default!;
	public (string PrettyFileName, string Effect, string Number, bool IsMatch) PrettyFileNameExploded
	{
		get
		{
			if (!IsEmpty && _PrettyFileNameExploded.PrettyFileName is null)
				_PrettyFileNameExploded = PrettyFileName.ExplodePrettyFileName();

			return _PrettyFileNameExploded;
		}
	}

	private ReadOnlyCollection<string> _tokens;

	public ReadOnlyCollection<string> TokensRaw
	{
		get
		{
			_tokens ??= Raw.Split(['\\', '/'], StringSplitOptions.RemoveEmptyEntries).ToList().AsReadOnly();
			return _tokens;
		}
	}
	
	private ReadOnlyCollection<string> _tokensNormalized;

	public ReadOnlyCollection<string> TokensNormalized
	{
		get
		{
			_tokensNormalized ??= TokensRaw.Select(t => t.ToUpper()).ToList().AsReadOnly();
			return _tokensNormalized;
		}
	}
}
