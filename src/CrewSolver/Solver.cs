namespace CrewSolver
{
    // TODO make classes internal, methods private, give tests access to private methods
    public static class Solver
    {

        /// <summary>
        /// Simulate one round of a game of Crew. Iterates all cards in the
        /// first players hand. From there, gathers all combinations of possible cards
        /// for the rest of the players. With all valid sequences of cards to play, checks
        /// if the sequences results in valid GameState. If they do, makes a recursive call
        /// to PlayRound on valid game states looking for a winning GameState.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static (bool winnable, GameState winningGameState) PlayRound(GameState state)
        {
            var firstPlayerCards = state.PlayerHands[state.FirstPlayerIndex].Cards;

            foreach (var card in firstPlayerCards)
            {
                //Console.WriteLine($"FirstPlayerCard: {firstPlayerCards.Count} cards to try");
                //Console.WriteLine($"FirstPlayerCard: trying card: {card}");
                var roundSuit = card.Suit;
                var otherPlayerCards = new List<List<Card>>();
                otherPlayerCards.Add(new List<Card>() { card });

                // iterate all other players possible cards
                for (var i = 1; i < state.PlayerCount; i++)
                {
                    // get the cards of the next player (iterate through list of players using mod)
                    var playerCards = state.PlayerHands[(state.FirstPlayerIndex + i) % state.PlayerCount].Cards;
                    var sameColorCards = playerCards.FindAll(x => x.Suit == roundSuit);
                    var validCards = sameColorCards.Count > 0 ? sameColorCards : playerCards;

                    otherPlayerCards.Add(validCards);
                }

                var cardSequences = GenerateCardSequences(otherPlayerCards);

                foreach (var cardsPlayed in cardSequences)
                {
                    // Console.WriteLine($"Trying Card Sequence:");

                    foreach (var cardPlayed in cardsPlayed)
                    {
                        // Console.WriteLine($"\t{cardPlayed}");
                    }

                    // see if new sequence generates a valid gamestate
                    var newGameState = UpdateGameState(state, cardsPlayed.ToList());

                    // if it's a winning GameState return the result
                    if (newGameState.IsWin)
                    {
                        return (newGameState.IsWin, newGameState);
                    }
                    // if it's not over, recur on it
                    else if (!newGameState.IsOver)
                    {
                        (bool isWin, GameState childGameState) = PlayRound(newGameState);

                        // if one of it's children has a winning GameState, return it
                        if (isWin)
                        {
                            return (isWin, childGameState);
                        }
                    }
                }
            }

            // if no children GameStates return a winning result, return a loss
            return (false, state);
        }

        /// <summary>
        /// Given the valid cards for the rest of the players, generate an enumerable
        /// of all possible sequences of cards the rest of the players could play.
        /// https://ericlippert.com/2010/06/28/computing-a-cartesian-product-with-linq/
        /// </summary>
        /// <param name="sequences"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<Card>> GenerateCardSequences(IEnumerable<IEnumerable<Card>> sequences)
        {
            IEnumerable<IEnumerable<Card>> empty = new[] { new List<Card>() };

            return sequences.Aggregate(
                empty,
                (accumlator, sequence) =>
                from prevSeq in accumlator
                from item in sequence
                select prevSeq.Concat(new[] { item }));
        }

        /// <summary>
        /// Applies a sequence of moves to the given GameState. Returns the resulting GameState. The first card
        /// in cardsPlayed is for the player who won the previous round.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="cardSequence"></param>
        /// <returns></returns>
        public static GameState UpdateGameState(GameState state, List<Card> cardsPlayed)
        {
            var winningPlayerIndex = 0;

            // Determine which player won the round.
            for (int i = 1; i < state.PlayerCount; i++)
            {
                // Console.WriteLine($"Current winnig player index: {winningPlayerIndex}, i: {i}");

                // If the card is same suit and higher than current winning player index, it's the new highest.
                // Or if the current card is a rocket, and the highest is not, rocket is highest.
                if ((cardsPlayed[i].Suit == cardsPlayed[winningPlayerIndex].Suit && cardsPlayed[i].Value > cardsPlayed[winningPlayerIndex].Value) ||
                    (cardsPlayed[i].Suit == Suit.Rocket && cardsPlayed[winningPlayerIndex].Suit != Suit.Rocket))
                {
                    // Console.WriteLine($"WinningPlayerIndex updated: {cardsPlayed[i]} > {cardsPlayed[winningPlayerIndex]}");
                    winningPlayerIndex = i;
                }
            }

            winningPlayerIndex = (state.FirstPlayerIndex + winningPlayerIndex) % state.PlayerCount;
            // Console.WriteLine($"winningPlayerIndex: {winningPlayerIndex}");

            // See if any of the cards played completed tasks. If a task was completed for the winning player,
            // add to list of tasks. If task completed for non-winning player, game is lost.
            var completedTasks = new List<Task>();
            var failed = false;
            foreach (var card in cardsPlayed)
            {
                for (int i = 0; i < state.PlayerCount; i++)
                {
                    // if this player still has tasks
                    if (state.PlayerHands[i].Tasks.Count > 0)
                    {
                        foreach (var task in state.PlayerHands[i].Tasks)
                        {
                            if (task.Card.Equals(card))
                            {
                                // Task completed.
                                if (i == winningPlayerIndex)
                                {
                                    completedTasks.Add(task);
                                    //Console.WriteLine($"Task completed: {task} by playing card: {card}");
                                }
                                // Task failed.
                                else
                                {
                                    failed = true;
                                }

                                // If the card is found, won't be found for other players.
                                break;
                            }
                        }
                    }
                }
            }

            //Console.WriteLine($"Completed Tasks: {completedTasks.Count}");

            // build result object
            var newHands = new Dictionary<int, Hand>();
            var rounds = new List<Round>(state.RoundsCompleted);
            var isWin = false;
            var isOver = false;

            // If failed, don't need to update rest of GameState, just isOver field, and return.
            if (failed)
            {
                isOver = true;
                //Console.WriteLine($"Failed. IsOver == true");

            }
            // If not failed, update entire GameState.
            else
            {

                var remainingTaskCount = 0;
                var remainingCardsCount = 0;
                for (int i = 0; i < state.PlayerCount; i++)
                {
                    // Determine remaining cards.
                    var remainingCards = new List<Card>(state.PlayerHands[i].Cards);
                    var remainingTasks = new List<Task>();
                    remainingCards = remainingCards.FindAll(x => !cardsPlayed.Contains(x));
                    remainingCardsCount += remainingCards.Count;

                    // Determine remaining tasks.
                    if (state.PlayerHands[i].Tasks.Count > 0)
                    {
                        remainingTasks = state.PlayerHands[i].Tasks.FindAll(x => !completedTasks.Contains(x));
                        remainingTaskCount += remainingTasks.Count;
                    }

                    // Build new hand.
                    newHands[i] = new Hand(remainingCards, remainingTasks);
                }

                // Create round object.
                rounds.Add(new Round(state.FirstPlayerIndex, winningPlayerIndex, cardsPlayed, completedTasks));

                // Check if game is won.
                isWin = remainingTaskCount == 0;

                // Check if game is over. Game is over if there are fewer cards than players remaining.
                // With 4,5 players, there should be 0 cards remaining. With 3 players, 1 card will remain at end.
                isOver = isWin || remainingCardsCount < state.PlayerCount;
            }

            return new GameState(
                newHands,
                rounds,
                state.PlayerCount,
                winningPlayerIndex,
                isWin,
                isOver);
        }

        public static IList<T> EnumeratorToList<T>(IEnumerator<T> e)
        {
            var list = new List<T>();
            while (e.MoveNext())
            {
                list.Add(e.Current);
            }
            return list;
        }
    }
}
