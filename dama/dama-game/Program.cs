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
            List<Chessboard> moveListPlayer1 = new List<Chessboard>();
            List<Chessboard> moveListPlayer2 = new List<Chessboard>();

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
                Console.Clear();// <--- screen CLEAR is here!

                Console.WriteLine(b.ToString());

                Console.WriteLine("Turn: {0}", toMove.Name);

                Console.WriteLine("Let move from...");
                Console.Write("V:");

                    char s = Console.ReadKey().KeyChar;
                    xs = b.PositionConverter(s);//addon

                    Console.WriteLine();
                Console.Write("H:");
                    ys = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                    Console.WriteLine();
                    Console.WriteLine("Let move to...");
                Console.Write("V:");
                //xe = int.Parse(Console.ReadLine());
                    char s2 = Console.ReadKey().KeyChar;
                    xe = b.PositionConverter(s2);//addon

                    Console.WriteLine();
                Console.Write("H:");
                    ye = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                    Console.WriteLine();




                    b.MovePawn(toMove, xs, ys, xe, ye);

                    if (toMove == p1)
                    {
                        moveListPlayer1.Add(b);//salvo mosse
                        toMove = p2;
                    }
                    else
                    {
                        moveListPlayer2.Add(b);//salvo mosse
                        toMove = p1;
                    }
                    //<--------ESITO MOSSA OK
                }
                catch(Exception e){
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
