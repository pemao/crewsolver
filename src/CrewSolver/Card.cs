namespace CrewSolver
{
    /// <summary>
    /// Class representation of a card.
    /// </summary>
    public class Card
    {
        public int Value { get; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Suit Suit { get; }

        public Card(int value, Suit suit)
        {
            this.Value = value;
            this.Suit = suit;

            this.Validate();
        }

        public override string ToString()
        {
            return $"{Suit.ToString()}({Value})";
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
                Card other = (Card)obj;
                return this.Value == other.Value && this.Suit == other.Suit;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Suit);
        }

        /// <summary>
        /// Private function to validate a Card. Checks that the card has a value 
        /// between 1-9 for color suits (Blue, Green, Pink, Yellow) and 1-4 for rockets.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void Validate()
        {
            if (Value < 1)
            {
                throw new ArgumentOutOfRangeException("Card value cannot be less than 1.");
            }

            if (Suit == Suit.Rocket)
            {
                if (Value > 4)
                {
                    throw new ArgumentOutOfRangeException("Rocket value must be between 1 and 4.");
                }
            }
            else
            {
                if (Value > 9)
                {
                    throw new ArgumentOutOfRangeException("Color suit value must be between 1 and 9.");
                }
            }
        }
    }
}
