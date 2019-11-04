using Prince.Games;

namespace Prince.Engine
{
    public class BruteForceEngine : IEngine
    {
        // Needs to take an IGame as an input, being able to call PlayMove and CheckIfWin
        public Assessment Calculate(IGame game)
        {
            var finalResult = new Assessment { Evaluation = -999 };
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
    }
}
