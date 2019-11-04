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
            IGame game = new TicTacToe();
            var engine = new MinimaxEngine();
            while (true)
            {
                var input = Console.ReadLine();
                if (!game.PlayMove(input))
                {
                    Console.WriteLine("Invalid move - input must be of the form 'X01'");
                    continue;
                }

                if (game.Adjudicate().HasValue)
                {
                    game.Reset();
                    continue;
                }
                var computerMove = engine.Calculate(game.Clone()).BestMove;
                game.PlayMove(computerMove);
                game.PrintBoard();
                if (game.Adjudicate().HasValue)
                {
                    game.Reset();
                }
            }
        }
    }
}
