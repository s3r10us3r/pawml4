using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pawm.Models;
using pawm.Services.Interfaces;
using System.Collections.ObjectModel;

namespace pawm.ViewModels
{
    public partial class DetailsPageViewModel : ObservableObject
    {
        private readonly TodoList _todoList;
        private readonly ITodoElementService _todoElementService;

        [ObservableProperty]
        private string _title = "";

        public DetailsPageViewModel(TodoList list, ITodoElementService elementService) 
        {
            _todoElementService = elementService;
            _todoList = list;
            Title = list.Name;
            LoadElements();
        }

        [ObservableProperty]
        private ObservableCollection<TodoElementViewModel> _todoElements = [];

        private async void LoadElements()
        {
            var todoElements = await _todoElementService.GetAllByListId(_todoList.Id);
            var vms = todoElements.Select(e => {
                var vm = new TodoElementViewModel(e);
                vm.PropertyChanged += async (sender, args) =>
                {
                    if (args.PropertyName == nameof(TodoElementViewModel.Title) ||
                        args.PropertyName == nameof(TodoElementViewModel.Description) ||
                        args.PropertyName == nameof(TodoElementViewModel.IsDone))
                    {
                        if (sender is TodoElementViewModel changedVm)
                            await SaveChanges(changedVm);
                    }
                };
                return vm;
            });
            TodoElements = new ObservableCollection<TodoElementViewModel>(vms);
        }

        [RelayCommand]
        private async Task AddNewTask()
        {
            var task = new TodoElement() { ListId = _todoList.Id };
            task = await _todoElementService.Create(task);
            var vm = new TodoElementViewModel(task);
            vm.PropertyChanged += async (sender, args) =>
            {
                if (args.PropertyName == nameof(TodoElementViewModel.Title) ||
                    args.PropertyName == nameof(TodoElementViewModel.Description) ||
                    args.PropertyName == nameof(TodoElementViewModel.IsDone))
                {
                    if (sender is TodoElementViewModel changedVm)
                        await SaveChanges(changedVm);
                }
            };
            TodoElements.Add(vm);
        }

        [RelayCommand]
        private async Task DeleteTask(TodoElementViewModel vm)
        {
            TodoElements.Remove(vm);
            await _todoElementService.Delete(vm.Id);
        }

        [RelayCommand]
        private async Task SaveChanges(TodoElementViewModel vm)
        {
            var todoElement = await _todoElementService.ReadOne(vm.Id);
            if (todoElement is null)
                return;
            todoElement.Title = vm.Title;
            todoElement.Description = vm.Description;
            todoElement.IsDone = vm.IsDone;
            await _todoElementService.Update(todoElement);
        }
    }
}
