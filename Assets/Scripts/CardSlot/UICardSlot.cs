using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Solitaire
{
    public class UICardSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private Transform _cardInitialParentParent;
        
        
        
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

            var moveCommand = new MoveCardCommand(cardController, cardController.uiCardSlot, this);
            UndoService.Instance.AddCommand(moveCommand);
        }

        
        public Transform GetCardLastParent()
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