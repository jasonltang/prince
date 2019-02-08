using System.Collections.Generic;

namespace Prince.Games
{
    public interface IGame
    {
        void Reset();
        bool PlayMove(string move);
        void PrintBoard();
        float? Evaluate();
        HashSet<string> GetPossibleMoves();
        IGame Clone();
        int GetMoveValue(string move);
    }
}
