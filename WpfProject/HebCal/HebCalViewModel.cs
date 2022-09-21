using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BE;
using BL;

namespace PL

{
    public class HebCalViewModel : INotifyPropertyChanged
    {
     
        public HebCalModel hcModel { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public HebCalViewModel()
        {
            hcModel = new HebCalModel();
        }

        public void RaisePropertyChanged(string name)
        {
   
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }



    }
}
