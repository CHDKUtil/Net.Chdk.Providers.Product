using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Net.Chdk.Providers.Product
{
    sealed class ProductProvider : DataProvider<Dictionary<string, string>>, IProductProvider
    {
        #region Constants

        private const string DataFileName = "products.json";

        #endregion

        #region Constructor

        public ProductProvider(ILoggerFactory loggerFactory)
            : base(loggerFactory.CreateLogger<ProductProvider>())
        {
        }

        #endregion

        #region IProductProvider Members

        public string[] GetProductNames()
        {
            return Data.Keys.ToArray();
        }

        public string GetCategoryName(string productName)
        {
            return Data[productName];
        }

        #endregion

        #region Data

        protected override string GetFilePath()
        {
            return Path.Combine(Directories.Data, DataFileName);
        }

        protected override LogLevel LogLevel => LogLevel.Information;

        protected override string Format => "Products: {0}";

        #endregion
    }
}
