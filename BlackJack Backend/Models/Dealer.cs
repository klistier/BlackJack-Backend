namespace BlackJack_Backend.Models
{
    public class Dealer
    {
        public List<Card> HandOfCards { get; set; } = [];
        public int HandValue { get; set; } = 0;
    }


}
