using System;

namespace Solitaire
{
    [Serializable]
    public class CardPlaceAnyIfEmptyOrDifferentColorAndNumberIncrease : CardPlaceBehavior
    {
        public override bool CanBePlaced(CardModel cardToBePlaced, CardModel cardDropOver)
        {
            if (cardDropOver == null)
            {
                return true;
            }

            return cardToBePlaced.Color != cardDropOver.Color
                   && cardToBePlaced.Number+1 == cardDropOver.Number;
        }
    }
}
