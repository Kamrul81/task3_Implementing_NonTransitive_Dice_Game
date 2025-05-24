public class ProbabilityCalculator
{
    public double CalculateWinProbability(Dice dice1, Dice dice2)
    {
        int wins = 0;
        int total = dice1.Faces.Length * dice2.Faces.Length;

        foreach (var face1 in dice1.Faces)
        {
            foreach (var face2 in dice2.Faces)
            {
                if (face1 > face2) wins++;
            }
        }

        return (double)wins / total;
    }
}