using Microsoft.Extensions.Logging;
using Net.Chdk.Model.Category;
using Net.Chdk.Providers.Category;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Net.Chdk.Providers.Product
{
    sealed class ProductProvider : DataProvider<Dictionary<string, string>>, IProductProvider, ICategoryProvider
    {
        #region Constants

        private const string DataFileName = "products.json";

        #endregion

        #region Constructor

        public ProductProvider(ILoggerFactory loggerFactory)
            : base(loggerFactory.CreateLogger<ProductProvider>())
        {
            _categories = new Lazy<CategoryInfo[]>(DoGetCategories);
            _categoryNames = new Lazy<string[]>(DoGetCategoryNames);
        }

        #endregion

        #region ICategoryProvider Members

        public CategoryInfo[] GetCategories()
        {
            return Categories;
        }

        public string[] GetCategoryNames()
        {
            return CategoryNames;
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

        #region Categories

        private readonly Lazy<CategoryInfo[]> _categories;

        private CategoryInfo[] Categories => _categories.Value;

        private CategoryInfo[] DoGetCategories()
        {
            return CategoryNames
                .Select(GetCategory)
                .ToArray();
        }

        private static CategoryInfo GetCategory(string name)
        {
            return new CategoryInfo
            {
                Name = name
            };
        }

        #endregion

        #region CategoryNames

        private readonly Lazy<string[]> _categoryNames;

        private string[] CategoryNames => _categoryNames.Value;

        private string[] DoGetCategoryNames()
        {
            return Data.Values
                .Distinct()
                .OrderBy(c => c)
                .ToArray();
        }

        #endregion
    }
}
