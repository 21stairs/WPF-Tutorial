using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DragnDrop.ViewModels
{
    class CanvasViewModel: BindableBase
    {
        public ICommand SaveRectangleCommand { get; }

        public CanvasViewModel()
        {
            //SaveRectangleCommand = new DelegateCommand(() =>   );
        }
    }
}
