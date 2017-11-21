using System;
using System.Collections.Generic;

namespace dama_game
{
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Player("Matteo");
            var p2 = new PlayerMediumComputer("CPU1");
            IScoreObserver observerScore1 = new Player1Scorer();
            IScoreObserver observerScore2 = new Player2Scorer();
            IScoreObserved tableScore = new Chessboard();
            //raccoglitore mosse
            List<Chessboard> movePlayer1 = new List<Chessboard>();
            List<Chessboard> movePlayer2 = new List<Chessboard>();
            //int scorePlayer1 = 0;
            //int scorePlayer2 = 0;

            
            IPlayer toMove = p1;
            tableScore.RegisterObserver(observerScore1);
            tableScore.RegisterObserver(observerScore2);


            int xs, ys, xe, ye;
            var b = new Chessboard(p1, p2);
            do
            {
                try{
                    Console.Clear();
                Console.WriteLine(b.ToString());

                Console.WriteLine("Turn: {0}", toMove.Name);

                Console.WriteLine("Let move from...");
                Console.Write("V:");
                xs = int.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.Write("H:");
                ys = int.Parse(Console.ReadLine());

                Console.WriteLine("Let move to...");
                Console.Write("V:");
                xe = int.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.Write("H:");
                ye = int.Parse(Console.ReadLine());

                b.MovePawn(toMove, xs, ys, xe, ye);

                if (toMove == p1)
                    toMove = p2;
                else
                    toMove = p1;
                }catch(Exception e){
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press to continue...");
                    Console.ReadLine();
                }

            } while (b.Winner == null);

            Console.WriteLine("Press to simulate legal move (5,1) -> (4,2)...");
            Console.ReadLine();

            b.MovePawn(p2, 5, 1, 4, 2);

            Console.WriteLine(b.ToString());

            Console.WriteLine("Press to simulate play...");
            Console.ReadLine();

            b.SimulatePlay();

            Console.WriteLine("Winner is {0} with score {1}", b.Winner.Name, b.Winner.Score);
            Console.WriteLine("Loser is {0} with score {1}", b.Loser.Name, b.Loser.Score);
        }
    }
}
