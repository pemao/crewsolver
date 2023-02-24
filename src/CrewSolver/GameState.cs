namespace CrewSolver
{
    public class GameState
    {
        /// <summary>
        /// The player hands.
        /// </summary>
        public Dictionary<int, Hand> PlayerHands { get; }

        /// <summary>
        /// List of completed rounds.
        /// </summary>
        public List<Round> RoundsCompleted { get; }

        /// <summary>
        /// Whether or not the game is won (no remaining tasks).
        /// </summary>
        public bool IsWin { get; }

        /// <summary>
        /// Whether or not the game is over (no remaining cards to play).
        /// </summary>
        public bool IsOver { get; }

        /// <summary>
        /// The number of players in the game.
        /// </summary>
        public int PlayerCount { get; }

        /// <summary>
        /// The index of the player who will act first for the next round.
        /// </summary>
        public int FirstPlayerIndex { get; }

        /// <summary>
        /// Constructor for the beginning of a game. Only known information is the player hands.
        /// </summary>
        /// <param name="startingHands"></param>
        public GameState(List<Hand> startingHands)
        {
            this.PlayerHands = new Dictionary<int, Hand>();
            this.RoundsCompleted = new List<Round>();
            this.IsWin = false;
            this.IsOver = false;
            this.PlayerCount = startingHands.Count;

            // TODO move this logic out of the constructor
            // look for the player with the 4 Rocket
            for (int i = 0; i < PlayerCount; i++)
            {
                PlayerHands[i] = startingHands[i];

                if (PlayerHands[i].Cards.Contains(new Card(4, Suit.Rocket)))
                {
                    Console.WriteLine($"Player {i + 1} is first to act (holds 4 Rocket).");
                    this.FirstPlayerIndex = i;
                    break;
                }

                if (i == PlayerCount - 1)
                {
                    Console.WriteLine("Player 1 is first to act (no 4 Rocket found, assuming first player acts first).");
                    this.FirstPlayerIndex = 0;
                }
            }

            // TODO implement validate
        }

        /// <summary>
        /// Constructor for the middle of a game.
        /// </summary>
        /// <param name="playerHands"></param>
        /// <param name="roundsCompleted"></param>
        /// <param name="playerCount"></param>
        /// <param name="firstPlayerIndex"></param>
        [JsonConstructor]
        public GameState(
            Dictionary<int, Hand> playerHands,
            List<Round> roundsCompleted,
            int playerCount,
            int firstPlayerIndex,
            bool isWin,
            bool isOver)
        {
            this.PlayerHands = playerHands;
            this.RoundsCompleted = roundsCompleted;
            this.PlayerCount = playerCount;
            this.FirstPlayerIndex = firstPlayerIndex;
            this.IsWin = isWin;
            this.IsOver = isOver;
        }

        // TODO add levels of verbosity to ToString (i.e to print all completed rounds)
        public override string ToString()
        {
            String toReturn = String.Empty;
            toReturn += $"Players: {PlayerCount}\n";
            toReturn += $"Rounds Completed: {RoundsCompleted.Count}\n";
            toReturn += $"Game Over: {IsOver}\n";
            toReturn += $"Won? {IsWin}\n";
            toReturn += $"First Player: {FirstPlayerIndex}\n";

            foreach ((int playerIndex, Hand hand) in PlayerHands)
            {
                toReturn += $"Player {playerIndex} hand:\n";
                toReturn += $"{hand}\n";
            }

            return toReturn;
        }

        public override bool Equals(object? obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                GameState other = (GameState)obj;

                return (this.PlayerHands.Keys.Count == other.PlayerHands.Keys.Count &&
                    this.PlayerHands.Keys.All(x => other.PlayerHands.ContainsKey(x) && this.PlayerHands[x].Equals(other.PlayerHands[x]))) &&
                    this.RoundsCompleted.Equals(other.RoundsCompleted) &&
                    this.IsWin == other.IsWin &&
                    this.IsOver == other.IsOver &&
                    this.PlayerCount == other.PlayerCount &&
                    this.FirstPlayerIndex == other.FirstPlayerIndex;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                this.PlayerHands,
                this.RoundsCompleted,
                this.IsWin,
                this.IsOver,
                this.PlayerCount,
                this.FirstPlayerIndex);
        }
    }
}
