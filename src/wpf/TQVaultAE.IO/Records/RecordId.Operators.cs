namespace TQVaultAE.IO.Records
{
    public partial class RecordId
    {
        public bool Equals(RecordId? other)
        {
            return other is not null && Normalized.Equals(other.Normalized);
        }

        public override bool Equals(object? obj) => obj is RecordId recordId && Equals(recordId);

        public static bool operator ==(RecordId lhs, RecordId rhs) => Equals(lhs, rhs);

        public static bool operator !=(RecordId lhs, RecordId rhs) => !(lhs == rhs);

        public override int GetHashCode() => Normalized.GetHashCode();

        public int CompareTo(RecordId? other)
        {
            return other is not null
                ? string.Compare(Normalized, other.Normalized, StringComparison.Ordinal)
                : 1;
        }

        public int CompareTo(object? obj)
        {
            return obj is RecordId other
                ? CompareTo(other)
                : throw new ArgumentException($"A {nameof(RecordId)} object is required for comparison.", nameof(obj));
        }

        public static bool operator <(RecordId left, RecordId right) => Compare(left, right) < 0;

        public static bool operator >(RecordId left, RecordId right) => Compare(left, right) > 0;

        public static int Compare(RecordId left, RecordId right)
        {
            if (ReferenceEquals(left, right))
                return 0;

            return left is not null ? left.CompareTo(right) : -1;
        }

        public static implicit operator string(RecordId recordId) => recordId.Raw;
        public static implicit operator RecordId(string recordId) => Create(recordId);
    }
}
