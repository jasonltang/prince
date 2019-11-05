using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Prince.Engine;
using Prince.Games;

namespace TicTacToe.ViewModel
{
    public class TicTacToeViewModel : ViewModelBase
    {
        #region Constants
        public const string X = "..\\Images\\corgi.png";
        public const string O = "..\\Images\\penguin.png";
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
        public ICommand ResetPlaySecondCommand => new RelayCommand(ResetPlaySecond);
        public ICommand GoToHomeScreenCommand => new RelayCommand<Window>(window =>
        {
            var msg = new GoToWindowMessage {FromWindow = window, ToWindow = WindowType.MainWindow};
            Messenger.Default.Send(msg);
        });

        private bool _canClick = true;
        private Player _playerSide = Player.X;

        TicTacToeGame game = new TicTacToeGame();
        IEngine engine = new MinimaxEngine(true);

        public void PlayMove(string param)
        {
            if (!_canClick)
                return;
            if (!game.PlayMove(_playerSide.ToString() + param[0] + param[1]))
                return;
            UpdateScreen(_playerSide + param);
            int? res;
            if ((res = game.Adjudicate(_playerSide)).HasValue)
            {
                TextBoxText = game.Result(res.Value);
                _canClick = false;
                return;
            }
            var computerMove = engine.Calculate(game.Clone()).GetMove();
            game.PlayMove(computerMove);
            UpdateScreen(computerMove);
            if ((res = game.Adjudicate(_playerSide)).HasValue)
            {
                TextBoxText = game.Result(res.Value);
                _canClick = false;
            }
        }

        private void UpdateScreen(string move)
        {
            var variableName = "ImagePath" + move[1] + move[2];
            var imageLocation = GetType().GetField(move[0].ToString()).GetValue(this);
            GetType().GetProperty(variableName).SetValue(this, imageLocation);
        }

        public void Reset()
        {
            game.Reset();
            _canClick = true;
            TextBoxText = default;
            _playerSide = Player.X;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var variableName = "ImagePath" + i + j;
                    GetType().GetProperty(variableName).SetValue(this, null);
                }
            }
        }

        public void ResetPlaySecond()
        {
            Reset();
            _playerSide = Player.O;
            var computerMove = game.GetFirstMove();
            game.PlayMove(computerMove);
            UpdateScreen(computerMove);
        }

        public TicTacToeViewModel()
        {
        }
    }
}