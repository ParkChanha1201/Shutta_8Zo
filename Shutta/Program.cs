using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    class Program
    {
        public const int PlayerCount = 2;
        public const int SeedMoney = 500;
        private const int BetMoney = 100;
        private static int StartCards = 3;

        static void Main(string[] args)
        {
            Console.WriteLine("게임의 룰을 선택하세요. (1:기본룰, 2:확장룰, 3:3장룰, 4:궁극의룰)");
            string inputText = Console.ReadLine();
            int input = int.Parse(inputText);
            RuleType ruleType = (RuleType)input;


            // 각 선수들이 시드 머니를 가진다.
            List<Player> players = new List<Player>();
            for (int i = 0; i < PlayerCount; i++)
            {
                if (ruleType == RuleType.Basic)
                {
                    players.Add(new Player(SeedMoney));
                    StartCards = 2;
                }
                else if (ruleType == RuleType.Advanced)
                {
                    players.Add(new AdvancedPlayer(SeedMoney));
                    StartCards = 2;
                }
                else if (ruleType == RuleType.ThreeCards)
                    players.Add(new ThreeCardsPlayer(SeedMoney));
                else if (ruleType == RuleType.Ultimate)
                    players.Add(new UltimatePlayer(SeedMoney));
            }



            int round = 1;
            // 선수 중 파산(오링)하는 사람이 있을 때 까지 라운드를 진행한다.
            while (true)
            {
                if (CanRunRound(players) == false)
                    break;


                Console.WriteLine($"[Round {round++}]");


                // 라운드를 진행한다
                RunRound(players);

                // 선수들이 가진 돈을 출력한다.
                PrintMoney(players);

                Console.WriteLine();
            }
        }

        private static void PrintMoney(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
                Console.WriteLine($"P{i} has \\{players[i].Money}");
        }

        private static bool CanRunRound(List<Player> players)
        {
            if (totalBetMoney != 0)
                return true;

            foreach (Player player in players)
                if (player.Money <= 0)
                    return false;

            return true;
        }

        static int totalBetMoney = 0;
        static void RunRound(List<Player> players)
        {
            // 각 선수가 이전 라운드에서 받은 카드를 클리어한다.
            foreach (Player player in players)
                player.PrepareRound();


            // 선수들이 학교를 간다

            foreach (Player player in players)
            {
                player.Money -= BetMoney;
                totalBetMoney += BetMoney;
            }


            // 딜러가 각 선수들에게 2장씩 카드를 돌린다
            Dealer dealer = new Dealer();
            foreach (Player player in players)
                for (int i = 0; i < StartCards; i++)
                    player.AddCard(dealer.Draw());

            ////플레이어가 첫 번째 카드를 확인한다
            Console.WriteLine($"첫 번째 카드 [{players[0][0]}]");

            Console.WriteLine("두 번째 카드를 상대방과 교환 하시겠습니까?(y/n)");
            //교환할지 선택한다
            if (Console.ReadLine().Equals("y"))
            {
                // 교환하기로 했다면 서로의 2번째 카드를 교환한다
                players[0].SwitchCard(players[1]);
            }

            // 각 선수들의 족보를 계산하고 출력한다.
            for (int i = 0; i < players.Count; i++)
            {
                Player p = players[i];

                p.CalculateScore();
                Console.WriteLine($"P{i} ({p[0]}, {p[1]}) => {p.Score}");
            }


            // 승자와 패자를 가린다.
            Player winner = FindWinner(players);


            //TODO : 승자가 1명 이상이면 베팅 머니를 돌려주고 라운드를 끝낸다.
            // 승자에게 모든 베팅 금액을 준다.

            if (winner != null)
            {
                winner.Money += totalBetMoney;
                totalBetMoney = 0;
            }
            else
                Console.WriteLine("이번판은 나가리");
        }

        private static Player FindWinner(List<Player> players)
        {
             // return players.OrderByDescending(x => x.Score).First();

            if (players[0].Score == players[1].Score)
                return null;
            else
                return (players[0].Score > players[1].Score) ? players[0] : players[1];

        }
    }
}
