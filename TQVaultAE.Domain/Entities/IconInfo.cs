using System.Windows.Media.Imaging;

namespace TQVaultAE.Domain.Entities
{
	public record IconInfo(IconCategory Category, RecordId On, BitmapImage OnBitmap, RecordId Off, BitmapImage OffBitmap, RecordId Over, BitmapImage OverBitmap)
	{
		/// <summary>
		/// Tell if <see cref="Over"/> is a duplicate of <see cref="On"/> or <see cref="Off"/>
		/// </summary>
		public bool IsOverSameAsOthers => Over == Off || Over == On;


		/// <summary>
		/// Tell if <paramref name="resourceId"/> belong to this record
		/// </summary>
		/// <param name="resourceId"></param>
		/// <returns></returns>
		public bool Own(RecordId resourceId)
		{
			if (RecordId.IsNullOrEmpty(resourceId)) 
				return false;

			return resourceId == Off || resourceId == On || resourceId == Over;
		}
	}
}
