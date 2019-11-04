// TODO Allow play as O
// TODO GO back and fix engine to randomise moves if multiple are equal

using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Prince.Engine;
using Prince.Games;

namespace TicTacToe.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Constants
        public const string X = "C:\\Jason\\Board Games 3.jpeg";
        public const string O = "C:\\Jason\\68682899_2884475871569003_5686428909109772288_o.jpg";
        #endregion

        #region Backing Properties
        private string _imagePath00;
        public string ImagePath00
        {
            get => _imagePath00;
            set
            {
                _imagePath00 = value;
                RaisePropertyChanged(nameof(ImagePath00));
            }
        }

        private string _imagePath01;
        public string ImagePath01
        {
            get => _imagePath01;
            set
            {
                _imagePath01 = value;
                RaisePropertyChanged(nameof(ImagePath01));
            }
        }

        private string _imagePath02;
        public string ImagePath02
        {
            get => _imagePath02;
            set
            {
                _imagePath02 = value;
                RaisePropertyChanged(nameof(ImagePath02));
            }
        }

        private string _imagePath10;
        public string ImagePath10
        {
            get => _imagePath10;
            set
            {
                _imagePath10 = value;
                RaisePropertyChanged(nameof(ImagePath10));
            }
        }

        private string _imagePath11;
        public string ImagePath11
        {
            get => _imagePath11;
            set
            {
                _imagePath11 = value;
                RaisePropertyChanged(nameof(ImagePath11));
            }
        }

        private string _imagePath12;
        public string ImagePath12
        {
            get => _imagePath12;
            set
            {
                _imagePath12 = value;
                RaisePropertyChanged(nameof(ImagePath12));
            }
        }

        private string _imagePath20;
        public string ImagePath20
        {
            get => _imagePath20;
            set
            {
                _imagePath20 = value;
                RaisePropertyChanged(nameof(ImagePath20));
            }
        }

        private string _imagePath21;
        public string ImagePath21
        {
            get => _imagePath21;
            set
            {
                _imagePath21 = value;
                RaisePropertyChanged(nameof(ImagePath21));
            }
        }

        private string _imagePath22;
        public string ImagePath22
        {
            get => _imagePath22;
            set
            {
                _imagePath22 = value;
                RaisePropertyChanged(nameof(ImagePath22));
            }
        }

        private string _textBoxText;
        public string TextBoxText
        {
            get => _textBoxText;
            set
            {
                _textBoxText = value;
                RaisePropertyChanged(nameof(TextBoxText));
            }
        }
        #endregion

        public ICommand PlayMoveCommand => new RelayCommand<string>(PlayMove);
        public ICommand ResetCommand => new RelayCommand(Reset);

        private bool _canClick = true;

        IGame game = new Prince.Games.TicTacToe();
        IEngine engine = new MinimaxEngine();

        public void PlayMove(string param)
        {
            if (!_canClick)
                return;
            game.PlayMove("X" + param[0] + param[1]);
            UpdateScreen("X" + param);
            int? res;
            if ((res = game.Adjudicate()).HasValue)
            {
                TextBoxText = Result(res.Value);
                _canClick = false;
                return;
            }
            var computerMove = engine.Calculate(game.Clone()).BestMove;
            game.PlayMove(computerMove);
            UpdateScreen(computerMove);
            if ((res = game.Adjudicate()).HasValue)
            {
                TextBoxText = Result(res.Value);
                _canClick = false;
            }
        }

        private void UpdateScreen(string move)
        {
            var variableName = "ImagePath" + move[1] + move[2];
            var imageLocation = GetType().GetField(move[0].ToString()).GetValue(this);
            GetType().GetProperty(variableName).SetValue(this, imageLocation);
        }

        private string Result(int result)
        {
            switch (result)
            {
                case 1:
                    return "You won!";
                case -1:
                    return "You lost!";
                case 0:
                    return "It's a draw!";
                default:
                    return string.Empty;

            }
        }

        public void Reset()
        {
            game.Reset();
            _canClick = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var variableName = "ImagePath" + i + j;
                    GetType().GetProperty(variableName).SetValue(this, null);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
    }
}