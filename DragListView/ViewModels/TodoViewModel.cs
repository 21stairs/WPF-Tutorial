
using DragListView.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DragListView.ViewModels
{
    class TodoViewModel : BindableBase
    {
        public TodoItemListingViewModel TodoList { get; }

        public TodoViewModel()
        {
            TodoList = new();
        }

    }
}
