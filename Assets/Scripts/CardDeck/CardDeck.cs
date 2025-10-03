using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Solitaire
{
    public class CardDeck : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private Transform _cardInitialParentParent;
        
        public Transform CardLastParent => _cards.Count <= 0 ? _cardInitialParentParent : _cards[^1].ChildParent;
        
        private List<CardController> _cards = new List<CardController>();

        private void Start()
        {
            var firstCard = _cardInitialParentParent.GetComponentInChildren<CardController>();
            if (firstCard != null)
            {
                _cards.Add(firstCard);
                _cards.AddRange(firstCard.GetChilds());
            }
        }

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

        public void AddCard(CardController cardController)
        {
            _cards.Add(cardController);
            _cards.AddRange(cardController.GetChilds());
        }
        
        public void RemoveCard(CardController cardController)
        {
            var index = _cards.IndexOf(cardController);
            _cards.RemoveRange(index, _cards.Count - index);
        }

    }
}