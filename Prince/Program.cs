// Eval function doesn't work properly, always picks first box

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prince.Games;

namespace Prince
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new TicTacToe();
            while (true)
            {
                var input = Console.ReadLine();
                if (!game.PlayMove(input))
                {
                    Console.WriteLine("Invalid move");
                    continue;
                }
                var computerMove = Engine.Engine.AssessPosition(game.Clone()).BestMove;
                game.PlayMove(computerMove);
                game.PrintBoard();
            }
        }
    }
}
