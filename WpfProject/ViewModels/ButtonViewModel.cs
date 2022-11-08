using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfProject.Command;

namespace WpfProject.ViewModels
{
    class ButtonViewModel
    {
        public Flights flight { get; set; }

        public ButtonViewModel()
        {
            flight = new Flights(this);
        }

        public void OnExecute()
        {
            MessageBox.Show("Keke is Beautiful");
        }
    }
}
