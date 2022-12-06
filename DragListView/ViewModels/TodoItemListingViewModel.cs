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
    class TodoItemListingViewModel : BindableBase
    {
        private readonly ObservableCollection<TodoItemModel> _todoItemModels;
        public IEnumerable<TodoItemModel> TodoItemModels => _todoItemModels;


        private TodoItemModel _incomeTodoItem;
        public TodoItemModel IncomeTodoItem
        {
            get => _incomeTodoItem;
            set => SetProperty(ref _incomeTodoItem, value);
        }

        private TodoItemModel _removeTodoItem;
        public TodoItemModel RemoveTodoItem
        {
            get => _removeTodoItem;
            set => SetProperty(ref _removeTodoItem, value);
        }

        private TodoItemModel _insertTodoItem;
        public TodoItemModel InsertTodoItem
        {
            get => _insertTodoItem;
            set => SetProperty(ref _insertTodoItem, value);
        }

        private TodoItemModel _targetTodoItem;
        public TodoItemModel TargetTodoItem
        {
            get => _targetTodoItem;
            set => SetProperty(ref _targetTodoItem, value);
        }

        public ICommand IncomeCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand InsertCommand { get; }


        public TodoItemListingViewModel()
        {
            _todoItemModels = new()
            {
                new TodoItemModel("흐아"),
                new TodoItemModel("흐에"),
                new TodoItemModel("흐오"),
            };

            IncomeCommand = new DelegateCommand<TodoItemModel>(AddTodo);
            InsertCommand = new DelegateCommand<TodoItemModel, TodoItemModel>(InsertTodo);
            RemoveCommand = new DelegateCommand<TodoItemModel>(RemoveTodo);
        }

        public void AddTodo(TodoItemModel item)
        {
            if (!_todoItemModels.Contains(item))
            {
                _todoItemModels.Add(item);
            }
        }

        public void InsertTodo(TodoItemModel insertTodoItem, TodoItemModel targetTodoItem)
        {
            if(insertTodoItem == targetTodoItem) return;

            int oldIndex= _todoItemModels.IndexOf(insertTodoItem);
            int nextIndex = _todoItemModels.IndexOf(targetTodoItem);

            if (oldIndex != -1 && nextIndex != -1)
            {
                _todoItemModels.Move(oldIndex, nextIndex);
            }
        }

        public void RemoveTodo(TodoItemModel item)
        {
            _todoItemModels.Remove(item);
        }
    }
}
