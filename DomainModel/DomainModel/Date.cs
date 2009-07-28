using System;
using System.Runtime.Serialization;

namespace DomainModel
{
    public class TravelDate
    {
        public virtual DateTime DateTime { get; set; }
        
        public TravelDate(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToBoolean(provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToChar(provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToSByte(provider);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToByte(provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToInt16(provider);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToUInt16(provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToInt32(provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToUInt32(provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToInt64(provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToUInt64(provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToSingle(provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToDouble(provider);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToDecimal(provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToDateTime(provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return ((IConvertible) DateTime).ToType(conversionType, provider);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable) DateTime).GetObjectData(info, context);
        }

        public DateTime Add(TimeSpan value)
        {
            return DateTime.Add(value);
        }

        public DateTime AddDays(double value)
        {
            return DateTime.AddDays(value);
        }

        public DateTime AddHours(double value)
        {
            return DateTime.AddHours(value);
        }

        public DateTime AddMilliseconds(double value)
        {
            return DateTime.AddMilliseconds(value);
        }

        public DateTime AddMinutes(double value)
        {
            return DateTime.AddMinutes(value);
        }

        public DateTime AddMonths(int months)
        {
            return DateTime.AddMonths(months);
        }

        public DateTime AddSeconds(double value)
        {
            return DateTime.AddSeconds(value);
        }

        public DateTime AddTicks(long value)
        {
            return DateTime.AddTicks(value);
        }

        public DateTime AddYears(int value)
        {
            return DateTime.AddYears(value);
        }

        public int CompareTo(object value)
        {
            return DateTime.CompareTo(value);
        }

        public int CompareTo(DateTime value)
        {
            return DateTime.CompareTo(value);
        }

        public bool Equals(DateTime value)
        {
            return DateTime.Equals(value);
        }

        public bool IsDaylightSavingTime()
        {
            return DateTime.IsDaylightSavingTime();
        }

        public long ToBinary()
        {
            return DateTime.ToBinary();
        }

        public TimeSpan Subtract(DateTime value)
        {
            return DateTime.Subtract(value);
        }

        public DateTime Subtract(TimeSpan value)
        {
            return DateTime.Subtract(value);
        }

        public double ToOADate()
        {
            return DateTime.ToOADate();
        }

        public long ToFileTime()
        {
            return DateTime.ToFileTime();
        }

        public long ToFileTimeUtc()
        {
            return DateTime.ToFileTimeUtc();
        }

        public DateTime ToLocalTime()
        {
            return DateTime.ToLocalTime();
        }

        public string ToLongDateString()
        {
            return DateTime.ToLongDateString();
        }

        public string ToLongTimeString()
        {
            return DateTime.ToLongTimeString();
        }

        public string ToShortDateString()
        {
            return DateTime.ToShortDateString();
        }

        public string ToShortTimeString()
        {
            return DateTime.ToShortTimeString();
        }

        public string ToString(string format)
        {
            return DateTime.ToString(format);
        }

        public string ToString(IFormatProvider provider)
        {
            return DateTime.ToString(provider);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return DateTime.ToString(format, provider);
        }

        public DateTime ToUniversalTime()
        {
            return DateTime.ToUniversalTime();
        }

        public string[] GetDateTimeFormats()
        {
            return DateTime.GetDateTimeFormats();
        }

        public string[] GetDateTimeFormats(IFormatProvider provider)
        {
            return DateTime.GetDateTimeFormats(provider);
        }

        public string[] GetDateTimeFormats(char format)
        {
            return DateTime.GetDateTimeFormats(format);
        }

        public string[] GetDateTimeFormats(char format, IFormatProvider provider)
        {
            return DateTime.GetDateTimeFormats(format, provider);
        }

        public TypeCode GetTypeCode()
        {
            return DateTime.GetTypeCode();
        }

        public DateTime Date
        {
            get { return DateTime.Date; }
        }

        public int Day
        {
            get { return DateTime.Day; }
        }

        public DayOfWeek DayOfWeek
        {
            get { return DateTime.DayOfWeek; }
        }

        public int DayOfYear
        {
            get { return DateTime.DayOfYear; }
        }

        public int Hour
        {
            get { return DateTime.Hour; }
        }

        public DateTimeKind Kind
        {
            get { return DateTime.Kind; }
        }

        public int Millisecond
        {
            get { return DateTime.Millisecond; }
        }

        public int Minute
        {
            get { return DateTime.Minute; }
        }

        public int Month
        {
            get { return DateTime.Month; }
        }

        public int Second
        {
            get { return DateTime.Second; }
        }

        public long Ticks
        {
            get { return DateTime.Ticks; }
        }

        public TimeSpan TimeOfDay
        {
            get { return DateTime.TimeOfDay; }
        }

        public int Year
        {
            get { return DateTime.Year; }
        }
    }
}