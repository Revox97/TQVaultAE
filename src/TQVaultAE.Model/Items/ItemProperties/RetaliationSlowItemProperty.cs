namespace TQVaultAE.Model.Items.ItemProperties
{
    public class RetaliationSlowItemProperty : ItemProperty
    {
        public bool IsGlobal { get; set; }

        public float Chance { get; set; }

        public float DurationMax { get; set; }

        public float DurationMin { get; set; }

        public float Max { get; set; }

        public float Min { get; set; }

        public override float GetValueBySeed(int seed)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
