public class DiceParser
{
    public List<Dice> ParseDice(string[] args)
    {
        if (args.Length < 3)
            throw new ArgumentException("At least 3 dice configurations are required.");

        var diceList = new List<Dice>();

        foreach (var arg in args)
        {
            var faceStrings = arg.Split(',');
            var faces = new int[faceStrings.Length];

            for (int i = 0; i < faceStrings.Length; i++)
            {
                if (!int.TryParse(faceStrings[i], out faces[i]))
                    throw new ArgumentException($"Invalid face value: {faceStrings[i]}");
            }

            diceList.Add(new Dice(faces));
        }

        return diceList;
    }
}