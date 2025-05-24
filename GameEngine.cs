using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

public class GameEngine
{
    private readonly List<Dice> _dice;
    private readonly FairRandomGenerator _randomGenerator;
    private readonly ProbabilityTableRenderer _tableRenderer;

    public GameEngine(List<Dice> dice)
    {
        _dice = dice;
        _randomGenerator = new FairRandomGenerator();
        _tableRenderer = new ProbabilityTableRenderer();
    }

    public void StartGame()
    {
        Console.WriteLine("=== Non-Transitive Dice Game ===");
        bool userGoesFirst = DetermineFirstMove();

        Dice userDie = SelectDice(userGoesFirst, isUser: true);
        Dice computerDie = SelectDice(!userGoesFirst, isUser: false, excludedDie: userGoesFirst ? null : userDie);

        int userRoll = RollDice(userDie, "Your");
        int computerRoll = RollDice(computerDie, "Computer's");

        Console.WriteLine($"\nYour roll: {userRoll}");
        Console.WriteLine($"Computer's roll: {computerRoll}");
        DetermineWinner(userRoll, computerRoll);
    }

    private bool DetermineFirstMove()
    {
        Console.WriteLine("\nDetermining who goes first...");
        int result = _randomGenerator.GenerateFairRandom(1, out _, out _);
        return result == 0;
    }

    private Dice SelectDice(bool isUserTurn, bool isUser, Dice? excludedDie = null)
    {
        var availableDice = _dice.Where(d => excludedDie == null || d != excludedDie).ToList();

        if (!isUser)
        {
            int index = RandomNumberGenerator.GetInt32(0, availableDice.Count);
            var selected = availableDice[index];
            // Show which die number (index) the computer selected from all dice
            int originalIndex = _dice.IndexOf(selected);
            Console.WriteLine($"\nComputer selected: [{selected.Name}] (Die {originalIndex})");
            return selected;
        }

        Console.WriteLine("\nAvailable dice:");
        for (int i = 0; i < availableDice.Count; i++)
        {
            Console.WriteLine($"{i} - [{availableDice[i].Name}]");
        }

        Console.WriteLine("? - Show probabilities");
        Console.Write("Select a die: ");

        while (true)
        {
            string input = Console.ReadLine()!;
            if (input == "?")
            {
                _tableRenderer.ShowTable(_dice);
                Console.Write("Select a die: ");
                continue;
            }
            if (int.TryParse(input, out int choice) && choice >= 0 && choice < availableDice.Count)
            {
                return availableDice[choice];
            }
            Console.Write("Invalid selection. Try again: ");
        }
    }

    private int RollDice(Dice die, string playerPrefix)
    {
        Console.WriteLine($"\nIt's time for {(playerPrefix == "Your" ? "your" : "my")} roll.");
        int faceIndex = _randomGenerator.GenerateFairRandom(die.Faces.Length - 1, out _, out _);
        int result = die.Roll(faceIndex);
        Console.WriteLine($"{playerPrefix} roll result is {result}.");
        return result;
    }

    private void DetermineWinner(int userRoll, int computerRoll)
    {
        Console.WriteLine("\n=== Final Result ===");
        Console.WriteLine($"Your roll: {userRoll}");
        Console.WriteLine($"Computer's roll: {computerRoll}");

        if (userRoll > computerRoll)
        {
            Console.WriteLine($"You win ({userRoll} > {computerRoll})!");
        }
        else if (computerRoll > userRoll)
        {
            Console.WriteLine($"Computer wins ({computerRoll} > {userRoll})!");
        }
        else
        {
            Console.WriteLine($"It's a tie ({userRoll} = {computerRoll})!");
        }
    }
}