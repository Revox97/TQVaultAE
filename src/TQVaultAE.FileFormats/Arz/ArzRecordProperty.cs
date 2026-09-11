using System.Globalization;
using System.Text;

namespace TQVaultAE.FileFormats.Arz
{
    /// <summary>
    /// Initializes a new instance of the Variable class.
    /// </summary>
    /// <param name="variableName">string name of the variable.</param>
    /// <param name="dataType">string type of data for variable.</param>
    /// <param name="numberOfValues">int number for values that the variable contains.</param>
    public class ArzRecordProperty(string variableName, ArzRecordPropertyType dataType, int numberOfValues)
    {
        public const string KEY_LEVELREQ = "LevelRequirement";
        public const string KEY_STRENGTH = "Strength";
        public const string KEY_DEXTERITY = "Dexterity";
        public const string KEY_INTELLIGENCE = "Intelligence";
        public const string KEY_LOOTRANDNAME = "lootRandomizerName";
        public const string KEY_LOOTRANDCOST = "lootRandomizerCost";
        public const string KEY_ITEMCLASS = "itemClassification";
        public const string KEY_FILEDESC = "FileDescription";

        /// <summary>
        /// the variable values.
        /// </summary>
        public object[] Values = new object[numberOfValues];

        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        public string Name { get; private set; } = variableName;

        public ArzRecordProperty Clone()
        {
            ArzRecordProperty newVariable = (ArzRecordProperty)MemberwiseClone();
            newVariable.Values = (object[])Values.Clone();
            return newVariable;
        }

        /// <summary>
        /// Gets the Datatype of the variable.
        /// </summary>
        public ArzRecordPropertyType DataType { get; private set; } = dataType;

        /// <summary>
        /// Gets the number of values that the variable contains.
        /// </summary>
        public int NumberOfValues => Values.Length;

        /// <summary>
        /// Gets or sets the generic object for a particular value.
        /// </summary>
        /// <param name="index">Index of the value.</param>
        /// <returns>object containing the value.</returns>
        public object this[int index]
        {
            get => Values[index];
            set => Values[index] = value;
        }

        public T? Get<T>(int index)
        {
            object? valueRaw = Values[index];

            if (typeof(T).IsEnum && valueRaw is not null)
                return (T)Enum.Parse(typeof(T), valueRaw.ToString() ?? string.Empty);

            if (valueRaw is T value)
                return value;

            return default;
        }

        /// <summary>
        /// Gets the integer for a value.
        /// Throws exception if value is not the correct type
        /// </summary>
        /// <param name="index">Index of the value.</param>
        /// <returns>Returns the integer for the value.</returns>
        public int GetInt32(int index = 0) => Convert.ToInt32(Values[index], CultureInfo.InvariantCulture);

        /// <summary>
        /// Gets the float for a value.
        /// </summary>
        /// <param name="index">Index of the value.</param>
        /// <returns>Single of the value.</returns>
        public float GetSingle(int index = 0) => Convert.ToSingle(Values[index], CultureInfo.InvariantCulture);

        /// <summary>
        /// Gets a string for a particular value.
        /// </summary>
        /// <param name="index">Index of the value.</param>
        /// <returns>
        /// string of value.
        /// </returns>
        public string? GetString(int index = 0) => Convert.ToString(Values[index], CultureInfo.InvariantCulture);

        /// <summary>
        /// Indicates whether any of the values in the variable are not zero.
        /// </summary>
        /// <returns>false if all values are zero or empty strings</returns>
        public bool IsValueNonZero()
        {
            if (NumberOfValues == 0)
                return false;

            foreach (object value in Values)
            {
                if (DataType == ArzRecordPropertyType.Float && Convert.ToSingle(value, CultureInfo.InvariantCulture) != 0.0F)
                    return true;
                if (DataType == ArzRecordPropertyType.String && !string.IsNullOrWhiteSpace(Convert.ToString(value, CultureInfo.InvariantCulture)))
                    return true;
                if (DataType == ArzRecordPropertyType.Integer && Convert.ToInt32(value, CultureInfo.InvariantCulture) != 0)
                    return true;
            }

            return false;
        }

        private readonly string? _toString = null;

        /// <summary>
        /// Converts the variable to a string.
        /// Format is name,val1;val2;val3;val4;...;valn,
        /// </summary>
        /// <returns>Returns converted string for the values including the variable name.</returns>
        public override string ToString()
        {
            if (_toString is not null)
                return _toString;

            // First set our val format string based on the data type
            string formatSpec = "{0}";
            if (DataType == ArzRecordPropertyType.Float)
                formatSpec = "{0:f6}";

            StringBuilder ans = new(64);
            ans.Append(Name);
            ans.Append(',');

            for (int i = 0; i < NumberOfValues; ++i)
            {
                if (i > 0)
                    ans.Append(';');

                ans.AppendFormat(CultureInfo.InvariantCulture, formatSpec, Values[i]);
            }

            ans.Append(',');
            return ans.ToString();
        }

        /// <summary>
        /// Indicate that some values are not equals default(type). Meaning there is something to display.
        /// </summary>
        public bool IsValueRelevant
        {
            get
            {
                foreach (object val in Values)
                {
                    switch (DataType)
                    {
                        case ArzRecordPropertyType.Integer:
                            int intval = Convert.ToInt32(val, CultureInfo.InvariantCulture);
                            if (intval != default) return true;
                            break;
                        case ArzRecordPropertyType.Float:
                            float fltval = Convert.ToSingle(val, CultureInfo.InvariantCulture);
                            if (fltval != default) return true;
                            break;
                        case ArzRecordPropertyType.String:
                            string? strtval = Convert.ToString(val, CultureInfo.InvariantCulture);
                            if (!string.IsNullOrWhiteSpace(strtval)) return true;
                            break;
                        case ArzRecordPropertyType.Boolean:
                            bool boolval = Convert.ToBoolean(val, CultureInfo.InvariantCulture);
                            if (boolval != default) return true;
                            break;
                        case ArzRecordPropertyType.Unknown:
                        default:
                            return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Converts the values to a string.
        /// Format is name,val1;val2;val3;val4;...;valn,
        /// </summary>
        /// <returns>Returns converted string for the values.</returns>
        public string ToStringValue()
        {
            // First set our val format string based on the data type
            string formatSpec = "{0}";
            if (DataType == ArzRecordPropertyType.Float)
                formatSpec = "{0:f6}";

            StringBuilder ans = new(64);
            for (int i = 0; i < NumberOfValues; ++i)
            {
                if (i > 0)
                    ans.Append(", ");

                ans.AppendFormat(CultureInfo.InvariantCulture, formatSpec, Values[i]);
            }

            return ans.ToString();
        }
    }
}
