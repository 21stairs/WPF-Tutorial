 using DragListView.Models;

using Prism.Commands;
using Prism.Mvvm;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DragListView.ViewModels
{
    class TodoItemListingViewModel : BindableBase
    {
        private ObservableCollection<TodoItemModel> _todoItemModels;
        public ObservableCollection<TodoItemModel> TodoItemModels
        {
            get => _todoItemModels;
            set => SetProperty(ref _todoItemModels, value);
        }


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

            IncomeCommand = new DelegateCommand(AddTodo);
            InsertCommand = new DelegateCommand(InsertTodo);
            RemoveCommand = new DelegateCommand(RemoveTodo);
        }

        public void AddTodo()
        {
            if (!_todoItemModels.Contains(_incomeTodoItem))
            {
                _todoItemModels.Add(_incomeTodoItem);
            }
        }

        public void InsertTodo()
        {
            if(_insertTodoItem == _targetTodoItem) return;

            int oldIndex= _todoItemModels.IndexOf(_insertTodoItem);
            int nextIndex = _todoItemModels.IndexOf(_targetTodoItem);

            if (oldIndex != -1 && nextIndex != -1)
            {
                _todoItemModels.Move(oldIndex, nextIndex);
            }
        }

        public void RemoveTodo()
        {
            _todoItemModels.Remove(_removeTodoItem);
        }
    }
}
