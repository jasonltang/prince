using NUnit.Framework;
using Prince.Engine;
using Prince.Games;

namespace PrinceTests.Engine
{
    [TestFixture]
    public class MinimaxEngineTests
    {
        [Test]
        public void Calculate_InitialStartingPosition_ReturnsZero()
        {
            var engine = new MinimaxEngine(true);
            var game = new TicTacToeGame();
            var result = engine.Calculate(game);
            Assert.That(result.Evaluation, Is.EqualTo(0));
        }

        [Test]
        public void Calculate_DeadLostPosition_ReturnsNegativeOne()
        {
            var engine = new MinimaxEngine(true);
            var game = new TicTacToeGame();
            game.SetState("OOX OXX XXO O");
            var result = engine.Calculate(game);
            Assert.That(result.Evaluation, Is.EqualTo(-1));
            Assert.That(result.GetMove(), Is.Null);
        }

        [TestCase("--- --- --- X", 0, "X00 X01 X02 X10 X11 X12 X20 X21 X22", null)]
        [TestCase("--- -X- --- O", 0, "O00 O02 O11 O20 O22", null)]
        [TestCase("X-- -O- --X O", 0, "O01 O10 O12 O21", null)]
        [TestCase("--- OXX --- O", 0, "O00 O02 O20 O22", null)]
        [TestCase("X-- --- --- O", 0, "O11", null)]
        [TestCase("--- OX- --- X", 1, "X00 X01 X02 X20 X21 X22", 5)] // Must win in quickest way possible
        [TestCase("--- OX- --X O", -1, "O00", 4)] // Must lose in the slowest way possible
        [TestCase("O-- -X- --X O", 0, "O02 O20", null)]
        public void Calculate_OtherVariousCases_ReturnsCorrectEvaluationAndMove(string boardState, int expectedEvaluation, string expectedBestMove, int? expectedMovesToFinishGame)
        {
            var engine = new MinimaxEngine(true);
            var game = new TicTacToeGame();
            game.SetState(boardState);
            var result = engine.Calculate(game);
            Assert.That(result.Evaluation, Is.EqualTo(expectedEvaluation));
            Assert.Contains(result.GetMove(), expectedBestMove.Split(' '));
            Assert.That(result.MovesToFinishGame, Is.EqualTo(expectedMovesToFinishGame));
        }
    }
}
