namespace Solitaire
{
    public interface ICardPlaceBehavior
    {
        bool CanBePlaced(CardModel cardToBePlaced, CardModel cardDropOver);
    }
}
