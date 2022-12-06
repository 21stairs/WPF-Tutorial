using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DragMvvm
{
    public partial class CanvasView : Window
    {
        #region Dependency Properties

        public bool IsChildHitTestVisible
        {
            get { return (bool)GetValue(IsChildHitTestVisibleProperty); }
            set { SetValue(IsChildHitTestVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsChildHitTestVisibleProperty =
            DependencyProperty.Register("IsChildHitTestVisible", typeof(bool), typeof(CanvasView),
                new PropertyMetadata(true));


        public ICommand RectangleRemoveCommand
        {
            get { return (ICommand)GetValue(RectangleRemoveCommandProperty); }
            set { SetValue(RectangleRemoveCommandProperty, value); }
        }

        public static readonly DependencyProperty RectangleRemoveCommandProperty =
            DependencyProperty.Register("RectangleRemoveCommand", typeof(ICommand), typeof(CanvasView), 
                new PropertyMetadata(null));


        public ICommand RectangleDropCommand
        {
            get { return (ICommand)GetValue(RectangleDropCommandProperty); }
            set { SetValue(RectangleDropCommandProperty, value); }
        }

        public static readonly DependencyProperty RectangleDropCommandProperty =
            DependencyProperty.Register("RectangleDropCommand", typeof(ICommand), typeof(CanvasView),
                new PropertyMetadata(null));


        public string RemoveRectangleName
        {
            get { return (string)GetValue(RemoveRectangleNameProperty); }
            set { SetValue(RemoveRectangleNameProperty, value); }
        }

        public static readonly DependencyProperty RemoveRectangleNameProperty =
            DependencyProperty.Register("RemoveRectangleName", typeof(string), typeof(CanvasView), 
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        #endregion

        #region Constructors

        public CanvasView()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                IsChildHitTestVisible = false;
                DragDrop.DoDragDrop(normalRectangle, new DataObject(DataFormats.Serializable, normalRectangle), DragDropEffects.Move);
                IsChildHitTestVisible = true;
            }
        }
        private void Canvas_Drop(object sender, DragEventArgs e) 
        {
            RectangleDropCommand?.Execute(null);
        }

        private void canvas_DragLeave(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);

            if (data is FrameworkElement element)
            {
                canvas.Children.Remove(element);
                RemoveRectangleName = element.Name;
                RectangleRemoveCommand?.Execute(null);
            }
        }

        private void canvas_DragOver(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);

            if (data is UIElement element)
            {
                Point dropPosition = e.GetPosition(canvas);

                Canvas.SetLeft(element, dropPosition.X);
                Canvas.SetTop(element, dropPosition.Y);
                if (!canvas.Children.Contains(element))
                {
                    canvas.Children.Add(element);
                }
            }
        }

        #endregion
    }
}
