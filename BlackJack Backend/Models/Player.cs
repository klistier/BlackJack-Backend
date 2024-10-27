namespace BlackJack_Backend.Models
{
    public class Player
    {
        public int Currency { get; set; } = 1000;
        public List<Card> HandOfCards { get; set; } = [];
        public int HandValue { get; set; } = 0;
        public bool CanDrawCard { get; set; } = true;

        //Satsa
        public string PlaceBet(int betSum)
        {
            if (betSum > Currency)
            {
                return "Inte tillräckligt med pengar!";
            }
            else
            {
                Currency -= betSum;
                return "Bet lagt!";
            }
        }
    }
}
