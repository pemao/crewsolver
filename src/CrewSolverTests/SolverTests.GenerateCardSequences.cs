namespace CrewSolverTests
{
    /// <summary>
    /// Tests for the Solver class GenerateCardSequences method.
    /// </summary>
    [TestClass]
    public partial class SolverTests
    {

        // TODO make sure test coverage is complete, make tests easier to write
        [TestMethod]
        public void GenerateCardSequences_EmptySequences()
        {
            var sequences = new List<List<Card>>() { };
            var enumerator = Solver.GenerateCardSequences(sequences).GetEnumerator();
            var generatedSequences = Solver.EnumeratorToList(enumerator);

            Assert.AreEqual(generatedSequences.Count, 1);
        }

        [TestMethod]
        public void GenerateCardSequences_OneItemOneSequence()
        {
            var expectedOutput = new List<Card>() { new Card(1, Suit.Green) };
            var sequences = new List<List<Card>>()
            {
                new List<Card>() { new Card(1, Suit.Green) }
            };
            var enumerator = Solver.GenerateCardSequences(sequences).GetEnumerator();
            var generatedSequences = Solver.EnumeratorToList(enumerator);
            var generatedSequence = generatedSequences[0].ToList<Card>();

            Assert.AreEqual(generatedSequences.Count, 1);
            Assert.IsTrue(generatedSequence.SequenceEqual(expectedOutput));
        }

        [TestMethod]
        public void GenerateCardSequences_MultiItemOneSequence()
        {
            var expectedOutputs = new List<List<Card>>()
            {
                new List<Card>() { new Card(1, Suit.Green) },
                new List<Card>() { new Card(2, Suit.Blue) },
                new List<Card>() { new Card(9, Suit.Green) }
            };

            var sequences = new List<List<Card>>()
            {
                new List<Card>() {
                    new Card(1, Suit.Green),
                    new Card(2, Suit.Blue),
                    new Card(9, Suit.Green)
                },
            };
            var enumerator = Solver.GenerateCardSequences(sequences).GetEnumerator();
            var generatedSequences = Solver.EnumeratorToList(enumerator);

            Assert.AreEqual(generatedSequences.Count, 3);

            for (int i = 0; i < expectedOutputs.Count; i++)
            {
                Assert.IsTrue(generatedSequences[i].ToList<Card>().SequenceEqual(expectedOutputs[i]));
            }
        }

        [TestMethod]
        public void GenerateCardSequences_OneItemMultiSequence()
        {
            var expectedOutputs = new List<List<Card>>()
            {
                new List<Card>() {
                    new Card(1, Suit.Green),
                    new Card(2, Suit.Blue),
                    new Card(9, Suit.Green)
                }
            };

            var sequences = new List<List<Card>>()
            {
                new List<Card>() { new Card(1, Suit.Green) },
                new List<Card>() { new Card(2, Suit.Blue) },
                new List<Card>() { new Card(9, Suit.Green) }
            };
            var enumerator = Solver.GenerateCardSequences(sequences).GetEnumerator();
            var generatedSequences = Solver.EnumeratorToList(enumerator);

            Assert.AreEqual(generatedSequences.Count, 1);

            for (int i = 0; i < expectedOutputs.Count; i++)
            {
                Assert.IsTrue(generatedSequences[i].ToList<Card>().SequenceEqual(expectedOutputs[i]));
            }
        }

        [TestMethod]
        public void GenerateCardSequences_MultiItemMultiSequence()
        {
            var expectedOutputs = new List<List<Card>>()
            {
                new List<Card>() {
                    new Card(1, Suit.Green),
                    new Card(2, Suit.Blue)
                },
                new List<Card>() {
                    new Card(1, Suit.Green),
                    new Card(8, Suit.Green)
                },
                new List<Card>() {
                    new Card(1, Suit.Green),
                    new Card(4, Suit.Rocket)
                },
                new List<Card>() {
                    new Card(5, Suit.Yellow),
                    new Card(2, Suit.Blue)
                },
                new List<Card>() {
                    new Card(5, Suit.Yellow),
                    new Card(8, Suit.Green)
                },
                new List<Card>() {
                    new Card(5, Suit.Yellow),
                    new Card(4, Suit.Rocket)
                },
            };

            var sequences = new List<List<Card>>()
            {
                new List<Card>()
                {
                    new Card(1, Suit.Green),
                    new Card(5, Suit.Yellow),
                },
                new List<Card>()
                {
                    new Card(2, Suit.Blue),
                    new Card(8, Suit.Green),
                    new Card(4, Suit.Rocket),
                },
            };
            var enumerator = Solver.GenerateCardSequences(sequences).GetEnumerator();
            var generatedSequences = Solver.EnumeratorToList(enumerator);

            Assert.AreEqual(generatedSequences.Count, expectedOutputs.Count);

            for (int i = 0; i < expectedOutputs.Count; i++)
            {
                Assert.IsTrue(generatedSequences[i].ToList<Card>().SequenceEqual(expectedOutputs[i]));
            }
        }
    }
}