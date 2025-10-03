using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Solitaire
{
    public class CardController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [field: SerializeField] public Transform ChildParent { get; private set; }

        public RectTransform CardRectTransform { get; private set; }
        public CardDeck CardDeck { get; private set; }

        private Canvas _canvas;

        public CardController CardChild { get; private set; }

        private void Start()
        {
            _canvas = GetComponentInParent<Canvas>();
            CardRectTransform = GetComponentInParent<RectTransform>();
            CardDeck = GetComponentInParent<CardDeck>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            CardRectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }

        public void SetDeck(CardDeck cardDeck)
        {
            CardDeck = cardDeck;
        }

        public List<CardController> GetChilds()
        {
            List<CardController> childs = new List<CardController>();
            if (CardChild == null)
            {
                return childs;
            }

            childs.Add(CardChild);
            childs.AddRange(CardChild.GetChilds());
            return childs;
        }

        public void ResetToOriginalPosition()
        {
            CardRectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
