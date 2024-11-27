using pawm.Services.Interfaces;

namespace pawm.Services.Extensions
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ITodoElementService, TodoElementService>();
            services.AddSingleton<ITodoListService, TodoListService>();
            services.AddSingleton<IMessageDisplayService, MauiMessageService>();
        }
    }
}
