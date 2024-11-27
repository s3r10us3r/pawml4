using pawm.ViewModels.Factories;
using pawm.ViewModels.Factories.Interfaces;

namespace pawm.ViewModels.Extensions
{
    public static class DependencyInjection
    {
        public static void AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<MainPageViewModel>();
        }

        public static void AddFactories(this IServiceCollection services)
        {
            services.AddSingleton<IDetailsPageViewModelFactory, DetailsPageViewModelFactory>();
        }
    }
}
