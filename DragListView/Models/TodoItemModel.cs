using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragListView.Models
{
    class TodoItemModel
    {
        public string Description { get; set; }

        public TodoItemModel(string description)
        {
            Description = description;
        }
    }
}
