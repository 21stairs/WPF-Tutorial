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

namespace DragMvvm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CanvasView : Window
    {

        public bool IsChildHitTestVisible
        {
            get { return (bool)GetValue(IsChildHitTestVisibleProperty); }
            set { SetValue(IsChildHitTestVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChildHitTestVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsChildHitTestVisibleProperty =
            DependencyProperty.Register("IsChildHitTestVisible", typeof(bool), typeof(CanvasView), new PropertyMetadata(true));




        public ICommand RectangleRemoveCommand
        {
            get { return (ICommand)GetValue(RectangleRemoveCommandProperty); }
            set { SetValue(RectangleRemoveCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RectangleRemoveCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RectangleRemoveCommandProperty =
            DependencyProperty.Register("RectangleRemoveCommand", typeof(ICommand), typeof(CanvasView), 
                new PropertyMetadata(null));


        public ICommand RectangleDropCommand
        {
            get { return (ICommand)GetValue(RectangleDropCommandProperty); }
            set { SetValue(RectangleDropCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RectangleDropCommandProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RectangleDropCommandProperty =
            DependencyProperty.Register("RectangleDropCommand", typeof(ICommand), typeof(CanvasView),
                new PropertyMetadata(null));



        public string RemoveRectangleName
        {
            get { return (string)GetValue(RemoveRectangleNameProperty); }
            set { SetValue(RemoveRectangleNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RemoveRectangleName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RemoveRectangleNameProperty =
            DependencyProperty.Register("RemoveRectangleName", typeof(string), typeof(CanvasView), 
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public CanvasView()
        {
            InitializeComponent();
        }
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
    }
}
