namespace BlackJack_Backend.Models
{
    public class Game
    {
        public Deck Deck { get; set; }
        public Player Player { get; set; }
        public Dealer Dealer { get; set; }

        public bool IsGameOver { get; set; } = false;
        public string Winner { get; set; } = string.Empty;
        public bool IsATie { get; set; } = false;

        //Skapa nya instanser av klasserna
        public Game()
        {
            Deck = new Deck();
            Player = new Player();
            Dealer = new Dealer();
        }
    }
}
