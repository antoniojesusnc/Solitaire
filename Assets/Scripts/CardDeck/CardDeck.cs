using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Solitaire
{
    public class CardDeck : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private Transform _cardInitialParentParent;
        


        public void OnDrop(PointerEventData eventData)
        {
            // Verificamos si lo que arrastramos tiene la carta
            if (eventData.pointerDrag == null)
            {
                return;
            }

            if (!eventData.pointerDrag.TryGetComponent<CardController>(out var cardController))
            {
                return;
            }

            var moveCommand = new MoveCardCommand(cardController, cardController.CardDeck, this);
            UndoService.Instance.AddCommand(moveCommand);
        }

        
        public Transform GetCardLastParent()
        {
            var allCardsInDeck = GetAllCards();
            return allCardsInDeck.Count <= 0 ? _cardInitialParentParent : allCardsInDeck[^1].ChildParent;
        }

        private List<CardController> GetAllCards()
        {
            var allCards = new List<CardController>();
            
            var cardController = GetComponentInChildren<CardController>();
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