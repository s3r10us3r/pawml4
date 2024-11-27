using CommunityToolkit.Mvvm.ComponentModel;
using pawm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace pawm.ViewModels
{
    public partial class TodoElementViewModel : ObservableObject
    {
        public string Id { get; init; }
        public string ListId { get; init; }

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        public string _description;

        [ObservableProperty]
        public bool _isDone;

        public TodoElementViewModel(TodoElement todoElement)
        {
            Id = todoElement.Id;
            ListId = todoElement.ListId;
            _title = todoElement.Title; 
            _description = todoElement.Description;
            _isDone = todoElement.IsDone;
        }
    }
}
