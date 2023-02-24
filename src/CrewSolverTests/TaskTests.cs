namespace CrewSolverTests
{
    /// <summary>
    /// Tests for the Task class.
    /// </summary>
    [TestClass]
    public class TaskTests
    {

        List<Suit> colorSuit = new List<Suit>
            {
                Suit.Blue,
                Suit.Green,
                Suit.Yellow,
                Suit.Pink
            };

        [TestMethod]
        public void Validate_ColorCardTasks_Passes()
        {
            for (int i = 1; i < 9; i++)
            {
                foreach (var suit in colorSuit)
                {
                    _ = new Task(new Card(i, suit));
                }
            }
        }

        [TestMethod]
        public void Validate_RocketCardTasks_Fails()
        {
            try
            {
                _ = new Task(new Card(1, Suit.Rocket));
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail("Did not throw exception for Task with rocket card.");
        }

        [TestMethod]
        public void Validate_SameTasks_Passes()
        {
            // TODO iterate all valid tasks
            Assert.AreEqual(
                new Task(new Card(1, Suit.Green)),
                new Task(new Card(1, Suit.Green))
                );
        }

        [TestMethod]
        public void Validate_DifferentTasks_Fails()
        {
            // TODO iterate all invalid tasks? or cover more scenarios at least.
            Assert.AreNotEqual(
                new Task(new Card(1, Suit.Green)),
                new Task(new Card(2, Suit.Green))
                );

            Assert.AreNotEqual(
                new Task(new Card(1, Suit.Green)),
                new Task(new Card(1, Suit.Blue))
                );
        }
    }
}