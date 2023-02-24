namespace CrewSolverTests
{
    /// <summary>
    /// Tests for the Round class.
    /// </summary>
    [TestClass]
    public class RoundTests
    {
        [TestMethod]
        public void Equals_SameCardsPlayedNoTasksCompleted_True()
        {
            var firstToPlay = 1;
            var winningPlayer = 1;

            var cardsPlayed = new List<Card>()
            {
                new Card(9, Suit.Blue),
                new Card(7, Suit.Blue)
            };

            var tasksCompleted = new List<Task>();

            var roundOne = new Round(firstToPlay, winningPlayer, cardsPlayed, tasksCompleted);
            var roundTwo = new Round(firstToPlay, winningPlayer, cardsPlayed, tasksCompleted);

            Assert.IsTrue(roundOne.Equals(roundTwo));
            Assert.IsTrue(roundTwo.Equals(roundOne));
        }

        [TestMethod]
        public void Equals_SameCardsPlayedTasksCompleted_True()
        {
            var firstToPlay = 1;
            var winningPlayer = 1;

            var cardsPlayed = new List<Card>()
            {
                new Card(9, Suit.Blue),
                new Card(7, Suit.Blue)
            };

            var tasksCompletedOne = new List<Task>()
            {
                new Task(new Card(9, Suit.Blue)),
                new Task(new Card(7, Suit.Blue))

            };
            var tasksCompletedTwo = new List<Task>()
            {
                new Task(new Card(7, Suit.Blue)),
                new Task(new Card(9, Suit.Blue)),
            };

            var roundOne = new Round(firstToPlay, winningPlayer, cardsPlayed, tasksCompletedOne);
            var roundTwo = new Round(firstToPlay, winningPlayer, cardsPlayed, tasksCompletedTwo);

            Assert.IsTrue(roundOne.Equals(roundTwo));
            Assert.IsTrue(roundTwo.Equals(roundOne));
        }

        [TestMethod]
        public void Equals_DifferentFirstToPlay_False()
        {
            var firstToPlayOne = 1;
            var firstToPlayTwo = 2;
            var winningPlayer = 1;

            var cardsPlayed = new List<Card>()
            {
                new Card(9, Suit.Blue),
                new Card(7, Suit.Blue)
            };

            var tasksCompleted = new List<Task>();

            var roundOne = new Round(firstToPlayOne, winningPlayer, cardsPlayed, tasksCompleted);
            var roundTwo = new Round(firstToPlayTwo, winningPlayer, cardsPlayed, tasksCompleted);

            Assert.IsFalse(roundOne.Equals(roundTwo));
            Assert.IsFalse(roundTwo.Equals(roundOne));
        }

        [TestMethod]
        public void Equals_DifferentWinningPlayer_False()
        {
            var firstToPlay = 1;
            var winningPlayerOne = 1;
            var winningPlayerTwo = 2;

            var cardsPlayed = new List<Card>()
            {
                new Card(9, Suit.Blue),
                new Card(7, Suit.Blue)
            };

            var tasksCompleted = new List<Task>();

            var roundOne = new Round(firstToPlay, winningPlayerOne, cardsPlayed, tasksCompleted);
            var roundTwo = new Round(firstToPlay, winningPlayerTwo, cardsPlayed, tasksCompleted);

            Assert.IsFalse(roundOne.Equals(roundTwo));
            Assert.IsFalse(roundTwo.Equals(roundOne));
        }

        [TestMethod]
        public void Equals_DifferentTasksCompleted_False()
        {
            var firstToPlay = 1;
            var winningPlayerOne = 1;
            var winningPlayerTwo = 2;

            var cardsPlayed = new List<Card>()
            {
                new Card(9, Suit.Blue),
                new Card(7, Suit.Blue)
            };

            var tasksCompletedOne = new List<Task>();
            var tasksCompletedTwo = new List<Task>()
            {
                new Task(new Card(7, Suit.Blue))
            };

            var roundOne = new Round(firstToPlay, winningPlayerOne, cardsPlayed, tasksCompletedOne);
            var roundTwo = new Round(firstToPlay, winningPlayerTwo, cardsPlayed, tasksCompletedTwo);

            Assert.IsFalse(roundOne.Equals(roundTwo));
            Assert.IsFalse(roundTwo.Equals(roundOne));
        }

        [TestMethod]
        public void Equals_DifferentCardsPlayed_False()
        {
            var firstToPlay = 1;
            var winningPlayer = 1;

            var cardsPlayedOne = new List<Card>()
            {
                new Card(9, Suit.Blue),
                new Card(7, Suit.Blue)
            };

            var cardsPlayedTwo = new List<Card>()
            {
                new Card(9, Suit.Blue),
                new Card(7, Suit.Green)
            };

            var tasksCompleted = new List<Task>();

            var roundOne = new Round(firstToPlay, winningPlayer, cardsPlayedOne, tasksCompleted);
            var roundTwo = new Round(firstToPlay, winningPlayer, cardsPlayedTwo, tasksCompleted);

            Assert.IsFalse(roundOne.Equals(roundTwo));
            Assert.IsFalse(roundTwo.Equals(roundOne));
        }
    }
}
