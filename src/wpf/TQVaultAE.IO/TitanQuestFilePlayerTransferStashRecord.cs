namespace TQVaultAE.IO
{
    internal class TitanQuestFilePlayerTransferStashRecord : TitanQuestFileRecord
    {
        private static readonly PlayerTransferStashRecordKeyDescriptor[] s_playerEnumDescriptor = [];
        // TODO replace Enum lib
        // (
		//	from descriptor in (
		//		from e in Enums.GetMembers<TQFilePlayerTransferStashKey>()
		//			.SelectMany(
		//				k => k.Attributes.GetAll<TQFileDataTypeAttribute>()
		//				, (Key, Attr) => new { Key, Attr.Version, Attr.DataType })
		//		from v in new TQVersion[] { TQVersion.TQ, TQVersion.TQIT, TQVersion.TQAE }// Detailed versions
		//		where e.Version.HasFlag(v)
		//		select new PlayerTransferStashRecordKeyDescriptor
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

		public TitanQuestFilePlayerTransferStashKey KeyAsEnum { get; set; }

		public override void DefineDataType()
		{
            // Find the corresponding datatype according to the enum & version
            PlayerTransferStashRecordKeyDescriptor? found = s_playerEnumDescriptor.FirstOrDefault(d => d.Name == KeyName && d.Version == File.Version);

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
