using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Data;
using System.IO;

namespace MTSUtility
{
    public class JSONSerializer
    {
        public JSONSerializer()
        {
            _formatJsonOutput = true;
        }

        public JSONSerializer(bool useJSONFormat)
        {
            _formatJsonOutput = useJSONFormat;
        }

        bool _formatJsonOutput;

        public bool FormatJsonOutput
        {
            get { return _formatJsonOutput; }
            set { _formatJsonOutput = value; }
        }

        public string Serialize(object value)
        {
            Type type = value.GetType();

            Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();

            json.NullValueHandling = NullValueHandling.Ignore;

            json.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace;
            json.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            if (type == typeof(DataRow))
                json.Converters.Add(new DataRowConverter());
            else if (type == typeof(DataTable))
                json.Converters.Add(new DataTableConverter());
            else if (type == typeof(DataSet))
                json.Converters.Add(new DataSetConverter());

            StringWriter sw = new StringWriter();
            Newtonsoft.Json.JsonTextWriter writer = new JsonTextWriter(sw);
            if (this.FormatJsonOutput)
                writer.Formatting = Formatting.Indented;
            else
                writer.Formatting = Formatting.None;

            writer.QuoteChar = '"';
            json.Serialize(writer, value);

            string output = sw.ToString();
            writer.Close();
            sw.Close();

            return output;
        }

        public object Deserialize(string jsonText, Type valueType)
        {
            Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();

            json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            json.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace;
            json.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            StringReader sr = new StringReader(jsonText);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
            object result = json.Deserialize(reader, valueType);
            reader.Close();

            return result;
        }

        /// <summary>
        /// Converts a <see cref="DataRow"/> object to and from JSON.
        /// </summary>
        public class DataRowConverter : JsonConverter
        {

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                DataRow row = value as DataRow;
                writer.WriteStartObject();
                foreach (DataColumn column in row.Table.Columns)
                {
                    writer.WritePropertyName(column.ColumnName);
                    serializer.Serialize(writer, row[column]);
                }
                writer.WriteEndObject();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override bool CanConvert(Type objectType)
            {
                return typeof(DataRow).IsAssignableFrom(objectType);
            }
        }


        /// <summary>
        /// Converts a DataTable to JSON. Note no support for deserialization
        /// </summary>
        public class DataTableConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                DataTable table = value as DataTable;
                DataRowConverter converter = new DataRowConverter();

                writer.WriteStartObject();

                writer.WritePropertyName("Rows");
                writer.WriteStartArray();

                foreach (DataRow row in table.Rows)
                {
                    converter.WriteJson(writer, row, serializer);
                }

                writer.WriteEndArray();
                writer.WriteEndObject();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override bool CanConvert(Type objectType)
            {
                return typeof(DataTable).IsAssignableFrom(objectType);
            }
        }

        /// <summary>
        /// Converts a <see cref="DataSet"/> object to JSON. No support for reading.
        /// </summary>
        public class DataSetConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                DataSet dataSet = value as DataSet;

                DataTableConverter converter = new DataTableConverter();

                writer.WriteStartObject();

                writer.WritePropertyName("Tables");
                writer.WriteStartArray();

                foreach (DataTable table in dataSet.Tables)
                {
                    converter.WriteJson(writer, table, serializer);
                }
                writer.WriteEndArray();
                writer.WriteEndObject();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override bool CanConvert(Type objectType)
            {
                return typeof(DataSet).IsAssignableFrom(objectType);
            }
        }
    }
}
