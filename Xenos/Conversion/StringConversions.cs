using System;
using System.ComponentModel;

namespace Xenos.Conversion
{
    /// <summary>
    /// Conversion methods to convert between strings and the basic primitive types.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class StringConversions
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string ConvertBoolToString(Nullable<bool> val)
        {
            return null == val ? null : val.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Nullable<bool> ConvertStringToBool(string val)
        {
            return null == val ? null : new Nullable<bool>(bool.Parse(val));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string ConvertByteToString(Nullable<byte> val)
        {
            return null == val ? null : val.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Nullable<byte> ConvertStringToByte(string val)
        {
            return null == val ? null : new Nullable<byte>(byte.Parse(val));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string ConvertCharToString(Nullable<char> val)
        {
            return null == val ? null : val.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Nullable<char> ConvertStringToChar(string val)
        {
            return null == val ? null : new Nullable<char>(char.Parse(val));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string ConvertShortToString(Nullable<short> val)
        {
            return null == val ? null : val.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Nullable<short> ConvertStringToShort(string val)
        {
            return null == val ? null : new Nullable<short>(short.Parse(val));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string ConvertUShortToString(Nullable<ushort> val)
        {
            return null == val ? null : val.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Nullable<ushort> ConvertStringToUShort(string val)
        {
            return null == val ? null : new Nullable<ushort>(ushort.Parse(val));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string ConvertIntToString(Nullable<int> val)
        {
            return null == val ? null : val.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Nullable<int> ConvertStringToInt(string val)
        {
            return null == val ? null : new Nullable<int>(int.Parse(val));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string ConvertLongToString(Nullable<long> val)
        {
            return null == val ? null : val.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Nullable<long> ConvertStringToLong(string val)
        {
            return null == val ? null : new Nullable<long>(long.Parse(val));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string ConvertULongToString(Nullable<ulong> val)
        {
            return null == val ? null : val.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Nullable<ulong> ConvertStringToULong(string val)
        {
            return null == val ? null : new Nullable<ulong>(ulong.Parse(val));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string ConvertFloatToString(Nullable<float> val)
        {
            return null == val ? null : val.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Nullable<float> ConvertStringToFloat(string val)
        {
            return null == val ? null : new Nullable<float>(float.Parse(val));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string ConvertDoubleToString(Nullable<double> val)
        {
            return null == val ? null : val.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Nullable<double> ConvertStringToDouble(string val)
        {
            return null == val ? null : new Nullable<double>(double.Parse(val));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string ConvertDecimalToString(Nullable<decimal> val)
        {
            return null == val ? null : val.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Nullable<decimal> ConvertStringToDecimal(string val)
        {
            return null == val ? null : new Nullable<decimal>(decimal.Parse(val));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string ConvertDateTimeToString(Nullable<DateTime> val)
        {
            return null == val ? null : val.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Nullable<DateTime> ConvertStringToDateTime(string val)
        {
            return null == val ? null : new Nullable<DateTime>(DateTime.Parse(val));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string ConvertEnumToString<TEnum>(Nullable<TEnum> val)
            where TEnum : struct
        {
            return null == val ? null : val.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Nullable<TEnum> ConvertStringToEnum<TEnum>(string val)
            where TEnum : struct
        {
            return null == val ? null : new Nullable<TEnum>((TEnum) Enum.Parse(typeof(TEnum), val));
        }
    }
}
