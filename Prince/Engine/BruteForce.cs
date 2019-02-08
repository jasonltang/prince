using Prince.Games;

namespace Prince.Engine
{
    public class BruteForce : IEngine
    {
        // Needs to take an IGame as an input, being able to call PlayMove and CheckIfWin
        public Assessment Calculate(IGame game)
        {
            var finalResult = new Assessment { Evaluation = -999, BestMove = "" };
            var evaluation = game.Evaluate();
            if (evaluation.HasValue)
            {
                finalResult.Evaluation = evaluation.Value;
                return finalResult;
            }
            
            foreach (string move in game.GetPossibleMoves())
            {
                var clonedGame = game.Clone();
                clonedGame.PlayMove(move);
                var assessment = Calculate(clonedGame);
                if (-assessment.Evaluation > finalResult.Evaluation)
                {
                    finalResult.Evaluation = -assessment.Evaluation;
                    finalResult.BestMove = move;
                }
            }
            return finalResult;
        }

        /// <summary>
        /// Baseline: return immediately if you find anything better than this.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="baseline"></param>
        /// <returns></returns>
        internal static Assessment Minimax(IGame game, float baseline = 999)
        {
            var finalResult = new Assessment { Evaluation = -999, BestMove = "" };
            var evaluation = game.Evaluate();
            if (evaluation.HasValue)
            {
                finalResult.Evaluation = evaluation.Value;
                return finalResult;
            }

            foreach (string move in game.GetPossibleMoves())
            {
                var clonedGame = game.Clone();
                clonedGame.PlayMove(move);
                var assessment = Minimax(clonedGame, -finalResult.Evaluation);
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
