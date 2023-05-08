using Newtonsoft.Json.Converters;

namespace SimplWindowsCWSIntegration.Converters
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}