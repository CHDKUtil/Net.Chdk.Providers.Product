using Microsoft.Extensions.Logging;
using System.IO;

namespace Net.Chdk.Providers.Product
{
    sealed class ProductProvider : DataProvider<string[]>, IProductProvider
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

        public string[] GetProducts()
        {
            return Data;
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
