namespace Solitaire
{
    public abstract class CardPlaceBehavior : ICardPlaceBehavior
    {
        public abstract bool CanBePlaced(CardModel cardToBePlaced, CardModel cardDropOver);
    }
}
