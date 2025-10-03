using UnityEngine;

namespace Solitaire
{
    public class MoveCardCommand : Command
    {
        private CardController _cardController;
        private CardDeck _origin;
        private CardDeck _destination;

        public MoveCardCommand(CardController cardController, CardDeck origin, CardDeck destination)
        {
            _cardController = cardController;
            _origin = origin;
            _destination = destination;
            
            _cardController.CardRectTransform.SetParent(_destination.CardLastParent);
            _cardController.CardRectTransform.anchoredPosition = Vector2.zero;
            _destination.AddCard(_cardController);
            _cardController.SetDeck(_destination);
            _origin.RemoveCard(_cardController);
        }

        public override void Undo()
        {
            _cardController.CardRectTransform.SetParent(_origin.CardLastParent);
            _cardController.CardRectTransform.anchoredPosition = Vector2.zero;
            _origin.AddCard(_cardController);
            _cardController.SetDeck(_origin);
            _destination.RemoveCard(_cardController);
        }
    }
}