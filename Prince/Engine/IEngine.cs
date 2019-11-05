using System;
using System.Collections.Generic;
using System.Linq;
using Prince.ExtensionMethods;
using Prince.Games;

namespace Prince.Engine
{
    public class Assessment
    {
        public float Evaluation;
        public HashSet<string> BestMoves;
        public int? MovesToFinishGame;
        
        public string GetMove()
        {
            if (BestMoves == null || !BestMoves.Any())
                return null;
            return BestMoves.ElementAt(Util.Random.Next(BestMoves.Count));
        }
            
    }


    public interface IEngine
    {
        Assessment Calculate(IGame game);
    }
}
