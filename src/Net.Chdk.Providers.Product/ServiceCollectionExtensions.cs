using Microsoft.Extensions.DependencyInjection;
using Net.Chdk.Providers.Category;

namespace Net.Chdk.Providers.Product
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProductProvider(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<ICategoryProvider, ProductProvider>()
                .AddSingleton<IProductProvider, ProductProvider>();
        }
    }
}
