namespace CrewSolverTests
{
    /// <summary>
    /// Tests for the GameState class.
    /// </summary>
    [TestClass]
    public class GameStateTests
    {

        // TODO should add more tests to see if I have full coverage
        [TestMethod]
        public void Equals_SameGameState_True()
        {
            var hands = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() { new Card(1, Suit.Blue)},
                        new List<Task>() { new Task(new Card(1, Suit.Yellow))}
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() { new Card(2, Suit.Blue)},
                        new List<Task>() { new Task(new Card(1, Suit.Blue))}
                    )
                },
            };

            var roundsCompleted = new List<Round>();
            var isWin = false;
            var isOver = false;
            var numPlayers = 2;
            var firstToPlay = 1;

            var gameStateOne = new GameState(
                hands,
                roundsCompleted,
                numPlayers,
                firstToPlay,
                isWin,
                isOver);

            var gameStateTwo = new GameState(
                hands,
                roundsCompleted,
                numPlayers,
                firstToPlay,
                isWin,
                isOver);

            Assert.IsTrue(gameStateOne.Equals(gameStateTwo));
            Assert.IsTrue(gameStateTwo.Equals(gameStateOne));
        }

        [TestMethod]
        public void Equals_DifferentGameState_False()
        {
            var handsOne = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() { new Card(1, Suit.Blue)},
                        new List<Task>() { new Task(new Card(1, Suit.Yellow))}
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() { new Card(2, Suit.Blue)},
                        new List<Task>() { new Task(new Card(1, Suit.Blue))}
                    )
                },
            };

            var handsTwo = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() { new Card(9, Suit.Blue)},
                        new List<Task>() { new Task(new Card(1, Suit.Yellow))}
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() { new Card(2, Suit.Blue)},
                        new List<Task>() { new Task(new Card(1, Suit.Blue))}
                    )
                },
            };

            var roundsCompleted = new List<Round>();
            var isWin = false;
            var isOver = false;
            var numPlayers = 2;
            var firstToPlay = 1;

            var gameStateOne = new GameState(
                handsOne,
                roundsCompleted,
                numPlayers,
                firstToPlay,
                isWin,
                isOver);

            var gameStateTwo = new GameState(
                handsTwo,
                roundsCompleted,
                numPlayers,
                firstToPlay,
                isWin,
                isOver);

            Assert.IsFalse(gameStateOne.Equals(gameStateTwo));
            Assert.IsFalse(gameStateTwo.Equals(gameStateOne));
        }
    }
}