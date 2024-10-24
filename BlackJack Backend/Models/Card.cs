namespace BlackJack_Backend.Models
{
    public class Card(string suite, string value)
    {
        public string Suite { get; set; } = suite;
        public string Value { get; set; } = value;
        public bool IsFaceUp { get; set; } = true;
    }
}
