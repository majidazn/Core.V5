using Framework.Utilities;
using System;

namespace Framework.Extensions
{
    public static class ConverterExtentions
    {
        public static bool ToBoolean(this object o, bool defaultValue)
        {
            var objectManager = new ObjectManager(o, defaultValue);
            return objectManager.BooleanValue;
        }
        public static byte ToByte(this object o, byte defaultValue)
        {
            var objectManager = new ObjectManager(o, defaultValue);
            return objectManager.ByteValue;
        }
        public static int ToInteger(this object o, int defaultValue)
        {
            var objectManager = new ObjectManager(o, defaultValue);
            return objectManager.IntegerValue;
        }
        public static long ToLong(this object o, long defaultValue)
        {
            var objectManager = new ObjectManager(o, defaultValue);
            return objectManager.LongValue;
        }
        public static Single ToSingle(this object o, Single defaultValue)
        {
            var objectManager = new ObjectManager(o, defaultValue);
            return objectManager.SingleValue;
        }
        public static float ToFloat(this object o, float defaultValue)
        {
            var objectManager = new ObjectManager(o, defaultValue);
            return objectManager.FloatValue;
        }
        public static double ToDouble(this object o, double defaultValue)
        {
            var objectManager = new ObjectManager(o, defaultValue);
            return objectManager.DoubleValue;
        }
        public static DateTime ToDateTime(this object o, DateTime defaultValue)
        {
            var objectManager = new ObjectManager(o, defaultValue);
            return objectManager.DateTimeValue;
        }
        public static Guid ToGuid(this object o, Guid defaultValue)
        {
            var objectManager = new ObjectManager(o, defaultValue);
            return objectManager.GuidValue;
        }
    }
}
