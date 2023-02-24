namespace CrewSolverTests
{
    /// <summary>
    /// Tests for the Hand class.
    /// </summary>
    [TestClass]
    public class HandTests
    {
        // TODO rename these with better names
        // TODO maybe make a list of all valid scenarios, list of invalid scenarios, iterate them in tests
        List<Card> noCard = new List<Card>();
        List<Card> singleCard = new List<Card>() { new Card(1, Suit.Blue) };
        List<Card> multipleCard = new List<Card>() {
                new Card(1, Suit.Blue),
                new Card(2, Suit.Blue),
            };
        List<Card> singleDuplicateCard = new List<Card>() {
                new Card(1, Suit.Blue),
                new Card(1, Suit.Blue),
            };
        List<Card> multipleDuplicateCard = new List<Card>() {
                new Card(2, Suit.Blue),
                new Card(1, Suit.Blue),
                new Card(7, Suit.Green),
                new Card(1, Suit.Blue),
            };

        List<Task> noTasks = new List<Task>();
        List<Task> singleTask = new List<Task>() { new Task(new Card(1, Suit.Blue)) };
        List<Task> multipleTask = new List<Task>() {
                new Task(new Card(1, Suit.Blue)),
                new Task(new Card(2, Suit.Blue)),
            };
        List<Task> singleDuplicateTask = new List<Task>() {
                new Task(new Card(1, Suit.Blue)),
                new Task(new Card(1, Suit.Blue)),
            };
        List<Task> multipleDuplicateTask = new List<Task>() {
                new Task(new Card(1, Suit.Blue)),
                new Task(new Card(2, Suit.Blue)),
                new Task(new Card(2, Suit.Blue)),
                new Task(new Card(1, Suit.Blue)),
            };

        [TestMethod]
        public void Validate_NoCardsNoTasks_Passes()
        {
            _ = new Hand(noCard, noTasks);
        }

        [TestMethod]
        public void Validate_NormalCardsNoTasks_Passes()
        {
            _ = new Hand(singleCard, noTasks);
            _ = new Hand(multipleCard, noTasks);
        }

        [TestMethod]
        public void Validate_NoCardsNormalTasks_Passes()
        {
            _ = new Hand(noCard, singleTask);
            _ = new Hand(noCard, multipleTask);
        }

        [TestMethod]
        public void Validate_NormalCardsNormalTasks_Passes()
        {
            _ = new Hand(singleCard, singleTask);
            _ = new Hand(singleCard, multipleTask);
            _ = new Hand(multipleCard, multipleTask);
            _ = new Hand(multipleCard, multipleTask);

        }

        [TestMethod]
        public void Validate_DuplicateCardsNormalTasks_Fails()
        {
            try
            {
                _ = new Hand(singleDuplicateCard, singleTask);
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail("Did not throw exception for hand with duplicate cards.");

            try
            {
                _ = new Hand(multipleDuplicateCard, singleTask);
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail("Did not throw exception for hand with duplicate cards.");
        }

        [TestMethod]
        public void Validate_NormalCardsAndDuplicateTasks_Fails()
        {
            try
            {
                _ = new Hand(singleCard, singleDuplicateTask);
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail("Did not throw exception for hand with duplicate cards.");

            try
            {
                _ = new Hand(singleCard, multipleDuplicateTask);
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail("Did not throw exception for hand with duplicate cards.");
        }

        [TestMethod]
        public void Validate_DuplicateCardsAndTasks_Fails()
        {
            try
            {
                _ = new Hand(singleDuplicateCard, singleDuplicateTask);
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail("Did not throw exception for hand with duplicate cards.");
        }

        [TestMethod]
        public void Equals_SameOrderSingleCardAndTask_ReturnsTrue()
        {
            var firstHand = new Hand(singleCard, singleTask);
            var secondHand = new Hand(singleCard, singleTask);

            Assert.IsTrue(firstHand.Equals(secondHand));
        }

        [TestMethod]
        public void Equals_SameOrderMultiCardAndTask_ReturnsTrue()
        {
            var firstHand = new Hand(multipleCard, multipleTask);
            var secondHand = new Hand(multipleCard, multipleTask);

            Assert.IsTrue(firstHand.Equals(secondHand));
            Assert.IsTrue(secondHand.Equals(firstHand));
        }

        [TestMethod]
        public void Equals_DifferentOrderMultiCardAndTask_ReturnsTrue()
        {
            var firstHand = new Hand(
            new List<Card>()
            {
                new Card(1, Suit.Blue),
                new Card(2, Suit.Blue)
            },
            new List<Task>()
            {
                new Task(new Card(5, Suit.Green)),
                new Task(new Card(6, Suit.Green)),
            });

            var secondHand = new Hand(
            new List<Card>()
            {
                new Card(2, Suit.Blue),
                new Card(1, Suit.Blue)
            },
            new List<Task>()
            {
                new Task(new Card(6, Suit.Green)),
                new Task(new Card(5, Suit.Green)),
            });

            Assert.IsTrue(firstHand.Equals(secondHand));
            Assert.IsTrue(secondHand.Equals(firstHand));
        }

        [TestMethod]
        public void Equals_DifferentCards_ReturnsFalse()
        {
            var firstHand = new Hand(
            new List<Card>()
            {
                new Card(1, Suit.Blue),
                new Card(3, Suit.Blue)
            },
            new List<Task>()
            {
                new Task(new Card(5, Suit.Green)),
                new Task(new Card(6, Suit.Green)),
            });

            var secondHand = new Hand(
            new List<Card>()
            {
                new Card(2, Suit.Blue),
                new Card(1, Suit.Blue)
            },
            new List<Task>()
            {
                new Task(new Card(6, Suit.Green)),
                new Task(new Card(5, Suit.Green)),
            });

            Assert.IsFalse(firstHand.Equals(secondHand));
        }

        [TestMethod]
        public void Equals_DifferentTasks_ReturnsFalse()
        {
            var firstHand = new Hand(
            new List<Card>()
            {
                new Card(1, Suit.Blue),
                new Card(3, Suit.Blue)
            },
            new List<Task>()
            {
                new Task(new Card(5, Suit.Yellow)),
                new Task(new Card(6, Suit.Green)),
            });

            var secondHand = new Hand(
            new List<Card>()
            {
                new Card(2, Suit.Blue),
                new Card(1, Suit.Blue)
            },
            new List<Task>()
            {
                new Task(new Card(6, Suit.Green)),
                new Task(new Card(5, Suit.Green)),
            });

            Assert.IsFalse(firstHand.Equals(secondHand));
        }
    }
}
