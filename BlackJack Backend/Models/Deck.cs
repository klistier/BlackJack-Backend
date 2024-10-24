using Microsoft.VisualBasic;

namespace BlackJack_Backend.Models
{
    public class Deck
    {
        public List<Card> CurrentDeck {get; set;} = [];

        List<string> suites = ["Hjärter", "Spader", "Ruter", "Klöver"];
        List<string> values = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "Knekt", "Dam", "Kung", "Ess"];
        
        //skapa och blanda leken
        public void CreateDeck()
        {
            foreach (string suite in suites)
            {
                foreach (string value in values)
                {
                    Card card = new(suite, value);
                    CurrentDeck.Add(card);
                }
            }
            Shuffle(CurrentDeck);
        }
        //blanda leken
        public List<Card> Shuffle(List<Card> deck)
        {
            Random rnd = new();
            for (int i = deck.Count - 1; i > 0; i--)
            {
                var k = rnd.Next(i + 1);
                (deck[i], deck[k]) = (deck[k], deck[i]);
            }
            return deck;
        }


    }

}
