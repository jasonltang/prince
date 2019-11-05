using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

// TODO: Fix switching between windows

namespace TicTacToe.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand GoToTicTacToeCommand => new RelayCommand<Window>(window =>
        {
            var msg = new GoToWindowMessage { FromWindow = window, ToWindow = WindowType.TicTacToeWindow };
            Messenger.Default.Send(msg);
        });

        //public void GoToTicTacToe()
        //{
        //    var window = new TicTacToeWindow()
        //    {
        //        DataContext = new TicTacToeViewModel()
        //    };
        //    window.Show();
        //}
    }
}