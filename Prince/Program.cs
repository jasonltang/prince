// Todo
// More unit tests. -- Keep writing.
// Prioritise quicker wins - done
// Randomise the winning move - should be a matter of passing in an optional parameter "TrackAllWinningMoves"
// Sorting of moves to make minimax more efficient. -- Done
// Allow computer to play both sides.

using System;
using Prince.Engine;
using Prince.Games;

namespace Prince
{
    class Program
    {
        static void Main(string[] args)
        {
            IGame game = new TicTacToeGame();
            var engine = new MinimaxEngine(true);
            while (true)
            {
                var input = Console.ReadLine();
                if (!game.PlayMove(input))
                {
                    Console.WriteLine("Invalid move - input must be of the form 'X01'");
                    continue;
                }

                int? res;
                if ((res = game.Adjudicate(Player.X)).HasValue)
                {
                    Console.WriteLine(((TicTacToeGame)game).Result(res.Value));
                    game.Reset();
                    continue;
                }
                var computerMove = engine.Calculate(game.Clone()).GetMove();
                game.PlayMove(computerMove);
                game.PrintBoard();
                if (game.Adjudicate(Player.X).HasValue)
                {
                    game.Reset();
                }
            }
        }
    }
}
