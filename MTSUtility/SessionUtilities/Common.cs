using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MTSUtility.SessionUtilities
{
    public static class Common
    {
        public static String ObjSerializer<T>(T obj)
        {
            if (!obj.GetType().IsSerializable)
            {
                return null;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, obj);
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        public static object ObjDeSerializer(string serialized)
        {
            if (serialized.Length == 0)
            {
                return null;
            }

            byte[] bytes = Convert.FromBase64String(serialized);

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return new BinaryFormatter().Deserialize(stream);
            }
        }
    }
}
