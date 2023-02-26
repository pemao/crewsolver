namespace CrewSolver
{
    internal class Round
    {
        public int FirstPlayerIndex { get; }
        public int WinningPlayerIndex { get; }
        public List<Card> CardsPlayed { get; }
        public List<Task> TasksCompleted { get; }

        public Round(int firstPlayerIndex, int winningPlayerIndex, List<Card> cardsPlayed, List<Task> tasksCompleted)
        {
            this.FirstPlayerIndex = firstPlayerIndex;
            this.WinningPlayerIndex = winningPlayerIndex;
            this.CardsPlayed = cardsPlayed;
            this.TasksCompleted = tasksCompleted;

            // TODO validate? validate that completed tasks are in the cards played?
        }

        public override string ToString()
        {
            String toReturn = String.Empty;

            toReturn += $"First Player: {FirstPlayerIndex}\nCards Played:";

            foreach (var card in this.CardsPlayed)
            {
                toReturn += card + ", ";
            }

            toReturn += $"\nWinning Player: {WinningPlayerIndex}\nTasks Completed:";

            foreach (var task in this.TasksCompleted)
            {
                toReturn += task + ", ";
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
                Round other = (Round)obj;

                // check that the cards and tasks in each hand are the same
                return this.FirstPlayerIndex == other.FirstPlayerIndex &&
                    this.WinningPlayerIndex == other.WinningPlayerIndex &&
                    this.CardsPlayed.SequenceEqual(other.CardsPlayed) &&
                    this.TasksCompleted.All(other.TasksCompleted.Contains) && this.TasksCompleted.Count == other.TasksCompleted.Count;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstPlayerIndex, WinningPlayerIndex, CardsPlayed, TasksCompleted);
        }
    }
}
