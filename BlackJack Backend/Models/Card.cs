﻿namespace BlackJack_Backend.Models
{
    public class Card(string suite, string value)
    {
        public int Id { get; set; }
        public string Suite { get; set; } = suite;
        public string Value { get; set; } = value;
    }
}