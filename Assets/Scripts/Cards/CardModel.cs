using System;

namespace Solitaire
{
    [Serializable]
    public class CardModel : IDisposable
    {
        public int Number { get; private set; }
        public CardSuitsTypes Suit { get; private set; }
        public CardColorTypes Color { get; private set; }

        public CardModel(int number, CardSuitsTypes suit, CardColorTypes color)
        {
            Color = color;
            Number = number;
            Suit = suit;
        }
        
        public void Dispose()
        {
            
        }
    }
}
