Console.WriteLine("===========");
Console.WriteLine("CREW SOLVER");
Console.WriteLine("===========");

if (args.Length > 0)
{
    var fileName = args[0];

    System.Console.WriteLine($"Reading input GameState from: {Directory.GetCurrentDirectory()}\\{fileName}");

    var jsonString = File.ReadAllText(fileName);
    GameState startGameState = JsonSerializer.Deserialize<GameState>(jsonString)!;

    Console.WriteLine();
    Console.WriteLine("Starting Game State:");
    Console.WriteLine(startGameState);
    Console.WriteLine();

    Console.WriteLine("Running Solver...");

    (bool isWinnable, GameState endGameState) = Solver.PlayRound(startGameState);

    if (isWinnable)
    {
        Console.WriteLine("GameState is winnable! Here's a solution:");

        for (int i = 0; i < endGameState.RoundsCompleted.Count; i++)
        {
            Console.WriteLine($"Round {i + 1}:");
            Console.WriteLine(endGameState.RoundsCompleted[i].ToString());
            Console.WriteLine();
        }
    }
    else
    {
        Console.WriteLine("GameState not winnable :(");
    }
}
else
{
    Console.WriteLine("No input file found.");
}
