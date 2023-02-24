namespace CrewSolver
{
    /// <summary>
    /// Class representation of a player. A player consists of their
    /// cards and their tasks.
    /// </summary>
    public class Hand
    {
        public List<Card> Cards { get; }
        public List<Task> Tasks { get; }

        public Hand(List<Card> cards, List<Task> tasks)
        {
            if (cards == null)
            {
                throw new ArgumentNullException(nameof(cards));
            }

            if (tasks == null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            this.Cards = cards;
            this.Tasks = tasks;

            this.Validate();
        }

        public override string ToString()
        {
            String toReturn = String.Empty;

            foreach (var card in this.Cards)
            {
                toReturn += card + ", ";
            }

            toReturn += "\n";

            foreach (var task in this.Tasks)
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
                Hand other = (Hand)obj;

                // check that the cards and tasks in each hand are the same
                return this.Cards.All(other.Cards.Contains) && this.Cards.Count == other.Cards.Count &&
                    this.Tasks.All(other.Tasks.Contains) && this.Tasks.Count == other.Tasks.Count;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Cards, this.Tasks);
        }

        /// <summary>
        /// Checks whether the instance of the Hand class is valid. Checks
        /// for duplicate cards and tasks.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private void Validate()
        {
            var cardSet = new HashSet<Card>();
            var taskSet = new HashSet<Task>();

            // TODO throw an aggregate exception reporting all the duplicates

            foreach (var card in this.Cards)
            {
                if (!cardSet.Add(card))
                {
                    throw new ArgumentException($"A hand cannot contain duplicate cards. Duplicate card: {card}");
                }
            }

            foreach (var task in this.Tasks)
            {
                if (!taskSet.Add(task))
                {
                    throw new ArgumentException($"A hand cannot contain duplicate task. Duplicate task: {task}");
                }
            }
        }
    }
}
