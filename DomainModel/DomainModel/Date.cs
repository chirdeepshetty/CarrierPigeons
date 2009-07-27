using System;
using System.Runtime.Serialization;

namespace DomainModel
{
    public class TravelDate
    {
        DateTime _dateTime { get; set; }
        
        public TravelDate(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToBoolean(provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToChar(provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToSByte(provider);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToByte(provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToInt16(provider);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToUInt16(provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToInt32(provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToUInt32(provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToInt64(provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToUInt64(provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToSingle(provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToDouble(provider);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToDecimal(provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToDateTime(provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return ((IConvertible) _dateTime).ToType(conversionType, provider);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable) _dateTime).GetObjectData(info, context);
        }

        public DateTime Add(TimeSpan value)
        {
            return _dateTime.Add(value);
        }

        public DateTime AddDays(double value)
        {
            return _dateTime.AddDays(value);
        }

        public DateTime AddHours(double value)
        {
            return _dateTime.AddHours(value);
        }

        public DateTime AddMilliseconds(double value)
        {
            return _dateTime.AddMilliseconds(value);
        }

        public DateTime AddMinutes(double value)
        {
            return _dateTime.AddMinutes(value);
        }

        public DateTime AddMonths(int months)
        {
            return _dateTime.AddMonths(months);
        }

        public DateTime AddSeconds(double value)
        {
            return _dateTime.AddSeconds(value);
        }

        public DateTime AddTicks(long value)
        {
            return _dateTime.AddTicks(value);
        }

        public DateTime AddYears(int value)
        {
            return _dateTime.AddYears(value);
        }

        public int CompareTo(object value)
        {
            return _dateTime.CompareTo(value);
        }

        public int CompareTo(DateTime value)
        {
            return _dateTime.CompareTo(value);
        }

        public bool Equals(DateTime value)
        {
            return _dateTime.Equals(value);
        }

        public bool IsDaylightSavingTime()
        {
            return _dateTime.IsDaylightSavingTime();
        }

        public long ToBinary()
        {
            return _dateTime.ToBinary();
        }

        public TimeSpan Subtract(DateTime value)
        {
            return _dateTime.Subtract(value);
        }

        public DateTime Subtract(TimeSpan value)
        {
            return _dateTime.Subtract(value);
        }

        public double ToOADate()
        {
            return _dateTime.ToOADate();
        }

        public long ToFileTime()
        {
            return _dateTime.ToFileTime();
        }

        public long ToFileTimeUtc()
        {
            return _dateTime.ToFileTimeUtc();
        }

        public DateTime ToLocalTime()
        {
            return _dateTime.ToLocalTime();
        }

        public string ToLongDateString()
        {
            return _dateTime.ToLongDateString();
        }

        public string ToLongTimeString()
        {
            return _dateTime.ToLongTimeString();
        }

        public string ToShortDateString()
        {
            return _dateTime.ToShortDateString();
        }

        public string ToShortTimeString()
        {
            return _dateTime.ToShortTimeString();
        }

        public string ToString(string format)
        {
            return _dateTime.ToString(format);
        }

        public string ToString(IFormatProvider provider)
        {
            return _dateTime.ToString(provider);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return _dateTime.ToString(format, provider);
        }

        public DateTime ToUniversalTime()
        {
            return _dateTime.ToUniversalTime();
        }

        public string[] GetDateTimeFormats()
        {
            return _dateTime.GetDateTimeFormats();
        }

        public string[] GetDateTimeFormats(IFormatProvider provider)
        {
            return _dateTime.GetDateTimeFormats(provider);
        }

        public string[] GetDateTimeFormats(char format)
        {
            return _dateTime.GetDateTimeFormats(format);
        }

        public string[] GetDateTimeFormats(char format, IFormatProvider provider)
        {
            return _dateTime.GetDateTimeFormats(format, provider);
        }

        public TypeCode GetTypeCode()
        {
            return _dateTime.GetTypeCode();
        }

        public DateTime Date
        {
            get { return _dateTime.Date; }
        }

        public int Day
        {
            get { return _dateTime.Day; }
        }

        public DayOfWeek DayOfWeek
        {
            get { return _dateTime.DayOfWeek; }
        }

        public int DayOfYear
        {
            get { return _dateTime.DayOfYear; }
        }

        public int Hour
        {
            get { return _dateTime.Hour; }
        }

        public DateTimeKind Kind
        {
            get { return _dateTime.Kind; }
        }

        public int Millisecond
        {
            get { return _dateTime.Millisecond; }
        }

        public int Minute
        {
            get { return _dateTime.Minute; }
        }

        public int Month
        {
            get { return _dateTime.Month; }
        }

        public int Second
        {
            get { return _dateTime.Second; }
        }

        public long Ticks
        {
            get { return _dateTime.Ticks; }
        }

        public TimeSpan TimeOfDay
        {
            get { return _dateTime.TimeOfDay; }
        }

        public int Year
        {
            get { return _dateTime.Year; }
        }
    }
}