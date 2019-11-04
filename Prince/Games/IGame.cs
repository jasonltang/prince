using System.Collections.Generic;

namespace Prince.Games
{
    public interface IGame
    {
        void Reset();
        bool PlayMove(string move);
        void PrintBoard();
        /// <summary>
        /// Evaluation, from the perspective of the player who is about to move.
        /// Returns any positive or negative number, with positive meaning more winning.
        /// </summary>
        /// <returns></returns>
        float? Evaluate();
        HashSet<string> GetPossibleMoves();
        IGame Clone();
        /// <summary>
        /// A function to specify in which order moves should be evaluated, with higher outputs being evaluated first.
        /// </summary>
        int GetMoveValue(string move);
        /// <summary>
        /// Set the game state based on an input string (syntax varies by game)
        /// </summary>
        void SetState(string state);
        /// <summary>
        /// Indicates whether the given evaluation indicates a forced win/loss.
        /// </summary>
        /// <param name="evaluation"></param>
        /// <returns></returns>
        bool IsTerminalEvaluation(float evaluation);

        int? Adjudicate();
    }
}
