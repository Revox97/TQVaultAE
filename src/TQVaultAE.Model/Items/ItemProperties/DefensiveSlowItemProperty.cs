namespace TQVaultAE.Model.Items.ItemProperties
{
    public class DefensiveSlowItemProperty : ItemProperty
    {
        public float Value { get; set; }

        public override float GetValueBySeed(int seed)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{Name} - {Value}";
        }
    }
}
