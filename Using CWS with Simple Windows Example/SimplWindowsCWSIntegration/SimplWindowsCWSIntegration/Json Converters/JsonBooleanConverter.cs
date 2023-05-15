using Newtonsoft.Json;
using System;


namespace SimplWindowsCWSIntegration.Converters
{
    /// <summary>
    /// Converts a boolean value to closed / open.
    /// </summary>
    public class JsonShadesBooleanConverter : JsonBooleanConverter
    {
        public JsonShadesBooleanConverter()
                : base("closed", "open") { }
    }

    /// <summary>
    /// Converts a boolean value to muted / unmuted.
    /// </summary>
    public class JsonMuteBooleanConverter : JsonBooleanConverter
    {
        public JsonMuteBooleanConverter()
                : base("muted", "unmuted") { }
    }

    /// <summary>
    /// Converts a boolean value to on / off
    /// </summary>
    public class JsonOnOffBooleanConverter : JsonBooleanConverter
    {
        public JsonOnOffBooleanConverter()
                : base("on", "off") { }
    }

    public class JsonBooleanConverter : JsonConverter
    {
        private readonly string True;
        private readonly string False;

        public JsonBooleanConverter(string True, string False)
        {
            this.True = True;
            this.False = False;
        }

        public JsonBooleanConverter()
        {
            this.True = "true";
            this.False = "false";
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((bool)value) ? True : False);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value.ToString() == True;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
    }
}