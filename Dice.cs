public class Dice
{
    public int[] Faces { get; }
    public string Name => string.Join(",", Faces);

    public Dice(int[] faces)
    {
        Faces = faces;
    }

    public int Roll(int faceIndex) => Faces[faceIndex];
}