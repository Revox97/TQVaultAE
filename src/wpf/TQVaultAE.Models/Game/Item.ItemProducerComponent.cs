using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game
{
    public partial class Item
    {
        private readonly IItemProducer? _itemProducerComponent = null!;

        public bool CanProduceItem => _itemProducerComponent is not null;

        public Item? ProducedItem => CanProduceItem ? _itemProducerComponent!.GetProducedItem() : null;

        // TODO Add required items
    }
}
