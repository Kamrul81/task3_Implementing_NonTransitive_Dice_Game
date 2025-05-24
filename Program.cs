try
{
    var parser = new DiceParser();
    var dice = parser.ParseDice(args);

    var game = new GameEngine(dice);
    game.StartGame();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    Console.WriteLine("Example usage: NonTransitiveDiceGame.exe 1,2,3,4,5,6 2,2,4,4,9,9 3,3,5,5,7,7");
    //hello checking push...
}