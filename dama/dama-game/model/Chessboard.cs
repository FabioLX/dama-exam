using System;
using System.Text;

public sealed class Chessboard
{
    private readonly IPlayer player1;
    private readonly IPlayer player2;
    public IPlayer Winner { get; private set; }
    public IPlayer Loser { get; private set; }
    protected int player1Score = 0;
    protected int player2Score = 0;

    private Box[,] boxes;

    public Chessboard(IPlayer p1, IPlayer p2)
    {
        this.player1 = p1;
        this.player2 = p2;

        this.boxes = new Box[8, 8];
        IPlayer pBox = null;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (x < 3 && FillablePosition(x, y))
                    pBox = p1;
                else if (x > 4 && FillablePosition(x, y))
                    pBox = p2;
                else
                    pBox = null;

                boxes[x, y] = new Box(x, y, pBox);
            }
        }
    }

    public void SimulatePlay()
    {
        Random rnd = new Random();
        int winnerScore = 0;
        if (rnd.Next(0, 2) == 0)
        {
            Winner = player1;
            Loser = player2;

            winnerScore = player1Score = 12;
        }
        else
        {
            Winner = player2;
            Loser = player1;

            winnerScore = player2Score = 12;
        }

        Winner.Winner();
        Loser.Loser();

        Winner.AddScore(winnerScore, Loser);
    }

    public override string ToString()
    {
        var stb = new StringBuilder();
        stb.AppendLine("  0 1 2 3 4 5 6 7");
        //TODO aggiungere LETTERE
        for (int x = 0; x < 8; x++)
        {
            stb.Append(x);
            for (int y = 0; y < 8; y++)
            {
                stb.Append("|" + boxes[x, y].ToString());
            }
            stb.AppendLine("|");
        }

        return stb.ToString();
    }

    bool FillablePosition(int x, int y)
    {
        return ((x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0));
    }

    public void MovePawn(IPlayer p, int xStart, int yStart, int xEnd, int yEnd)
    {
        Box jumpedBox = null;
        if(xEnd < 0 || xEnd > 7 || yEnd < 0 || yEnd > 7)
        {
            throw new MoveException("Illegal move outside chessboard.");
        }

        var startBox = boxes[xStart, yStart];
        if(startBox.IsFree() || !startBox.Owner.Equals(p))
            throw new MoveException("Empty box or it is not your.");

        var arrivalBox = boxes[xEnd, yEnd];

        if (Math.Abs(xStart - xEnd) == 2 && Math.Abs(yStart - yEnd) == 2)
        {
            //It is a jump
            int xInter, yInter;
            if (yStart > yEnd)
            {
                yInter = yEnd + 1;
            }
            else
            {
                yInter = yEnd - 1;
            }

            if (xStart > xEnd)
            {
                xInter = xEnd + 1;
            }
            else
            {
                xInter = xEnd - 1;
            }

            jumpedBox = boxes[xInter, yInter];

        }
        else if (Math.Abs(xStart - xEnd) > 2 || Math.Abs(yStart - yEnd) > 2 ||
               Math.Abs(xStart - xEnd) == 0 || Math.Abs(yStart - yEnd) == 0)
        {
            throw new MoveException("Illegal move.");
        }


        if (arrivalBox.IsFree())
        {
            //occupa box di arrivo
            arrivalBox.Owner = p;
            //svuota box partenza
            startBox.Free();

            //se c'e' salto
            if (jumpedBox != null)
            {
                jumpedBox.Free();
                if (p == player1)
                {
                    player1Score++;
                    //TODO scatta salovataggio movimento BOX - observer
                }
                else
                {
                    player2Score++;
                    //TODO scatta salovataggio movimento BOX - observer
                }
            }
        }
        else if (arrivalBox.Owner.Equals(p))
        {
            throw new MoveException("Box own this player");
        }
        else
        {
            throw new MoveException("Box is owned by enemy.");
        }


       
    }
    //ritorno punteggio
    public int GetScorePlayer(int playerNumber)
    {
        if (playerNumber == 1)
        {
            return player1Score;
        }
        else
        { return player2Score; }


    }

    

    public class Box
    {
        public Box(int x, int y, IPlayer p = null)
        {
            horizontal = y;
            vertical = x;
            Owner = p;
        }
        public IPlayer Owner { get; set; }
        public int vertical;//1-8

        public int horizontal;//A-H
        //TODO metodo conversione Lettere/numeri

        public bool IsFree()
        {
            return Owner == null;
        }

        public void Free()
        {
            Owner = null;
        }

        public override string ToString()
        {
            if (IsFree())
            {
                return " ";
            }
            else return Owner.ToString();
        }
    }

}