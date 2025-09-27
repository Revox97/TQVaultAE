using TQVaultAE.Models.PlayerData;
using TQVaultAE.UI.Models;

namespace TQVaultAE.UI.Builder
{
    internal class EquipmentModelBuilder
    {
        private readonly EquipmentModel _model = new();

        public EquipmentModel Build() => _model;

        internal EquipmentModelBuilder AddWeaponOne(Item item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            if (item.ItemType == ItemType.WeaponOneHanded)
            {
                _model.WeaponOne = item;
                return this;
            }

            if (item.ItemType == ItemType.WeaponTwoHanded)
            {
                _model.WeaponOne = item;
                _model.ShieldOne = item;
                return this;
            }

            throw new ArgumentException($"Item must be of type {ItemType.WeaponOneHanded} or {ItemType.WeaponTwoHanded}.");
        }

        internal EquipmentModelBuilder AddWeaponTwo(Item item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            if (item.ItemType == ItemType.WeaponOneHanded)
            {
                _model.WeaponTwo = item;
                return this;
            }

            if (item.ItemType == ItemType.WeaponTwoHanded)
            {
                _model.WeaponTwo = item;
                _model.ShieldTwo = item;
                return this;
            }

            throw new ArgumentException($"Item must be of type {ItemType.WeaponOneHanded} or {ItemType.WeaponTwoHanded}.");
        }

        internal EquipmentModelBuilder AddShieldOne(Item item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            if (item.ItemType != ItemType.Shield && item.ItemType != ItemType.WeaponOneHanded)
                throw new ArgumentException($"Item must be of type {ItemType.WeaponOneHanded} or {ItemType.Shield}.");

            if (_model.WeaponOne?.ItemType == ItemType.WeaponTwoHanded)
                _model.WeaponOne = item.ItemType == ItemType.Shield ? null! : item;

            _model.ShieldOne = item;
            return this;
        }

        internal EquipmentModelBuilder AddShieldTwo(Item item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            if (item.ItemType != ItemType.Shield && item.ItemType != ItemType.WeaponOneHanded)
                throw new ArgumentException($"Item must be of type {ItemType.WeaponOneHanded} or {ItemType.Shield}.");

            if (_model.WeaponTwo?.ItemType == ItemType.WeaponTwoHanded)
                _model.WeaponTwo = item.ItemType == ItemType.Shield ? null! : item;

            _model.ShieldTwo = item;
            return this;
        }

        internal EquipmentModelBuilder AddHeadArmor(Item item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            if (item.ItemType != ItemType.Head)
                throw new ArgumentException($"Item must be of type {ItemType.Head}.");

            _model.Head = item;
            return this;
        }

        internal EquipmentModelBuilder AddTorsoArmor(Item item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            if (item.ItemType != ItemType.Torso)
                throw new ArgumentException($"Item must be of type {ItemType.Torso}.");

            _model.Torso = item;
            return this;
        }

        internal EquipmentModelBuilder AddArmArmor(Item item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            if (item.ItemType != ItemType.Arms)
                throw new ArgumentException($"Item must be of type {ItemType.Arms}.");

            _model.Arms = item;
            return this;
        }

        internal EquipmentModelBuilder AddLegArmor(Item item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            if (item.ItemType != ItemType.Legs)
                throw new ArgumentException($"Item must be of type {ItemType.Legs}.");

            _model.Legs = item;
            return this;
        }

        internal EquipmentModelBuilder AddNecklace(Item item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            if (item.ItemType != ItemType.Necklace)
                throw new ArgumentException($"Item must be of type {ItemType.Necklace}.");

            _model.Necklace = item;
            return this;
        }

        internal EquipmentModelBuilder AddRingOne(Item item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            if (item.ItemType != ItemType.Ring)
                throw new ArgumentException($"Item must be of type {ItemType.Ring}.");

            _model.RingOne = item;
            return this;
        }

        internal EquipmentModelBuilder AddRingTwo(Item item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            if (item.ItemType != ItemType.Ring)
                throw new ArgumentException($"Item must be of type {ItemType.Ring}.");

            _model.RingTwo = item;
            return this;
        }

        internal EquipmentModelBuilder AddArtifact(Item item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            if (item.ItemType != ItemType.Artifact)
                throw new ArgumentException($"Item must be of type {ItemType.Artifact}.");

            _model.Artifact = item;
            return this;
        }
    }
}
