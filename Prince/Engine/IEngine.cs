using Prince.Games;

namespace Prince.Engine
{
    public struct Assessment
    {
        public float Evaluation;
        public string BestMove;
    }


    interface IEngine
    {
        Assessment Calculate(IGame game);
    }
}
