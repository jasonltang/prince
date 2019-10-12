using Prince.Games;

namespace Prince.Engine
{
    public struct Assessment
    {
        public float Evaluation;
        public string BestMove;
        public int? MovesToFinishGame;
    }


    interface IEngine
    {
        Assessment Calculate(IGame game);
    }
}
