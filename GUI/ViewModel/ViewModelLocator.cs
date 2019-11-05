/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:TicTacToeGame"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System.Windows;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using TicTacToe.View;

namespace TicTacToe.ViewModel
{
    public enum WindowType
    {
        MainWindow,
        TicTacToeWindow
    }

    public class GoToWindowMessage
    {
        public Window FromWindow { get; set; }
        public WindowType ToWindow { get; set; }
    }

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainWindowViewModel>();
            SimpleIoc.Default.Register<TicTacToeViewModel>();

            Messenger.Default.Register<GoToWindowMessage>(this, msg =>
            {
                Window view;
                switch (msg.ToWindow)
                {
                    case WindowType.TicTacToeWindow:
                        view = new TicTacToeView()
                        {
                            DataContext = SimpleIoc.Default.GetInstance<TicTacToeViewModel>()
                        };
                        break;
                    case WindowType.MainWindow:
                        view = new MainWindowView()
                        {
                            DataContext = SimpleIoc.Default.GetInstance<MainWindowViewModel>()
                        };
                        break;
                    default:
                        return;
                }
                msg.FromWindow.Close();
                view.ShowDialog();
            });
        }

        public MainWindowViewModel MainView => ServiceLocator.Current.GetInstance<MainWindowViewModel>();
        public TicTacToeViewModel TicTacToe => ServiceLocator.Current.GetInstance<TicTacToeViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}