namespace CrewSolverTests
{
    /// <summary>
    /// Tests for the Card class.
    /// </summary>
    [TestClass]
    public class CardTests
    {

        List<Suit> colorSuit = new List<Suit>
            {
                Suit.Blue,
                Suit.Green,
                Suit.Yellow,
                Suit.Pink
            };

        [TestMethod]
        public void Validate_RegularCards_Passes()
        {
            _ = new Card(1, Suit.Rocket);
            _ = new Card(2, Suit.Rocket);
            _ = new Card(3, Suit.Rocket);
            _ = new Card(4, Suit.Rocket);

            for (int i = 1; i < 9; i++)
            {
                foreach (var suit in colorSuit)
                {
                    _ = new Card(i, suit);
                }
            }
        }

        [TestMethod]
        public void Validate_ColorCardZeroValue_Fails()
        {
            try
            {
                _ = new Card(0, Suit.Blue);
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail("Did not throw exception for value: 0");
        }

        [TestMethod]
        public void Validate_RocketCardZeroValue_Fails()
        {
            try
            {
                _ = new Card(0, Suit.Rocket);
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail("Did not throw exception for value: 0");
        }

        [TestMethod]
        public void Validate_ColorCardTooLargeValue_Fails()
        {
            foreach (var suit in colorSuit)
            {
                try
                {
                    _ = new Card(10, Suit.Blue);
                }
                catch (Exception)
                {
                    continue;
                }

                Assert.Fail("Did not throw exception for too large value");
            }


        }

        [TestMethod]
        public void Validate_RocketCardTooLargeValue_Fails()
        {
            try
            {
                _ = new Card(5, Suit.Rocket);
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail("Did not throw exception for too large value");
        }

        [TestMethod]
        public void Equals_SameCard_Passes()
        {
            Assert.AreEqual(new Card(1, Suit.Rocket), new Card(1, Suit.Rocket));
        }

        [TestMethod]
        public void Equals_DifferentCard_Fails()
        {
            Assert.AreNotEqual(new Card(1, Suit.Rocket), new Card(1, Suit.Blue));
            Assert.AreNotEqual(new Card(1, Suit.Green), new Card(1, Suit.Blue));
        }
    }
}