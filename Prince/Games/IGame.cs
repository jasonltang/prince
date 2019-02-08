using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prince.Games
{
    interface IGame
    {
        void Reset();
        bool PlayMove(string move);
        void PrintBoard();
        float? Evaluate();
        HashSet<string> GetPossibleMoves();
        IGame Clone();
    }
}
