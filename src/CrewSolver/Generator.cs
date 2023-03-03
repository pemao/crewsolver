namespace CrewSolver
{
    /// <summary>
    /// Generate random hands for testing.
    /// </summary>
    internal class Generator
    {
        public static void GenerateRandomGameStates()
        {
            // Create folder for test jsons.
            var path = "../CrewSolverTests/TestGameStates";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // For 1 to 10 tasks.
            for (int i = 1; i <= 10; i++)
            {
                // For 3 to 5 players.
                for (int j = 4; j <= 4; j++)
                {
                    Console.WriteLine($"Generating {j} players, {i} tasks");

                    var hands = Generator.GenerateHands(j, i);
                    var initialGameState = new GameState(hands);

                    var options = new JsonSerializerOptions()
                    {
                        WriteIndented = true,
                    };
                    var jsonString = JsonSerializer.Serialize(initialGameState, options);

                    File.WriteAllText($"{path}/Random_{j}Players_{i}Tasks.json", jsonString);
                }
            }
        }

        /// <summary>
        /// Given a number of players and number of tasks, generate a random list of hands.
        /// </summary>
        /// <param name="numPlayers"></param>
        /// <param name="numTasks"></param>
        /// <returns></returns>
        public static List<Hand> GenerateHands(int numPlayers, int numTasks)
        {
            var allCards = new List<Card>();
            var rng = new Random();
            var playerCards = new List<List<Card>>();
            var playerTasks = new List<List<Task>>();
            var hands = new List<Hand>();
            var firstPlayerIndex = 0;

            // initialize list of player cards, player tasks
            for (int i = 0; i < numPlayers; i++)
            {
                playerCards.Add(new List<Card>());
                playerTasks.Add(new List<Task>());
            }

            // generate all the cards in the deck
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                var maxVal = suit == Suit.Rocket ? 4 : 9;
                for (int i = 1; i <= maxVal; i++)
                {
                    allCards.Add(new Card(i, suit));
                }
            }

            // randomize the order of the cards
            for (int i = allCards.Count - 1; i >= 0; i--)
            {
                int j = rng.Next(i + 1);
                var temp = allCards[j];
                allCards[j] = allCards[i];
                allCards[i] = temp;
            }

            // split the cards among players, record which player has 4 rocket
            for (int i = 0; i < allCards.Count; i++)
            {
                var curPlayer = i % numPlayers;
                playerCards[curPlayer].Add(allCards[i]);

                if (allCards[i] == new Card(4, Suit.Rocket))
                {
                    firstPlayerIndex = curPlayer;
                }
            }

            // assign tasks in order (first player gets task first)
            var allCardsNoRockets = allCards.Where(x => x.Suit != Suit.Rocket).ToList();
            var taskNumber = 0;
            var tasksSet = new HashSet<int>();
            while (taskNumber < numTasks)
            {
                var curPlayer = firstPlayerIndex + taskNumber % numPlayers;
                var rand = rng.Next(allCardsNoRockets.Count);

                while (tasksSet.Contains(rand))
                {
                    rand = rng.Next(allCardsNoRockets.Count);
                }

                tasksSet.Add(rand);
                playerTasks[curPlayer].Add(new Task(allCardsNoRockets[rand]));
                taskNumber++;
            }

            // create hands
            for (int i = 0; i < numPlayers; i++)
            {
                hands.Add(new Hand(playerCards[i], playerTasks[i]));
            }

            return hands;
        }
    }
}
