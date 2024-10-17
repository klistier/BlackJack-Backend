namespace BlackJack_Backend.Models
{
    public class Player(string name, int currency, List<Card> handOfCards)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public int Currency { get; set; } = currency;
        public List<Card> HandOfCards { get; set; } = handOfCards;



        //räkna ut värdet på spelarhanden
        public int Value CalculateUserHandValue(List<Card> handOfCards)
        {
            int totalSum = 0;
            foreach (Card card in handOfCards)
            {
                if (int.TryParse(card.Value, out int cardValue))
                {
                    totalSum += cardValue;

                }
                else if (card.Value == "Knekt" || card.Value == "Dam" || card.Value == "Kung")
                {
                    totalSum += 10;
                }
                else if (card.Value == "Ess")
                {
                    totalSum += 11;
                }
                //HANTERA OM ESS ÄR 1 HÄR

            }
        }

        //spelardrag

    }



}
