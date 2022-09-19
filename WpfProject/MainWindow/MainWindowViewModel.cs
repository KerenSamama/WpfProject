using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BE;


namespace PL.MainWindow
{
    public class MainWindowViewModel //: INotifyPropertyChanged
    {
        // The model will provide data to the view model

        public MainWindowModel MainWindowModel { get; set; }


        public event PropertyChangingEventHandler PropertyChanged;

       
        /*protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventHandler(name));
        }*/

        public MainWindowViewModel() 
        {
            MainWindowModel = new MainWindowModel();
        }


        
             
       
    }
}
