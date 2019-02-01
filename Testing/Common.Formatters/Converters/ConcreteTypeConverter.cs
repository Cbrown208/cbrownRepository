using System;
using Newtonsoft.Json;

namespace Common.Formatters.Converters
{
    public class ConcreteTypeConverter<TInterface, TConcrete> : JsonConverter
    {
        /// <summary>
        ///     Can Convert
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TInterface);
        }

        /// <summary>
        ///     Read Json
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return serializer.Deserialize<TConcrete>(reader);
        }

        /// <summary>
        ///     Write Json
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}