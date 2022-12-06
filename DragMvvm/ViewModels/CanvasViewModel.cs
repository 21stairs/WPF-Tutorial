using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DragMvvm.ViewModels
{
    class CanvasViewModel : BindableBase
    {
        private double _x;
        public double X
        {
            get => _x;
            set => SetProperty(ref _x, value);
        }

        private double _y;
        public double Y
        {
            get => _y;
            set => SetProperty(ref _y, value);
        }

        private string _removeRectangleName;
        public string RemoveRectangleName
        {
            get => _removeRectangleName;
            set => SetProperty(ref _removeRectangleName, value);
        }

        public ICommand SaveRectangleCommand { get; }
        public ICommand DeleteRectangleCommand { get; }

        public CanvasViewModel()
        {
            SaveRectangleCommand = new DelegateCommand(Save);
            DeleteRectangleCommand = new DelegateCommand(Delete);
        }

        public void Save()
        {
            SaveRectangleCommand?.Execute(this);
        }
        public void Delete()
        {
            DeleteRectangleCommand?.Execute(this);
        }
    }
}
