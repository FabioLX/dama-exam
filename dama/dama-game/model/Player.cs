using System;

public interface IPlayer
{
    string Name { get; }

    int Win { get; }
    int Lose { get; }
    int Par { get; }

    int Score { get; }

    int Difficulty { get; }

    void Winner();
    void Loser();
    void Draw();

    void AddScore(int points, IPlayer enemy);
}

public class Player : IPlayer
{
    private readonly string name;
    private int win = 0;
    private int lose = 0;
    private int par = 0;
    private int score = 0;
    protected int difficulty = 1;

    public Player(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Must have a value", nameof(name));
        }

        this.name = name;
    }

    public string Name => name;

    public int Win => win;

    public int Lose => lose;

    public int Par => par;

    public int Score => score;

    public int Difficulty => difficulty;

    public void AddScore(int points, IPlayer enemy)
    {
        if(points < 1){
            throw new ArgumentException("Must be grater then zero", nameof(points));
        }

        score += points * enemy.Difficulty;
    }

    public void Winner()
    {
        win++;
    }

    public void Loser()
    {
        lose++;
    }

    public void Draw()
    {
        par++;
    }

    public override string ToString()
    {
        return Name[0].ToString().ToUpper();
    }

  
}

public sealed class PlayerEasyComputer : Player
{
    public PlayerEasyComputer(string name) : base(name)
    {
        difficulty = 1;
    }
}

public sealed class PlayerMediumComputer : Player
{
    public PlayerMediumComputer(string name) : base(name)
    {
        difficulty = 2;
    }
}

public sealed class PlayerHardComputer : Player
{
    public PlayerHardComputer(string name) : base(name)
    {
        difficulty = 3;
    }
}

class Player1Scorer : IScoreObserver
{
    int score = 0;
    public void NotifyScore(int score)
    {
        score++;
        Console.WriteLine("Score player1: "+ score);
    }
}

class Player2Scorer : IScoreObserver
{
    int score = 0;
    public void NotifyScore(int score)
    {
        score++;
        Console.WriteLine("Score player2: " + score);
    }
}