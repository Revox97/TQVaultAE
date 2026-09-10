namespace TQVaultAE.TitanQuestDataProviders.Model
{
    // Format of an ARC file
    // 0x00-0x02: "ARC" header (0x41, 0x52, 0x43)
    // 0x08 - 4 bytes = # of files (numEntries)
    // 0x0C - 4 bytes = # of parts (numParts)
    // 0x18 - 4 bytes = offset to directory structure (tocOffset)

    // Format of directory structure (at tocOffset):
    // Part entries: numParts * 12 bytes each:
    //   4-byte int = offset in file where this part begins
    //   4-byte int = size of compressed part
    //   4-byte int = size of uncompressed part
    //   These triplets repeat for each part in the arc file

    // After the part triplets are a bunch of null-terminated strings
    // which are the sub filenames.

    // Then file record entries: numEntries * 44 bytes each (seek from end of file):
    //   4-byte int = storageType (3 = compressed, 1 = non-compressed)
    //   4-byte int = offset in file where first part of this subfile begins
    //   4-byte int = compressed size of this file
    //   4-byte int = uncompressed size of this file
    //   4-byte crap (timestamp?)
    //   4-byte crap (timestamp?)
    //   4-byte crap (timestamp?)
    //   4-byte int = numParts this file uses
    //   4-byte int = part# of first part for this file (starting at 0)
    //   4-byte int = length of filename string
    //   4-byte int = offset in directory structure for filename

    // Then at fileNamesOffset: null-terminated ASCII filenames
    public class ArcFile
    {
        public string FileName { get; init; } = string.Empty;
        public List<ArcFileRecord> Records { get; set; } = [];
    }
}
