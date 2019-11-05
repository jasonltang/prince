using System;
using System.Collections.Generic;
using Prince.Games;
using System.Linq;

namespace Prince.Engine
{
    public class MinimaxEngine : IEngine
    {
        private const int MaxEval = 999;

        private bool _randomiseMoves;

        public MinimaxEngine(bool randomiseMoves = false)
        {
            _randomiseMoves = randomiseMoves;
        }

        public Assessment Calculate(IGame game)
        {
            return Calculate(game, MaxEval);
        }

        /// <summary>
        /// Baseline: return immediately if you find anything better (higher) than this.
        /// </summary>
        internal Assessment Calculate(IGame game, float baseline)
        {
            var interimResult = new Assessment
            {
                Evaluation = -MaxEval
            };

            // Return immediately if the game has reached a terminal state.
            var evaluation = game.Evaluate();
            if (evaluation.HasValue)
            {
                interimResult.Evaluation = evaluation.Value;
                return interimResult;
            }

            var possibleMoves = game.GetPossibleMoves().OrderBy(game.GetMoveValue);

            foreach (var move in possibleMoves)
            {
                var clonedGame = game.Clone();
                clonedGame.PlayMove(move);
                var assessment = Calculate(clonedGame, -interimResult.Evaluation);

                // If terminal board position, start counting backward the number of moves
                if (game.IsTerminalEvaluation(assessment.Evaluation))
                {
                    assessment.MovesToFinishGame = assessment.MovesToFinishGame.GetValueOrDefault(0) + 1;
                }

                // If you found a move which is stronger than what you have so far, overwrite your best move
                switch (IsStrongerThan(assessment, interimResult))
                {
                    case true:
                        interimResult.Evaluation = -assessment.Evaluation;
                        interimResult.BestMoves = new HashSet<string> {move};
                        interimResult.MovesToFinishGame = assessment.MovesToFinishGame;
                        break;
                    case null when _randomiseMoves:
                        interimResult.BestMoves.Add(move);
                        break;
                }

                // If your best evaluation is better than baseline,
                // then the previous opponent should've played the other (baseline) move instead, so no point evaluating this branch further.
                if (-assessment.Evaluation > baseline)
                {
                    return interimResult;
                }
            }
            return interimResult;
        }

        /// <summary>
        /// Returns null if two moves are equally good.
        /// </summary>
        private bool? IsStrongerThan(Assessment move1, Assessment move2)
        {
            if (-move1.Evaluation > move2.Evaluation) return true;
            if (-move1.Evaluation < move2.Evaluation) return false;
            if (Math.Abs(-move1.Evaluation) < 0.00001 || move1.MovesToFinishGame == move2.MovesToFinishGame)
                return null;
            if (-move1.Evaluation > 0) // Winning - best move is quickest win
                return move1.MovesToFinishGame < move2.MovesToFinishGame;
            if (-move1.Evaluation < 0) // Losing - best move is slowest loss
                return move1.MovesToFinishGame > move2.MovesToFinishGame;
            return false;
        }
    }
}
