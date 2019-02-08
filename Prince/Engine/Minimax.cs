using Prince.Games;
using System.Linq;

namespace Prince.Engine
{
    public class Minimax : IEngine
    {
        public Assessment Calculate(IGame game)
        {
            return Calculate(game, 999);
        }

        /// <summary>
        /// Baseline: return immediately if you find anything better than this.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="baseline"></param>
        /// <returns></returns>
        private Assessment Calculate(IGame game, float baseline = 999)
        {
            var finalResult = new Assessment { Evaluation = -999, BestMove = "" };
            var evaluation = game.Evaluate();
            if (evaluation.HasValue)
            {
                finalResult.Evaluation = evaluation.Value;
                return finalResult;
            }

            var possibleMoves = game.GetPossibleMoves().OrderBy(move => game.GetMoveValue(move));

            foreach (string move in possibleMoves)
            {
                var clonedGame = game.Clone();
                clonedGame.PlayMove(move);
                var assessment = Calculate(clonedGame, -finalResult.Evaluation);
                if (-assessment.Evaluation > finalResult.Evaluation)
                {
                    finalResult.Evaluation = -assessment.Evaluation;
                    finalResult.BestMove = move;
                }
                if (-assessment.Evaluation >= baseline)
                {
                    return finalResult;
                }
            }
            return finalResult;
        }

    }
}
