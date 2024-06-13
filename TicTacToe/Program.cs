// See https://aka.ms/new-console-template for more information
using System;

public class Position
{
    public int Number { get; set; }
    public char Marker { get; set; }

    public Position(int number)
    {
        Number = number;
        Marker = ' ';
    }
}

public class Player
{
    public string Name { get; set; }
    public char Marker { get; set; }

    public Player(string name, char marker)
    {
        Name = name;
        Marker = marker;
    }
}

public class GameBoard
{
    public Position[] Positions { get; set; }

    public GameBoard()
    {
        Positions = new Position[9];
        for (int i = 0; i < 9; i++)
        {
            Positions[i] = new Position(i + 1);
        }
    }

    public void Display()
    {
        for (int i = 0; i < 9; i++)
        {
            if (i % 3 == 0) Console.WriteLine();
            Console.Write($"|{(Positions[i].Marker == ' ' ? Positions[i].Number.ToString() : Positions[i].Marker.ToString())}|");
        }
        Console.WriteLine();
    }
}

public class Game
{
    private GameBoard gameBoard;
    private Player player1;
    private Player player2;
    private Player currentPlayer;

    public Game(Player player1, Player player2)
    {
        gameBoard = new GameBoard();
        this.player1 = player1;
        this.player2 = player2;
        currentPlayer = player1; // Player 1 starts
    }

    public void Play()
    {
        bool gameWon = false;
        while (!gameWon && !IsDraw())
        {
            gameBoard.Display();
            Console.WriteLine($"{currentPlayer.Name}'s turn. Enter a position (1-9): ");
            int position;
            while (!int.TryParse(Console.ReadLine(), out position) || position < 1 || position > 9 || gameBoard.Positions[position - 1].Marker != ' ')
            {
                Console.WriteLine("Invalid position. Try again.");
            }

            gameBoard.Positions[position - 1].Marker = currentPlayer.Marker;

            if (CheckForWinner())
            {
                gameBoard.Display();
                Console.WriteLine($"{currentPlayer.Name} wins!");
                gameWon = true;
            }
            else
            {
                currentPlayer = currentPlayer == player1 ? player2 : player1;
            }
        }

        if (!gameWon)
        {
            gameBoard.Display();
            Console.WriteLine("It's a draw!");
        }
    }

    private bool IsDraw()
    {
        foreach (var position in gameBoard.Positions)
        {
            if (position.Marker == ' ') return false;
        }
        return true;
    }

    private bool CheckForWinner()
    {
        int[][] winningCombinations = new int[][]
        {
            new int[] { 0, 1, 2 },
            new int[] { 3, 4, 5 },
            new int[] { 6, 7, 8 },
            new int[] { 0, 3, 6 },
            new int[] { 1, 4, 7 },
            new int[] { 2, 5, 8 },
            new int[] { 0, 4, 8 },
            new int[] { 2, 4, 6 }
        };

        foreach (var combination in winningCombinations)
        {
            if (gameBoard.Positions[combination[0]].Marker == currentPlayer.Marker &&
                gameBoard.Positions[combination[1]].Marker == currentPlayer.Marker &&
                gameBoard.Positions[combination[2]].Marker == currentPlayer.Marker)
            {
                return true;
            }
        }

        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Player player1 = new Player("Player 1", 'X');
        Player player2 = new Player("Player 2", 'O');

        Game game = new Game(player1, player2);
        game.Play();
    }
}

