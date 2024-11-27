using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pawm.Models;
using pawm.Services.Interfaces;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using pawm.ViewModels.Factories.Interfaces;
using pawm.ViewModels.Factories;

namespace pawm.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly ITodoListService _todoListService;
        private readonly IMessageDisplayService _messageDisplayService;
        private readonly IDetailsPageViewModelFactory _detailsPageViewModelFactory;

        public MainPageViewModel(ITodoListService todoListService, IMessageDisplayService messageDisplayService, IDetailsPageViewModelFactory detailsPageViewModelFactory)
        {
            _todoListService = todoListService;
            _messageDisplayService = messageDisplayService;
            _detailsPageViewModelFactory = detailsPageViewModelFactory;
            LoadTodoListAsync();
        }

        private async void LoadTodoListAsync()
        {
            TodoLists = new ObservableCollection<TodoList>(await _todoListService.ReadAll());
        }

        [ObservableProperty]
        private ObservableCollection<TodoList> _todoLists = [];

        [RelayCommand]
        private async Task AddNewTaskList()
        {
            var newListTitle = await _messageDisplayService.AskForText("New list name", "Type new list name below.");
            if (string.IsNullOrWhiteSpace(newListTitle))
                return;
            
            var newList = new TodoList() { Name = newListTitle };
            newList = await _todoListService.Create(newList);
            TodoLists.Add(newList);
        }

        [RelayCommand]
        private async Task DeleteTaskList(string id)
        {
            var list = TodoLists.FirstOrDefault(t => t.Id == id);
            if(list == null)
            {
                await _messageDisplayService.DisplayMessage("Something went wrong :/");
                return;
            }
            TodoLists.Remove(list);
            await _todoListService.Delete(list.Id);
        }

        [RelayCommand]
        private async Task EditTaskListName(string id)
        {
            Console.WriteLine(id);
            var list = TodoLists.FirstOrDefault(t => t.Id == id);
            if (list is null)
            {
                await _messageDisplayService.DisplayMessage("Something went wrong :/");
                return;
            }

            int listIndex = TodoLists.IndexOf(list);
            var newName = await _messageDisplayService.AskForText("Edit title", "New list title:", defaultText: list.Name);
            if (string.IsNullOrEmpty(newName))
            {
                return;
            }

            list.Name = newName;
            var updatedList = await _todoListService.Update(list);
            TodoLists[listIndex] = updatedList;
        }

        [RelayCommand]
        private async Task OpenDetails(TodoList list)
        {
            var vm = _detailsPageViewModelFactory.CreateDetailsPageViewModel(list);
            var page = new DetailsPage(vm);
            await Shell.Current.Navigation.PushAsync(page);
        }
    }
}

