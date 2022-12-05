using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DragnDrop.Views
{
    /// <summary>
    /// Canvas.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CanvasView : UserControl
    {


        public bool IsChildHitTestVisible
        {
            get { return (bool)GetValue(IsChildHitTestVisibleProperty); }
            set { SetValue(IsChildHitTestVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChildHitTestVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsChildHitTestVisibleProperty =
            DependencyProperty.Register("IsChildHitTestVisible", typeof(bool), typeof(CanvasView), new PropertyMetadata(true));




        public CanvasView()
        {
            InitializeComponent();
        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {

        }

        private void redRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                IsChildHitTestVisible = false;
                DragDrop.DoDragDrop(redRectangle, new DataObject(DataFormats.Serializable, redRectangle), DragDropEffects.Move);
                IsChildHitTestVisible = true;
            }
        }

        private void canvas_DragLeave(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);

            if (data is UIElement element)
            {
                canvas.Children.Remove(element);
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
