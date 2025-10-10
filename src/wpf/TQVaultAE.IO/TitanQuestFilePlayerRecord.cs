namespace TQVaultAE.IO
{
    internal class TitanQuestFilePlayerRecord : TitanQuestFileRecord
    {
        private static readonly PlayerRecordKeyDescriptor[] s_playerEnumDescriptor = [];
        // TODO replace ENUM library with something modern - at best built in
        // (
		//	from descriptor in (
		//		from e in Enums.GetMembers<TitanQuestFilePlayerRecordKey>()
		//			.SelectMany(
		//				k => k.Attributes.GetAll<TitanQuestFileDataTypeAttribute>()
		//				, (Key, Attr) => new { Key, Attr.Version, Attr.DataType })
		//		from v in new TitanQuestVersion[] { TitanQuestVersion.Original, TitanQuestVersion.ImmortalThrone, TitanQuestVersion.AnniversaryEdition }// Detailed versions
		//		where e.Version.HasFlag(v)
		//		select new PlayerRecordKeyDescriptor
		//		{
		//			Enum = e.Key.Value,
		//			Name = e.Key.AsString(EnumFormat.Description, EnumFormat.Name),
		//			DataType = e.DataType,
		//			Version = v
		//		}
		//	)
		//	group descriptor by new { descriptor.DataType, descriptor.Enum, descriptor.Version } into grp
		//	select grp.First()
		//).ToArray();

		public TitanQuestFilePlayerRecordKey KeyAsEnum { get; set; }

		public override void DefineDataType()
		{
            // Find the corresponding datatype according to the enum & version
            PlayerRecordKeyDescriptor? found = s_playerEnumDescriptor.FirstOrDefault(d => d.Name == KeyName && d.Version == File.Version);

			if (found is not null)
			{
				DataType = found.DataType;
				KeyAsEnum = found.Enum;
			}
		}

		/// <summary>
		/// Try to read value from the file
		/// </summary>
		public override void ReadValue()
		{
			DefineDataType();
			base.ReadValue();
		}
    }
}
