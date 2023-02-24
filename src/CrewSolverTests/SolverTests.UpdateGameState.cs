namespace CrewSolverTests
{
    /// <summary>
    /// Tests for the Solver class UpdateGameState method.
    /// </summary>
    public partial class SolverTests
    {
        [TestMethod]
        public void UpdateGameState_OneTaskCompleted_ReturnsUpdatedGameState()
        {
            var hands = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() { new Card(9, Suit.Blue), new Card(1, Suit.Pink)},
                        new List<Task>() { new Task(new Card(9, Suit.Blue))}
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() { new Card(2, Suit.Blue), new Card(2, Suit.Pink)},
                        new List<Task>() { new Task(new Card(1, Suit.Blue))}
                    )
                },
            };
            var completedRounds = new List<Round>();
            var isWin = false;
            var isOver = false;
            var numPlayers = 2;
            var firstToPlay = 0;

            var gameStateOne = new GameState(
                hands,
                completedRounds,
                numPlayers,
                firstToPlay,
                isWin,
                isOver);

            var moves = new List<Card>()
            {
                new Card(9, Suit.Blue),
                new Card(2, Suit.Blue)
            };

            var resultGameState = Solver.UpdateGameState(gameStateOne, moves);

            // Game metadata is correct
            Assert.IsTrue(resultGameState.IsWin == false);
            Assert.IsTrue(resultGameState.IsOver == false);
            Assert.IsTrue(resultGameState.PlayerCount == 2);
            Assert.IsTrue(resultGameState.FirstPlayerIndex == 0);

            // Completed rounds was upated correctly
            Assert.IsTrue(resultGameState.RoundsCompleted.Count == 1);
            Assert.IsTrue(resultGameState.RoundsCompleted[0].Equals(
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
                        new Task(new Card(9, Suit.Blue))
                    }
                    )
                ));

            // Remaining cards and tasks are correct
            Assert.IsTrue(resultGameState.PlayerHands[0].Cards.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[0].Cards[0].Equals(new Card(1, Suit.Pink)));
            Assert.IsTrue(resultGameState.PlayerHands[1].Cards.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[1].Cards[0].Equals(new Card(2, Suit.Pink)));
            Assert.IsTrue(resultGameState.PlayerHands[0].Tasks.Count == 0);
            Assert.IsTrue(resultGameState.PlayerHands[1].Tasks.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[1].Tasks[0].Equals(new Task(new Card(1, Suit.Blue))));
        }

        [TestMethod]
        public void UpdateGameState_MultiTaskCompleted_ReturnsUpdatedGameState()
        {
            var hands = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() { new Card(9, Suit.Blue), new Card(1, Suit.Pink)},
                        new List<Task>() { new Task(new Card(9, Suit.Blue)), new Task(new Card(2, Suit.Blue)) }
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() { new Card(2, Suit.Blue), new Card(2, Suit.Pink)},
                        new List<Task>() { new Task(new Card(1, Suit.Blue))}
                    )
                },
            };
            var completedRounds = new List<Round>();
            var isWin = false;
            var isOver = false;
            var numPlayers = 2;
            var firstToPlay = 0;

            var gameStateOne = new GameState(
                hands,
                completedRounds,
                numPlayers,
                firstToPlay,
                isWin,
                isOver);

            var moves = new List<Card>()
            {
                new Card(9, Suit.Blue),
                new Card(2, Suit.Blue)
            };

            var resultGameState = Solver.UpdateGameState(gameStateOne, moves);

            // Game metadata is correct
            Assert.IsFalse(resultGameState.IsWin);
            Assert.IsFalse(resultGameState.IsOver);
            Assert.IsTrue(resultGameState.PlayerCount == 2);
            Assert.IsTrue(resultGameState.FirstPlayerIndex == 0);

            // Completed rounds was upated correctly
            Assert.IsTrue(resultGameState.RoundsCompleted.Count == 1);
            Assert.IsTrue(resultGameState.RoundsCompleted[0].Equals(
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
                        new Task(new Card(2, Suit.Blue)),
                    }
                    )
                ));

            // Remaining cards and tasks are correct
            Assert.IsTrue(resultGameState.PlayerHands[0].Cards.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[0].Cards[0].Equals(new Card(1, Suit.Pink)));
            Assert.IsTrue(resultGameState.PlayerHands[1].Cards.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[1].Cards[0].Equals(new Card(2, Suit.Pink)));
            Assert.IsTrue(resultGameState.PlayerHands[0].Tasks.Count == 0);
            Assert.IsTrue(resultGameState.PlayerHands[1].Tasks.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[1].Tasks[0].Equals(new Task(new Card(1, Suit.Blue))));
        }

        [TestMethod]
        public void UpdateGameState_NoTaskCompleted_ReturnsUpdatedGameState()
        {
            var hands = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() { new Card(9, Suit.Blue), new Card(1, Suit.Pink)},
                        new List<Task>() { new Task(new Card(9, Suit.Yellow)) }
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() { new Card(2, Suit.Blue), new Card(2, Suit.Pink)},
                        new List<Task>() { new Task(new Card(1, Suit.Blue))}
                    )
                },
            };

            var completedRounds = new List<Round>();
            var isWin = false;
            var isOver = false;
            var numPlayers = 2;
            var firstToPlay = 0;

            var gameStateOne = new GameState(
                hands,
                completedRounds,
                numPlayers,
                firstToPlay,
                isWin,
                isOver);

            var moves = new List<Card>()
            {
                new Card(9, Suit.Blue),
                new Card(2, Suit.Blue)
            };

            var resultGameState = Solver.UpdateGameState(gameStateOne, moves);

            // Game metadata is correct
            Assert.IsFalse(resultGameState.IsWin);
            Assert.IsFalse(resultGameState.IsOver);
            Assert.IsTrue(resultGameState.PlayerCount == 2);
            Assert.IsTrue(resultGameState.FirstPlayerIndex == 0);

            // Completed rounds was upated correctly
            Assert.IsTrue(resultGameState.RoundsCompleted.Count == 1);
            Assert.IsTrue(resultGameState.RoundsCompleted[0].Equals(
                new Round(
                    firstPlayerIndex: 0,
                    winningPlayerIndex: 0,
                    cardsPlayed: new List<Card>()
                    {
                        new Card(9, Suit.Blue),
                        new Card(2, Suit.Blue)
                    },
                    tasksCompleted: new List<Task>() { }
                    )
                ));

            // Remaining cards and tasks are correct
            Assert.IsTrue(resultGameState.PlayerHands[0].Cards.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[0].Cards[0].Equals(new Card(1, Suit.Pink)));
            Assert.IsTrue(resultGameState.PlayerHands[1].Cards.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[1].Cards[0].Equals(new Card(2, Suit.Pink)));
            Assert.IsTrue(resultGameState.PlayerHands[0].Tasks.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[0].Tasks[0].Equals(new Task(new Card(9, Suit.Yellow))));
            Assert.IsTrue(resultGameState.PlayerHands[1].Tasks.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[1].Tasks[0].Equals(new Task(new Card(1, Suit.Blue))));
        }

        /// <summary>
        /// Check that the last two tasks were completed. Check that the GameState
        /// isWin and isOver properties have the correct values.
        /// </summary>
        [TestMethod]
        public void UpdateGameState_LastTaskCompleted_ReturnsWinGameState()
        {
            var hands = new Dictionary<int, Hand>()
            {
                {0,
                    new Hand(
                        new List<Card>() { new Card(9, Suit.Blue), new Card(1, Suit.Pink)},
                        new List<Task>() { new Task(new Card(9, Suit.Blue)), new Task(new Card(2, Suit.Blue)) }
                    )
                },
                {1,
                    new Hand(
                        new List<Card>() { new Card(2, Suit.Blue), new Card(2, Suit.Pink)},
                        new List<Task>() {}
                    )
                },
            };

            var completedRounds = new List<Round>();
            var isWin = false;
            var isOver = false;
            var numPlayers = 2;
            var firstToPlay = 0;

            var gameStateOne = new GameState(
                hands,
                completedRounds,
                numPlayers,
                firstToPlay,
                isWin,
                isOver);

            var moves = new List<Card>()
            {
                new Card(9, Suit.Blue),
                new Card(2, Suit.Blue)
            };

            var resultGameState = Solver.UpdateGameState(gameStateOne, moves);

            // Game metadata is correct
            Assert.IsTrue(resultGameState.IsWin);
            Assert.IsTrue(resultGameState.IsOver);
            Assert.IsTrue(resultGameState.PlayerCount == 2);
            Assert.IsTrue(resultGameState.FirstPlayerIndex == 0);

            // Completed rounds was upated correctly
            Assert.IsTrue(resultGameState.RoundsCompleted.Count == 1);
            Assert.IsTrue(resultGameState.RoundsCompleted[0].Equals(
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
                        new Task(new Card(2, Suit.Blue)),
                    }
                    )
                ));
        }

        /// <summary>
        /// Check that when a task is failed, the GameState has the correct values
        /// for isOver and isWin.
        /// </summary>
        [TestMethod]
        public void UpdateGameState_TaskFailed_ReturnsLoseGameState()
        {
            var hands = new Dictionary<int, Hand>()
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
                        new List<Task>() { new Task(new Card(2, Suit.Blue))}
                    )
                },
            };

            var completedRounds = new List<Round>();
            var isWin = false;
            var isOver = false;
            var numPlayers = 2;
            var firstToPlay = 0;

            var gameStateOne = new GameState(
                hands,
                completedRounds,
                numPlayers,
                firstToPlay,
                isWin,
                isOver);

            var moves = new List<Card>()
            {
                new Card(9, Suit.Blue),
                new Card(2, Suit.Blue)
            };

            var resultGameState = Solver.UpdateGameState(gameStateOne, moves);

            // Game metadata is correct
            Assert.IsFalse(resultGameState.IsWin);
            Assert.IsTrue(resultGameState.IsOver);
            Assert.IsTrue(resultGameState.PlayerCount == 2);
            Assert.IsTrue(resultGameState.FirstPlayerIndex == 0);

            // Completed rounds was upated correctly
            Assert.IsTrue(resultGameState.RoundsCompleted.Count == 0);
        }

        [TestMethod]
        public void UpdateGameState_OneTaskCompletedByPlayerTwo_ReturnsUpdatedGameState()
        {
            var hands = new Dictionary<int, Hand>()
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
                        new List<Task>() { new Task(new Card(1, Suit.Pink))}
                    )
                },
            };

            var completedRounds = new List<Round>();
            var isWin = false;
            var isOver = false;
            var numPlayers = 2;
            var firstToPlay = 0;

            var gameStateOne = new GameState(
                hands,
                completedRounds,
                numPlayers,
                firstToPlay,
                isWin,
                isOver);

            var moves = new List<Card>()
            {
                new Card(1, Suit.Pink),
                new Card(2, Suit.Pink)
            };

            var resultGameState = Solver.UpdateGameState(gameStateOne, moves);

            // Game metadata is correct
            Assert.IsTrue(resultGameState.IsWin == false);
            Assert.IsTrue(resultGameState.IsOver == false);
            Assert.IsTrue(resultGameState.PlayerCount == 2);
            Assert.IsTrue(resultGameState.FirstPlayerIndex == 1);

            // Completed rounds was upated correctly
            Assert.IsTrue(resultGameState.RoundsCompleted.Count == 1);
            Assert.IsTrue(resultGameState.RoundsCompleted[0].Equals(
                new Round(
                    firstPlayerIndex: 0,
                    winningPlayerIndex: 1,
                    cardsPlayed: new List<Card>()
                    {
                        new Card(1, Suit.Pink),
                        new Card(2, Suit.Pink)
                    },
                    tasksCompleted: new List<Task>()
                    {
                        new Task(new Card(1, Suit.Pink))
                    }
                    )
                ));

            // Remaining cards and tasks are correct
            Assert.IsTrue(resultGameState.PlayerHands[0].Cards.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[0].Cards[0].Equals(new Card(9, Suit.Blue)));
            Assert.IsTrue(resultGameState.PlayerHands[1].Cards.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[1].Cards[0].Equals(new Card(2, Suit.Blue)));
            Assert.IsTrue(resultGameState.PlayerHands[1].Tasks.Count == 0);
            Assert.IsTrue(resultGameState.PlayerHands[0].Tasks.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[0].Tasks[0].Equals(new Task(new Card(9, Suit.Blue))));
        }

        [TestMethod]
        public void UpdateGameState_ThreePlayers_ReturnsUpdatedGameState()
        {
            var hands = new Dictionary<int, Hand>()
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
                        new List<Task>() { new Task(new Card(1, Suit.Pink))}
                    )
                },
                {2,
                    new Hand(
                        new List<Card>() { new Card(7, Suit.Blue), new Card(1, Suit.Rocket)},
                        new List<Task>() { new Task(new Card(4, Suit.Pink))}
                    )
                },
            };

            var completedRounds = new List<Round>();
            var isWin = false;
            var isOver = false;
            var numPlayers = 3;
            var firstToPlay = 0;

            var gameStateOne = new GameState(
                hands,
                completedRounds,
                numPlayers,
                firstToPlay,
                isWin,
                isOver);

            var moves = new List<Card>()
            {
                new Card(1, Suit.Pink),
                new Card(2, Suit.Pink),
                new Card(7, Suit.Blue)
            };

            var resultGameState = Solver.UpdateGameState(gameStateOne, moves);

            // Game metadata is correct
            Assert.IsTrue(resultGameState.IsWin == false);
            Assert.IsTrue(resultGameState.IsOver == false);
            Assert.IsTrue(resultGameState.PlayerCount == 3);
            Assert.IsTrue(resultGameState.FirstPlayerIndex == 1);

            // Completed rounds was upated correctly
            Assert.IsTrue(resultGameState.RoundsCompleted.Count == 1);
            Assert.IsTrue(resultGameState.RoundsCompleted[0].Equals(
                new Round(
                    firstPlayerIndex: 0,
                    winningPlayerIndex: 1,
                    cardsPlayed: moves,
                    tasksCompleted: new List<Task>()
                    {
                        new Task(new Card(1, Suit.Pink))
                    }
                    )
                ));

            // Remaining cards and tasks are correct
            Assert.IsTrue(resultGameState.PlayerHands[0].Cards.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[0].Cards[0].Equals(new Card(9, Suit.Blue)));
            Assert.IsTrue(resultGameState.PlayerHands[1].Cards.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[1].Cards[0].Equals(new Card(2, Suit.Blue)));
            Assert.IsTrue(resultGameState.PlayerHands[2].Cards.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[2].Cards[0].Equals(new Card(1, Suit.Rocket)));
            Assert.IsTrue(resultGameState.PlayerHands[0].Tasks.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[1].Tasks.Count == 0);
            Assert.IsTrue(resultGameState.PlayerHands[2].Tasks.Count == 1);
            Assert.IsTrue(resultGameState.PlayerHands[0].Tasks[0].Equals(new Task(new Card(9, Suit.Blue))));
            Assert.IsTrue(resultGameState.PlayerHands[2].Tasks[0].Equals(new Task(new Card(4, Suit.Pink))));
        }
    }
}