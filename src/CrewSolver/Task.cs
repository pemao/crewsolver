namespace CrewSolver
{
    public class Task
    {
        public Card Card { get; }

        public Task(Card card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            this.Card = card;

            this.Validate();
        }

        public override string ToString()
        {
            return $"Task({Card})";
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
                Task other = (Task)obj;
                return this.Card.Equals(other.Card);
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Card);
        }

        private void Validate()
        {
            // TODO: remove this when we consider the rounds that do not have any tasks?
            if (Card == null)
            {
                throw new ArgumentNullException("Task card cannot be null.");
            }

            if (Card.Suit == Suit.Rocket)
            {
                throw new ArgumentException("Task card cannot be a rocket card.");
            }
        }
    }
}
