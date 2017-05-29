using Newtonsoft.Json;
using System;
using System.IO;

namespace Net.Chdk.Providers.Product
{
    sealed class ProductProvider : IProductProvider
    {
        #region Constants

        private const string DataPath = "Data";
        private string DataFileName = "products.json";

        #endregion

        #region Constructor

        public ProductProvider()
        {
            data = new Lazy<string[]>(GetData);
        }

        #endregion

        #region IProductProvider Members

        public string[] GetProducts()
        {
            return Data;
        }

        #endregion

        #region Serializer

        private static readonly Lazy<JsonSerializer> serializer = new Lazy<JsonSerializer>(GetSerializer);

        private static JsonSerializer Serializer => serializer.Value;

        private static JsonSerializer GetSerializer()
        {
            return JsonSerializer.CreateDefault();
        }

        #endregion

        #region Data

        private readonly Lazy<string[]> data;

        private string[] Data => data.Value;

        private string[] GetData()
        {
            var filePath = Path.Combine(DataPath, DataFileName);
            using (var reader = File.OpenText(filePath))
            using (var jsonReader = new JsonTextReader(reader))
            {
                return Serializer.Deserialize<string[]>(jsonReader);
            }
        }

        #endregion
    }
}
