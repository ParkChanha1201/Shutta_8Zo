using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class ThreeCardsPlayer : Player
    {
        public ThreeCardsPlayer(int money) : base(money)
        {

        }

        public override void CalculateScore()
        {
            ThreeCardsResult result;

            List<int> scores = new List<int>();
            
            scores.Add(base.CalculateScore(1, 2));
            scores.Add(base.CalculateScore(0, 2));
            scores.Add(base.CalculateScore(0, 1));

            result = (scores[(int)ThreeCardsResult.zeroOne] > scores[(int)ThreeCardsResult.zeroTwo]) 
                ? ThreeCardsResult.zeroOne : ThreeCardsResult.zeroTwo;
            result = (scores[(int)result] > scores[(int)ThreeCardsResult.oneTwo])
                ? result : ThreeCardsResult.oneTwo;

            RemoveCard((int)result);

            Score = scores[(int)result];

        }

    }
}
