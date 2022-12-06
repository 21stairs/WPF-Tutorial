using Prism.Ioc;
using Prism.Unity;

using DragListView.Views;
using System.Windows;
using DragListView.Interfaces;
using DragListView.Based;

namespace DragListView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterSingleton<IView, CanvasMain>(ContentName.Canvas);
        }
    }
}
