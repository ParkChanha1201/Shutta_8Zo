using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class AdvancedPlayer : Player
    {
        public AdvancedPlayer(int money) : base(money)
        {
        }

        // 3+8 => 1000점+ // 1+8/1+3 => 500점+
        public override void CalculateScore()
        {
            if (GetCard(0).IsKwang && GetCard(1).IsKwang)
                Score = 1000;
            else
                base.CalculateScore();

        }

        public override int CalculateScore(int cardIndex1, int cardIndex2)
        {

            if (GetCard(cardIndex1).IsKwang && GetCard(cardIndex2).IsKwang)
                return 1000;

            if (GetCard(cardIndex1).No == GetCard(cardIndex2).No)
                return GetCard(cardIndex1).No * 10; // 10 ~ 100
            else
                return (GetCard(cardIndex1).No + GetCard(cardIndex2).No) % 10; // 0 ~ 9
        }




    }
}
