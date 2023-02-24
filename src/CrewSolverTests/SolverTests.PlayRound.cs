namespace CrewSolverTests
{
    /// <summary>
    /// Tests for the Solver class PlayRound method.
    /// </summary>
    public partial class SolverTests
    {
        /// <summary>
        /// Win the game in just a single round. Player 0 plays the 9 blue, completes the only remaining task.
        /// </summary>
        [TestMethod]
        public void PlayRound_SingleRound_ReturnsWin()
        {
            var playerHands = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() { new Card(9, Suit.Blue), new Card(1, Suit.Pink)},
                        new List<Task>() { new Task(new Card(9, Suit.Blue)) }
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() { new Card(2, Suit.Blue), new Card(2, Suit.Pink)},
                        new List<Task>() {}
                    )
                },
            };

            var gameStateOne = new GameState(
                playerHands: playerHands,
                roundsCompleted: new List<Round>(),
                playerCount: 2,
                firstPlayerIndex: 0,
                isWin: false,
                isOver: false);

            (bool isWin, GameState endGameState) = Solver.PlayRound(gameStateOne);

            // Game metadata is correct
            Assert.IsTrue(isWin);
            Assert.IsTrue(endGameState.IsWin);
            Assert.IsTrue(endGameState.IsOver);
            Assert.IsTrue(endGameState.PlayerCount == 2);

            // Completed rounds was upated correctly
            Assert.IsTrue(endGameState.RoundsCompleted.Count == 1);
            Assert.IsTrue(endGameState.RoundsCompleted[0].Equals(
                new Round(
                    firstPlayerIndex: 0,
                    winningPlayerIndex: 0,
                    cardsPlayed: new List<Card>()
                    {
                        new Card(9, Suit.Blue),
                        new Card(2, Suit.Blue)
                    },
                    tasksCompleted: new List<Task>()
                    {
                        new Task(new Card(9, Suit.Blue)),
                    }
                    )
                ));
        }

        /// <summary>
        /// Single round returns loss. Player 0 must complete 9B task, Player 1 must complete 1B task - impossible.
        /// </summary>
        [TestMethod]
        public void PlayRound_TwoRound_ReturnsLose()
        {
            var playerHands = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() { new Card(9, Suit.Blue)},
                        new List<Task>() { new Task(new Card(9, Suit.Blue)) }
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() { new Card(2, Suit.Blue)},
                        new List<Task>() { new Task(new Card(2, Suit.Blue)) }
                    )
                },
            };

            var gameStateOne = new GameState(
                playerHands: playerHands,
                roundsCompleted: new List<Round>(),
                playerCount: 2,
                firstPlayerIndex: 0,
                isWin: false,
                isOver: false);

            (bool isWin, GameState endGameState) = Solver.PlayRound(gameStateOne);

            // Game metadata is correct
            Assert.IsFalse(isWin);
        }

        [TestMethod]
        public void PlayRound_TwoRounds_ReturnsWin()
        {
            var playerHands = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() { new Card(9, Suit.Blue), new Card(1, Suit.Pink)},
                        new List<Task>() { new Task(new Card(9, Suit.Blue)) }
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() { new Card(2, Suit.Blue), new Card(2, Suit.Pink)},
                        new List<Task>() { new Task(new Card(2, Suit.Pink)) }
                    )
                },
            };

            var gameStateOne = new GameState(
                playerHands: playerHands,
                roundsCompleted: new List<Round>(),
                playerCount: 2,
                firstPlayerIndex: 0,
                isWin: false,
                isOver: false);

            (bool isWin, GameState endGameState) = Solver.PlayRound(gameStateOne);

            // Game metadata is correct
            Assert.IsTrue(isWin);
            Assert.IsTrue(endGameState.RoundsCompleted.Count == 2);
        }


        /// <summary>
        /// Two round returns loss. Player 0 must complete 9B task, Player 1 must complete 1B task - impossible.
        /// </summary>
        [TestMethod]
        public void PlayRound_TwoRounds_ReturnsLose()
        {
            var playerHands = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() { new Card(9, Suit.Blue), new Card(1, Suit.Pink)},
                        new List<Task>() { new Task(new Card(9, Suit.Blue)) }
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() { new Card(2, Suit.Blue), new Card(2, Suit.Pink)},
                        new List<Task>() { new Task(new Card(2, Suit.Blue)) }
                    )
                },
            };

            var gameStateOne = new GameState(
                playerHands: playerHands,
                roundsCompleted: new List<Round>(),
                playerCount: 2,
                firstPlayerIndex: 0,
                isWin: false,
                isOver: false);

            (bool isWin, GameState endGameState) = Solver.PlayRound(gameStateOne);

            // Game metadata is correct
            Assert.IsFalse(isWin);
        }


        [TestMethod]
        public void PlayRound_MultiRoundsOneTaskPerRound_ReturnsWin()
        {
            var playerHands = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() {
                            new Card(9, Suit.Blue),
                            new Card(1, Suit.Pink),
                            new Card(5, Suit.Pink),
                            new Card(6, Suit.Yellow),
                        },
                        new List<Task>()
                        {
                            new Task(new Card(9, Suit.Blue)),
                            new Task(new Card(5, Suit.Pink)),
                            new Task(new Card(8, Suit.Green)),
                        }
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() {
                            new Card(2, Suit.Blue),
                            new Card(2, Suit.Pink),
                            new Card(3, Suit.Pink),
                            new Card(8, Suit.Green),
                        },
                        new List<Task>()
                        {
                            new Task(new Card(2, Suit.Pink))
                        }
                    )
                },
            };

            var gameStateOne = new GameState(
                playerHands: playerHands,
                roundsCompleted: new List<Round>(),
                playerCount: 2,
                firstPlayerIndex: 0,
                isWin: false,
                isOver: false);

            (bool isWin, GameState endGameState) = Solver.PlayRound(gameStateOne);

            // Game metadata is correct
            Assert.IsTrue(isWin);
            Assert.IsTrue(endGameState.RoundsCompleted.Count == 4);
        }

        [TestMethod]
        public void PlayRound_DifferentColorToWin_ReturnsWin()
        {
            // need to bleed out to win
            var playerHands = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() {
                            new Card(9, Suit.Blue),
                            new Card(7, Suit.Green),
                            new Card(6, Suit.Green),
                            new Card(5, Suit.Green),
                        },
                        new List<Task>()
                        {
                            new Task(new Card(8, Suit.Green)),
                        }
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() {
                            new Card(1, Suit.Green),
                            new Card(2, Suit.Green),
                            new Card(4, Suit.Green),
                            new Card(8, Suit.Green),
                        },
                        new List<Task>()
                        {
                        }
                    )
                },
            };

            var gameStateOne = new GameState(
                playerHands: playerHands,
                roundsCompleted: new List<Round>(),
                playerCount: 2,
                firstPlayerIndex: 0,
                isWin: false,
                isOver: false);

            (bool isWin, GameState endGameState) = Solver.PlayRound(gameStateOne);

            // Game metadata is correct
            Assert.IsTrue(isWin);
            Assert.IsTrue(endGameState.RoundsCompleted.Count == 1);
        }

        [TestMethod]
        public void PlayRound_FullGameFourTurns_ReturnsWin()
        {
            var playerHands = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() {
                            new Card(1, Suit.Blue),
                            new Card(1, Suit.Green),
                            new Card(3, Suit.Yellow),
                            new Card(4, Suit.Yellow),
                        },
                        new List<Task>()
                        {
                        }
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() {
                            new Card(1, Suit.Yellow),
                            new Card(2, Suit.Blue),
                            new Card(2, Suit.Green),
                            new Card(3, Suit.Green),
                        },
                        new List<Task>()
                        {
                        }
                    )
                },
                {2,
                    new Hand(
                        new List<Card>() {
                            new Card(4, Suit.Blue),
                            new Card(2, Suit.Yellow),
                            new Card(2, Suit.Pink),
                            new Card(3, Suit.Blue),
                        },
                        new List<Task>()
                        {
                            new Task(new Card(1, Suit.Pink)),
                        }
                    )
                },
                {3,
                    new Hand(
                        new List<Card>() {
                            new Card(1, Suit.Pink),
                            new Card(3, Suit.Pink),
                            new Card(4, Suit.Green),
                            new Card(4, Suit.Pink),
                        },
                        new List<Task>()
                        {
                            new Task(new Card(1, Suit.Blue)),
                            new Task(new Card(2, Suit.Green)),
                        }
                    )
                },
            };

            var gameStateOne = new GameState(
                playerHands: playerHands,
                roundsCompleted: new List<Round>(),
                playerCount: 4,
                firstPlayerIndex: 3,
                isWin: false,
                isOver: false);

            (bool isWin, GameState endGameState) = Solver.PlayRound(gameStateOne);

            if (isWin)
            {
                foreach (Round round in endGameState.RoundsCompleted)
                {
                    Console.WriteLine($"First to Play: {round.FirstPlayerIndex}");
                    Console.WriteLine($"Winning Player: {round.WinningPlayerIndex}");

                    foreach (Task task in round.TasksCompleted)
                    {
                        Console.WriteLine($"\t task completed: {task}");
                    }

                    foreach (Card card in round.CardsPlayed)
                    {
                        Console.WriteLine($"\t{card}");
                    }
                }
            }

            // Game metadata is correct
            Assert.IsTrue(isWin);
        }

        [TestMethod]
        public void PlayRound_FullGame_ReturnsWin()
        {
            var playerHands = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() {
                            new Card(1, Suit.Blue),
                            new Card(1, Suit.Green),
                            new Card(3, Suit.Yellow),
                            new Card(4, Suit.Yellow),
                        },
                        new List<Task>()
                        {
                        }
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() {
                            new Card(1, Suit.Yellow),
                            new Card(2, Suit.Blue),
                            new Card(2, Suit.Green),
                            new Card(3, Suit.Green),
                        },
                        new List<Task>()
                        {
                        }
                    )
                },
                {2,
                    new Hand(
                        new List<Card>() {
                            new Card(4, Suit.Blue),
                            new Card(2, Suit.Yellow),
                            new Card(2, Suit.Pink),
                            new Card(5, Suit.Pink),
                        },
                        new List<Task>()
                        {
                            new Task(new Card(1, Suit.Pink)),
                        }
                    )
                },
                {3,
                    new Hand(
                        new List<Card>() {
                            new Card(1, Suit.Pink),
                            new Card(3, Suit.Pink),
                            new Card(4, Suit.Green),
                            new Card(4, Suit.Pink),
                        },
                        new List<Task>()
                        {
                            new Task(new Card(1, Suit.Blue)),
                            new Task(new Card(2, Suit.Green)),
                        }
                    )
                },
            };

            var gameStateOne = new GameState(
                playerHands: playerHands,
                roundsCompleted: new List<Round>(),
                playerCount: 4,
                firstPlayerIndex: 3,
                isWin: false,
                isOver: false);

            (bool isWin, GameState endGameState) = Solver.PlayRound(gameStateOne);

            // Game metadata is correct
            Assert.IsTrue(isWin);
        }
    }
}