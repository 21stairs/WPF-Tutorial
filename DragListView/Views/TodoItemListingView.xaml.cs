using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DragListView.Views
{
    public partial class TodoItemListingView : UserControl
    {
        public object IncomeTodoItem
        {
            get { return GetValue(IncomeTodoItemProperty); }
            set { SetValue(IncomeTodoItemProperty, value); }
        }

        public static readonly DependencyProperty IncomeTodoItemProperty =
            DependencyProperty.Register("IncomeTodoItem", typeof(object), typeof(TodoItemListingView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object RemoveTodoItem
        {
            get { return GetValue(RemoveTodoItemProperty); }
            set { SetValue(RemoveTodoItemProperty, value); }
        }

        public static readonly DependencyProperty RemoveTodoItemProperty =
            DependencyProperty.Register("RemoveTodoItem", typeof(object), typeof(TodoItemListingView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public object InsertTodoItem
        {
            get { return GetValue(InsertTodoItemProperty); }
            set { SetValue(InsertTodoItemProperty, value); }
        }

        public static readonly DependencyProperty InsertTodoItemProperty =
            DependencyProperty.Register("InsertTodoItem", typeof(object), typeof(TodoItemListingView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public object TargetTodoItem
        {
            get { return GetValue(TargetTodoItemProperty); }
            set { SetValue(TargetTodoItemProperty, value); }
        }

        public static readonly DependencyProperty TargetTodoItemProperty =
            DependencyProperty.Register("TargetTodoItem", typeof(object), typeof(TodoItemListingView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public ICommand DropCommand
        {
            get { return (ICommand)GetValue(DropCommandProperty); }
            set { SetValue(DropCommandProperty, value); }
        }

        public static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.Register("DropCommand", typeof(ICommand), typeof(TodoItemListingView), new PropertyMetadata(null));

        public ICommand RemoveCommand
        {
            get { return (ICommand)GetValue(RemoveCommandProperty); }
            set { SetValue(RemoveCommandProperty, value); }
        }

        public static readonly DependencyProperty RemoveCommandProperty =
            DependencyProperty.Register("RemoveCommand", typeof(ICommand), typeof(TodoItemListingView), new PropertyMetadata(null));

        public ICommand InsertCommand
        {
            get { return (ICommand)GetValue(InsertCommandProperty); }
            set { SetValue(InsertCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InsertCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InsertCommandProperty =
            DependencyProperty.Register("InsertCommand", typeof(ICommand), typeof(TodoItemListingView), new PropertyMetadata(null));

        #region Constructors

        public TodoItemListingView()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void ListView_DragLeave(object sender, DragEventArgs e)
        {
            HitTestResult result = VisualTreeHelper.HitTest(lv, e.GetPosition(lv));

            if (result is null && (RemoveCommand?.CanExecute(null) ?? false))
            {
                RemoveTodoItem = e.Data.GetData(DataFormats.Serializable);
                RemoveCommand?.Execute(RemoveTodoItem);
            }
        }

        private void ListView_DragOver(object sender, DragEventArgs e)
        {
            object todoItem = e.Data.GetData(DataFormats.Serializable);
            Add(todoItem);
        }

        private void ListViewItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed
                && sender is FrameworkElement frameworkElement)
            {
                object todoItem = frameworkElement.DataContext;

                DragDropEffects dragDropResult = DragDrop.DoDragDrop(frameworkElement,
                    new DataObject(DataFormats.Serializable, todoItem),
                    DragDropEffects.Move);

                if (dragDropResult == DragDropEffects.None)
                {
                    Add(todoItem);
                }
            }
        }

        private void ListViewItem_DragOver(object sender, DragEventArgs e)
        {
            if ((InsertCommand?.CanExecute(null) ?? false) && sender is FrameworkElement element)
            {
                TargetTodoItem = element.DataContext;
                InsertTodoItem = e.Data.GetData(DataFormats.Serializable);

                InsertCommand?.Execute(InsertTodoItem);
            }
        }

        private void Add(object todoItem)
        {
            if (DropCommand?.CanExecute(null) ?? false)
            {
                IncomeTodoItem = todoItem;
                DropCommand?.Execute(IncomeTodoItem);
            }
        }

        #endregion
    }
}
