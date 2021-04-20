using System;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace backend.domain.Converters
{
    public class DynamoNullableDateTimeConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            DynamoDBEntry entry = new Primitive { Value = null };

            //Format - 2015-03-12T20:24:07.647Z
            if (value != null)
                entry = new Primitive { Value = ((DateTime)value).ToUniversalTime().ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ") };

            return entry;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            if (entry is not Primitive primitive) 
                return null;
            var dtString = primitive.Value.ToString();
            if (string.IsNullOrWhiteSpace(dtString))
                return null;

            var value = DateTime.Parse(dtString, null, System.Globalization.DateTimeStyles.RoundtripKind);
            return value;

        }
    }
}