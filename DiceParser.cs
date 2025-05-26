public class DiceParser
{
    public List<Dice> ParseDice(string[] args)
    {
        if (args.Length < 3)
            throw new ArgumentException($"Given {args.Length} dice. At least 3 dice required. Example: 1,2,3 4,5,6 7,8,9");

        int expectedFaceCount = -1;
        var diceList = new List<Dice>();

        foreach (var arg in args)
        {
            string[] faceStrings = arg.Split(',');

            // Set expected face count from first die
            if (expectedFaceCount == -1)
            {
                expectedFaceCount = faceStrings.Length;
                if (expectedFaceCount < 2)
                    throw new ArgumentException($"Dice must have ≥2 faces. Invalid: {arg}");
            }

            // Validate face count consistency
            if (faceStrings.Length != expectedFaceCount)
                throw new ArgumentException(
                    $"All dice must have {expectedFaceCount} faces. Invalid: {arg}");

            // Parse faces
            int[] faces = new int[expectedFaceCount];
            for (int i = 0; i < expectedFaceCount; i++)
            {
                if (!int.TryParse(faceStrings[i], out faces[i]))
                    throw new ArgumentException($"Invalid face value: {faceStrings[i]}");
            }

            diceList.Add(new Dice(faces));
        }

        return diceList;
    }
}