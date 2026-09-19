using Avalonia;
using TQVaultAE.Application.Factories;
using TQVaultAE.FileFormats.Chr;
using TQVaultAE.Model.Items;
using TQVaultAE.Model.Stashes;
using TQVaultAE.TitanQuestDataProviders.SaveGame;

namespace TQVaultAE.Application.Services
{
    public class StashService
    {
        private readonly string _mainPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TQVaultTestData\Main");
        private readonly string _sysPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TQVaultTestData\Sys");

        internal async Task<ItemStash> CreateStorageAreaFromPlayerNameAsync(string name)
        {
            string path = Path.Combine(_mainPath, '_' + name, "winsys.dxb");

            ChrProvider chrProvider = new();
            ChrFile stashFile = await chrProvider.ReadAsync(path).ConfigureAwait(false);

            ItemStash storageAreaStash = new()
            {
                Width = stashFile.Root.FindElement("sackWidth")?.AsInt32() ?? 0,
                Height = stashFile.Root.FindElement("sackHeight")?.AsInt32() ?? 0,
                ItemCount = stashFile.Root.FindElement("numItems")?.AsInt32() ?? 0,
            };

            int itemStartIndex = 6;
            int itemElementCount = 4;

            for (int i = 0; i < storageAreaStash.ItemCount; i++)
            {
                int itemStart = (i * itemElementCount) + itemStartIndex;
                Item item = ReadBaseItem(stashFile, itemStart);
                storageAreaStash.Items.Add(ReadCompleteItem(item));
            }

            return storageAreaStash;
        }

        internal async Task<ItemStash> CreateTransferAreaAsync()
        {
            string path = Path.Combine(_sysPath, "winsys.dxb");

            ChrProvider chrProvider = new();
            ChrFile stashFile = await chrProvider.ReadAsync(path).ConfigureAwait(false);

            ItemStash transferAreaStash = new()
            {
                Width = stashFile.Root.FindElement("sackWidth")?.AsInt32() ?? 0,
                Height = stashFile.Root.FindElement("sackHeight")?.AsInt32() ?? 0,
                ItemCount = stashFile.Root.FindElement("numItems")?.AsInt32() ?? 0,
            };

            int itemStartIndex = 6;
            int itemElementCount = 4;

            for (int i = 0; i < transferAreaStash.ItemCount; i++)
            {
                int itemStart = (i * itemElementCount) + itemStartIndex;
                Item item = ReadBaseItem(stashFile, itemStart);
                transferAreaStash.Items.Add(ReadCompleteItem(item));
            }

            return transferAreaStash;
        }

        internal async Task<ItemStash> CreateRelicVaultAsync()
        {
            string path = Path.Combine(_sysPath, "miscsys.dxb");

            ChrProvider chrProvider = new();
            ChrFile stashFile = await chrProvider.ReadAsync(path).ConfigureAwait(false);

            ItemStash transferAreaStash = new()
            {
                Width = stashFile.Root.FindElement("sackWidth")?.AsInt32() ?? 0,
                Height = stashFile.Root.FindElement("sackHeight")?.AsInt32() ?? 0,
                ItemCount = stashFile.Root.FindElement("numItems")?.AsInt32() ?? 0,
            };

            int itemStartIndex = 6;
            int itemElementCount = 4;

            for (int i = 0; i < transferAreaStash.ItemCount; i++)
            {
                int itemStart = (i * itemElementCount) + itemStartIndex;
                Item item = ReadBaseItem(stashFile, itemStart);
                transferAreaStash.Items.Add(ReadCompleteItem(item));
            }

            return transferAreaStash;
        }

        // TODO Make the following methods more universal in item factory. Lots of redundancy here.
        private static Item ReadBaseItem(ChrFile stashFile, int itemStart)
        {
            ChrBlock itemBlock = stashFile.Root.Children[0].Children[itemStart];
            int posX = (int)stashFile.Root.Children[0].Children[itemStart + 1].AsFloat();
            int posY = (int)stashFile.Root.Children[0].Children[itemStart + 2].AsFloat();
            int stackCount = itemStart + 3 < stashFile.Root.Children[0].Children.Count ? stashFile.Root.Children[0].Children[itemStart + 3].AsInt32() : 0;

            Point position = new(posX, posY);

            return new Item()
            {
                Position = position,
                ResourcePath = itemBlock.FindElement("baseName")?.AsString() ?? string.Empty,
                Seed = itemBlock.FindElement("seed")?.AsInt32() ?? -1,
                Prefix = ReadAffix(itemBlock, "prefixName"),
                Suffix = ReadAffix(itemBlock, "suffixName"),
                Var1 = itemBlock.FindElement("var1")?.AsInt32() ?? -1,
                Var2 = itemBlock.FindElement("var2")?.AsInt32() ?? -1,
                StackCount = stackCount,
            };
        }

        private static Item ReadCompleteItem(Item item) => ItemFactory.GetCompleteItemAsync(item).Result;

        private static Affix? ReadAffix(ChrBlock item, string key)
        {
            ChrBlock? affixBlock = item.FindElement(key);

            if (affixBlock is null || affixBlock.RawData.Length == 0)
                return null;

            string result = System.Text.Encoding.UTF8.GetString(affixBlock.RawData);

            return new Affix()
            {
                Path = result
            };
        }
    }
}
