using UnityEngine;

namespace Solitaire
{
    public class MoveCardCommand : Command
    {
        private UICardController _uiCardController;
        private UICardSlot _origin;
        private UICardSlot _destination;

        public MoveCardCommand(UICardController uiCardController, UICardSlot origin, UICardSlot destination)
        {
            _uiCardController = uiCardController;
            _origin = origin;
            _destination = destination;
            
            _uiCardController.CardRectTransform.SetParent(_destination.GetCardLastParent());
            _uiCardController.CardRectTransform.anchoredPosition = Vector2.zero;
            _uiCardController.SetDeck(_destination);
        }

        public override void Undo()
        {
            _uiCardController.CardRectTransform.SetParent(_origin.GetCardLastParent());
            _uiCardController.CardRectTransform.anchoredPosition = Vector2.zero;
            _uiCardController.SetDeck(_origin);
        }
    }
}