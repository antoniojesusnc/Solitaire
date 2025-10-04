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
            
            _cardController.CardRectTransform.SetParent(_destination.GetCardLastParent());
            _cardController.CardRectTransform.anchoredPosition = Vector2.zero;
            _cardController.SetDeck(_destination);
        }

        public override void Undo()
        {
            _cardController.CardRectTransform.SetParent(_origin.GetCardLastParent());
            _cardController.CardRectTransform.anchoredPosition = Vector2.zero;
            _cardController.SetDeck(_origin);
        }
    }
}