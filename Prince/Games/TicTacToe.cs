using System;
using System.Collections.Generic;
using System.Linq;
using Prince.ExtensionMethods;


namespace Prince.Games
{
    public enum Player
    {
        X, O
    }

    struct TicTacToeMove
    {
        public Player Player;
        public int Row;
        public int Col;
    }

    public class TicTacToe : IGame
    {
        private class State
        {
            public char[,] Board = new char[3, 3];
            public Player PlayerToMove;
        }

        private State _state;

        private const char Blank = '-';

        public TicTacToe()
        {
            Reset();
        }

        /// <summary>
        /// Valid input string: "--- OX- --- X". Game layout given in three rows, followed by player to move.
        /// </summary>
        public void SetState(string state)
        {
            var stateArray = state.Split(' ');
            if (stateArray.Length != 4)
                throw new ArgumentException("Input not a valid format!");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _state.Board[i, j] = stateArray[i][j];
                }

                _state.PlayerToMove = (Player) Enum.Parse(typeof(Player), stateArray[3]);
            }
        }

        public IGame Clone() // Cloning may have a problem
        {
            var newObj = new TicTacToe();
            newObj._state.Board = (char[,])this._state.Board.Clone();
            newObj._state.PlayerToMove = this._state.PlayerToMove;

            return newObj;
        }

        public void Reset()
        {
            _state = new State {PlayerToMove = Player.X};
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _state.Board[i, j] = Blank;
                }
            }
        }

        /// <summary>
        /// Input must be of the form 'X11' for the middle square, or 'X01' for the left square, etc.
        /// </summary>
        public bool PlayMove(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input move is empty!");
            input = input.ToUpper();
            var isValidSyntax =
                input.Length == 3 &&
                char.IsLetter(input[0]) &&
                char.IsNumber(input[1]) &&
                char.IsNumber(input[2]);
            if (!isValidSyntax)
            {
                return false;
            }
            var player = char.ToUpper(input[0]) == 'X' ? Player.X : Player.O;
            if (!GetPossibleMoves().Contains(input))
            {
                return false;
            }
            var move = new TicTacToeMove
            {
                Player = player,
                Row = int.Parse(input[1].ToString()),
                Col = int.Parse(input[2].ToString())
            };
            if (move.Player == Player.X)
            {
                _state.Board[move.Row, move.Col] = 'X';
                _state.PlayerToMove = Player.O;
            }
            else
            {
                _state.Board[move.Row, move.Col] = 'O';
                _state.PlayerToMove = Player.X;
            }
            return true;
        }

        public HashSet<string> GetPossibleMoves()
        {
            var playerToMove = _state.PlayerToMove;
            var possibleMoves = new HashSet<string>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_state.Board[i, j] == Blank)
                    {
                        possibleMoves.Add(playerToMove.ToString() + i + j);
                    }
                }
            }
            return possibleMoves;
        }

        public bool IsTerminalEvaluation(float evaluation)
        {
            return Math.Abs(evaluation) == 1;
        }

        public int GetMoveValue(string move)
        {
            var row = int.Parse(move[1].ToString());
            var col = int.Parse(move[2].ToString());
            if (row == 1 && col == 1)
            {
                return 2;
            }
            else if (row % 2 == 0 && col % 2 == 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void PrintBoard()
        {
            Console.WriteLine(_state.Board.GetRow(0));
            Console.WriteLine(_state.Board.GetRow(1));
            Console.WriteLine(_state.Board.GetRow(2));
        }

        public bool CheckIfWinner(Player playerJustMoved)
        {
            var ch = playerJustMoved.ToString()[0];
            var board = _state.Board;
            return board.GetRow(0).All(c => c == ch) ||
                   board.GetRow(1).All(c => c == ch) ||
                   board.GetRow(2).All(c => c == ch) ||
                   board.GetCol(0).All(c => c == ch) ||
                   board.GetCol(1).All(c => c == ch) ||
                   board.GetCol(2).All(c => c == ch) ||
                   board[0, 0] == ch && board[1, 1] == ch && board[2, 2] == ch ||
                   board[2, 0] == ch && board[1, 1] == ch && board[0, 2] == ch;
        }

        public int? Adjudicate()
        {
            if (CheckIfWinner(Player.X))
            {
                return 1;
            }
            if (CheckIfWinner(Player.O))
            {
                return -1;
            }
            if (BoardFull())
            {
                return 0;
            }
            return null;
        }


        /// <summary>
        /// Ranges from -1 to 1.
        /// </summary>
        public float? Evaluate()
        {
            if (CheckIfWinner(Player.X) || CheckIfWinner(Player.O))
            {
                return -1;
            }
            else if (BoardFull())
            {
                return 0;
            }
            else
            {
                return null;
            }
        }

        private bool BoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_state.Board[i, j] == Blank)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
