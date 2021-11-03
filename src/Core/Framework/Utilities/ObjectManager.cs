using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utilities
{
    public class ObjectManager
    {
        public ObjectManager()
        {
        }
        public ObjectManager(object value, object defaultValue)
        {
            Value = value;
            DefaultValue = defaultValue;
        }

        public object Value { get; set; }
        public object DefaultValue { get; set; }

        public bool BooleanValue
        {
            get
            {
                var value = GetCorrectValue();

                bool convertedValue = false;
                if (bool.TryParse(value.ToString(), out convertedValue))
                    return convertedValue;
                return (bool)DefaultValue;
            }
        }

        public byte ByteValue
        {
            get
            {
                var value = GetCorrectValue();

                byte convertedValue = 0;
                if (byte.TryParse(GetIntegerNumberFromStringValue(value), out convertedValue))
                    return convertedValue;
                return (byte)DefaultValue;
            }
        }

        public int IntegerValue
        {
            get
            {
                var value = GetCorrectValue();

                int convertedValue = 0;
                if (int.TryParse(GetIntegerNumberFromStringValue(value), out convertedValue))
                    return convertedValue;
                return (int)DefaultValue;
            }
        }

        public long LongValue
        {
            get
            {
                var value = GetCorrectValue();

                long convertedValue = 0;
                if (long.TryParse(GetIntegerNumberFromStringValue(value), out convertedValue))
                    return convertedValue;
                return (long)DefaultValue;
            }
        }

        public static string GetIntegerNumberFromStringValue(object value)
        {
            return String.Format("{0:F20}", value).Contains(".") ? value.ToString().Split('.')[0] : value.ToString();
        }

        public Single SingleValue
        {
            get
            {
                var value = GetCorrectValue();

                Single convertedValue = 0;
                if (Single.TryParse(value.ToString(), out convertedValue))
                    return convertedValue;
                return (Single)DefaultValue;
            }
        }

        public float FloatValue
        {
            get
            {
                var value = GetCorrectValue();

                float convertedValue = 0;
                if (float.TryParse(value.ToString(), out convertedValue))
                    return convertedValue;
                return (float)DefaultValue;
            }
        }

        public double DoubleValue
        {
            get
            {
                var value = GetCorrectValue();

                double convertedValue = 0;
                if (double.TryParse(value.ToString(), out convertedValue))
                    return convertedValue;
                return (double)DefaultValue;
            }
        }

        public DateTime DateTimeValue
        {
            get
            {
                var value = GetCorrectValue();

                DateTime convertedValue = new DateTime();
                if (DateTime.TryParse(value.ToString(), out convertedValue))
                    return convertedValue;
                return (DateTime)DefaultValue;
            }
        }

        public bool? NullableBooleanValue
        {
            get
            {
                var value = GetCorrectValue();

                bool convertedValue = false;
                if (bool.TryParse(value.ToString(), out convertedValue))
                    return convertedValue;
                return (bool?)DefaultValue;
            }
        }

        public byte? NullableByteValue
        {
            get
            {
                var value = GetCorrectValue();

                byte convertedValue = 0;
                if (byte.TryParse(value.ToString(), out convertedValue))
                    return convertedValue;
                return (byte?)DefaultValue;
            }
        }

        public int? NullableIntegerValue
        {
            get
            {
                var value = GetCorrectValue();

                int convertedValue = 0;
                if (int.TryParse(value.ToString(), out convertedValue))
                    return convertedValue;
                return (int?)DefaultValue;
            }
        }

        public long? NullableLongValue
        {
            get
            {
                var value = GetCorrectValue();

                long convertedValue = 0;
                if (long.TryParse(value.ToString(), out convertedValue))
                    return convertedValue;
                return (long?)DefaultValue;
            }
        }

        public float? NullableFloatValue
        {
            get
            {
                var value = GetCorrectValue();

                float convertedValue = 0;
                if (float.TryParse(value.ToString(), out convertedValue))
                    return convertedValue;
                return (float?)DefaultValue;
            }
        }

        public double? NullableDoubleValue
        {
            get
            {
                var value = GetCorrectValue();

                double convertedValue = 0;
                if (double.TryParse(value.ToString(), out convertedValue))
                    return convertedValue;
                return (double?)DefaultValue;
            }
        }

        public Guid GuidValue
        {
            get
            {
                var value = GetCorrectValue();

                Guid convertedValue = new Guid();
                if (Guid.TryParse(value.ToString().Replace("_", "-"), out convertedValue))
                    return convertedValue;
                return (Guid)DefaultValue;
            }
        }

        public string StringValue
        {
            get
            {
                var value = GetCorrectValue();

                return value.ToString();
            }
        }

        private object GetCorrectValue()
        {
            var value = Value;

            if (value == null && DefaultValue == null)
                return string.Empty;
            else if (value == null || (value.GetType() == typeof(string) && value.ToString().Trim() == string.Empty))
                value = DefaultValue;

            return value;
        }

    }
}
