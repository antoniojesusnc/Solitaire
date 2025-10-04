using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Solitaire
{
    public class UICardSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private Transform _cardInitialParentParent;
        
        [SerializeReference, SubclassSelector]
        private ICardPlaceBehavior _cardPlaceBehavior;
        
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
            {
                return;
            }

            if (!eventData.pointerDrag.TryGetComponent<UICardController>(out var cardController))
            {
                return;
            }
            
            if (!_cardPlaceBehavior.CanBePlaced(cardController.CardModel, GetLastCard()?.CardModel))
            {
                cardController.ResetToOriginalPosition();
                return;
            }

            var moveCommand = new MoveCardCommand(cardController, cardController.uiCardSlot, this);
            UndoService.Instance.AddCommand(moveCommand);
        }

        public UICardController GetLastCard()
        {
            var allCardsInDeck = GetAllCards();
            return allCardsInDeck.Count <= 0 ? null : allCardsInDeck[^1];
        }
        public Transform GetLastCardChildParent()
        {
            var allCardsInDeck = GetAllCards();
            return allCardsInDeck.Count <= 0 ? _cardInitialParentParent : allCardsInDeck[^1].ChildParent;
        }

        private List<UICardController> GetAllCards()
        {
            var allCards = new List<UICardController>();
            
            var cardController = GetComponentInChildren<UICardController>();
            if (cardController == null)
            {
                return allCards;
            }
            
            allCards.Add(cardController);
            allCards.AddRange(cardController.GetChilds());

            return allCards;
        }
    }
}