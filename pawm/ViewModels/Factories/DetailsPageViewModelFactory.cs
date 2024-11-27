using pawm.Models;
using pawm.Services.Interfaces;
using pawm.ViewModels.Factories.Interfaces;

namespace pawm.ViewModels.Factories
{
    public class DetailsPageViewModelFactory : IDetailsPageViewModelFactory
    {
        private readonly ITodoElementService _todoElementService;

        public DetailsPageViewModelFactory(ITodoElementService todoElementService) 
        { 
            _todoElementService = todoElementService;
        }

        public DetailsPageViewModel CreateDetailsPageViewModel(TodoList list)
        {
            return new DetailsPageViewModel(list, _todoElementService);
        }
    }
}
