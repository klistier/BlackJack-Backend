using Microsoft.VisualBasic;

namespace BlackJack_Backend.Models
{
    public class Deck
    {
        List<string> suites = new List<string> { "Hjärter", "Spader", "Ruter", "Klöver" };
        List<string> values = new List<string> { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Knekt", "Dam", "Kung", "Ess" };
        readonly List<Card> deck = new List<Card>();

        //skapa och blanda leken
        public List<Card> CreateDeck()
        {
            foreach (string suite in suites)
            {
                foreach (string value in values)
                {
                    Card card = new(suite, value);
                    deck.Add(card);
                }
            }
            Shuffle(deck);
            return deck;
        }
        //blanda leken
        public List<Card> Shuffle(List<Card> deck)
        {
            Random rnd = new();
            for (int i = deck.Count - 1; i > 0; i--)
            {
                var k = rnd.Next(i + 1);
                var value = deck[k];
                deck[k] = deck[i];
                deck[i] = value;
            }
            return deck;
        }
        //dra ett kort
        public Card DrawCard(bool isFaceUp = true)
        {
            var card = deck[0];
            deck.RemoveAt(0);
            return card;
        }

    }

}
