namespace BlackJack_Backend.Models
{
    public class GameLogic
    {
        private readonly Deck deck;

        //dela ut första handen
        public (List<Card> playerHand, List<Card> dealerHand) DealInitialHand()
        {

            List<Card> playerHand = [deck.DrawCard(), deck.DrawCard()];
            List<Card> dealerHand = [deck.DrawCard(false), deck.DrawCard()];

            return (playerHand, dealerHand);
        }

        //dela ut ett kort
        public Card DealCard()
        {
            return (deck.DrawCard());
        }
    }
}
