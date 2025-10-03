using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game.Components
{
    public class ItemProducerComponent(Item producedItem) : IItemProducer
    {
        private readonly Item _producedItem = producedItem;

        public Item GetProducedItem() => _producedItem;
    }
}
