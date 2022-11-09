using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfProject.ViewModels;

namespace WpfProject.Command
{
    class Flights : ICommand
    {
        ButtonViewModel _buttonViewModel;
        public Flights(ButtonViewModel viewmodel)
        {
            _buttonViewModel = viewmodel;
        }
        

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)// bouton est executer ou pas 
        {
            return true;
        }

        public void Execute(object parameter)// c'est ce que fait la fonction quand on click sur le bouton
        {
            _buttonViewModel.OnExecute();
        }
    }

}
