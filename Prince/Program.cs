// Todo
// Randomise the winning move.
// Prioritise immediate wins.
// Sorting of moves to make minimax more efficient.
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
            var game = new TicTacToe();
            var engine = new Minimax();
            while (true)
            {
                var input = Console.ReadLine();
                if (!game.PlayMove(input))
                {
                    Console.WriteLine("Invalid move");
                    continue;
                }
                if (game.Adjudicate()) continue;
                var computerMove = engine.Calculate(game.Clone()).BestMove;
                game.PlayMove(computerMove);
                game.PrintBoard();
                game.Adjudicate();
            }
        }
    }
}
