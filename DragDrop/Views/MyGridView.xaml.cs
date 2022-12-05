using DragnDrop.Interfaces;
using System;
using System.Windows.Controls;

namespace DragnDrop.Views
{
    /// <summary>
    /// GridView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MyGridView : UserControl, IView
    {
        public MyGridView()
        {
            InitializeComponent();
        }
    }
}
