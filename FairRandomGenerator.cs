using System;
using System.Security.Cryptography;
using System.Text;

public class FairRandomGenerator
{
    private readonly HmacGenerator _hmacGenerator = new();

    public int GenerateFairRandom(int maxValue, out string hmac, out string key)
    {
        // Computer selects random number and HMAC
        int computerNumber = RandomNumberGenerator.GetInt32(0, maxValue + 1);
        key = _hmacGenerator.GenerateKey();
        hmac = _hmacGenerator.CalculateHmac(computerNumber.ToString(), key);

        Console.WriteLine($"I selected a random value in range 0..{maxValue} (HMAC={hmac}).");
        Console.WriteLine("Try to guess my selection:");

        // Show options
        for (int i = 0; i <= maxValue; i++)
        {
            Console.WriteLine($"{i} - {i}");
        }
        Console.WriteLine("X - Exit");
        Console.WriteLine("? - Help");

        // Input loop
        while (true)
        {
            Console.Write("Your selection: ");
            string input = Console.ReadLine()?.Trim() ?? "";

            // Handle Help
            if (input == "?")
            {
                Console.WriteLine($"\nHelp: Select a number between 0-{maxValue} to add to the roll.");
                Console.WriteLine($"The final result will be (your number + computer's number) mod {maxValue + 1}.");
                Console.WriteLine("X - Exit the game\n");
                continue;
            }

            // Handle Exit
            if (input.Equals("X", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Game exited by user.");
                Environment.Exit(0);
            }

            // Parse number
            if (int.TryParse(input, out int userNumber) &&
                userNumber >= 0 &&
                userNumber <= maxValue)
            {
                int result = (computerNumber + userNumber) % (maxValue + 1);
                Console.WriteLine($"My number is {computerNumber} (KEY={key}).");
                Console.WriteLine($"Fair result: {computerNumber} + {userNumber} = {result} (mod {maxValue + 1}).");
                return result;
            }

            Console.WriteLine($"Invalid input. Please enter 0-{maxValue}, 'X', or '?'.");
        }
    }
}