using pawm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pawm.ViewModels.Factories.Interfaces
{
    public interface IDetailsPageViewModelFactory
    {
        public DetailsPageViewModel CreateDetailsPageViewModel(TodoList list);
    }
}
