using ConsoleTables;

public class ProbabilityTableRenderer
{
    private readonly ProbabilityCalculator _calculator = new();

    public void ShowTable(List<Dice> dice)
    {
        var table = new ConsoleTable(new[] { "User Dice \\ Computer Dice" }.Concat(dice.Select(d => d.Name)).ToArray());

        foreach (var userDie in dice)
        {
            var row = new List<object> { userDie.Name };

            foreach (var computerDie in dice)
            {
                if (userDie == computerDie)
                    row.Add("-");
                else
                    row.Add(_calculator.CalculateWinProbability(userDie, computerDie).ToString("0.0000"));
            }

            table.AddRow(row.ToArray());
        }

        Console.WriteLine("Probability of User Winning:");
        table.Write(Format.Alternative);
    }
}