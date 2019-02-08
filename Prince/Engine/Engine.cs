using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prince.Games;

namespace Prince.Engine
{
    public struct Assessment
    {
        public float Evaluation;
        public string BestMove;
    }

    public static class Engine
    {
        // Needs to take an IGame as an input, being able to call PlayMove and CheckIfWin
        internal static Assessment AssessPosition(IGame game)
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
                var assessment = AssessPosition(clonedGame);
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
