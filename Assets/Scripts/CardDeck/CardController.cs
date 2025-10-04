using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Solitaire
{
    public class CardController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] 
        private CanvasGroup canvasGroup;

        [field: SerializeField] 
        public Image _image;
        [field: SerializeField] 
        public Canvas ImageCanvas { get; private set; }
        
        [field: SerializeField] 
        public Transform ChildParent { get; private set; }
        
        public RectTransform CardRectTransform { get; private set; }
        public CardDeck CardDeck { get; private set; }

        private Canvas _generalCanvas;

        public CardController CardChild => ChildParent.GetComponentInChildren<CardController>();

        private void Start()
        {
            _generalCanvas = GetComponentInParent<Canvas>();
            ImageCanvas = _image.GetComponent<Canvas>();
            CardRectTransform = GetComponentInParent<RectTransform>();
            CardDeck = GetComponentInParent<CardDeck>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;

            SetCardLayerToMoving();
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            CardRectTransform.anchoredPosition += eventData.delta / _generalCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            SetCardLayerToIdle();
        }
        
        private void SetCardLayerToMoving()
        {
            foreach (CardController cardChild in GetChilds())
            {
                cardChild.ImageCanvas.overrideSorting = true;
                cardChild.ImageCanvas.sortingOrder = UISortingOrder.CardMovingSortingOrder;
            }
        }
        
        private void SetCardLayerToIdle()
        {
            foreach (CardController cardChild in GetChilds())
            {
                cardChild.ImageCanvas.overrideSorting = false;
                cardChild.ImageCanvas.sortingOrder = UISortingOrder.CardMovingSortingOrder;
            }
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
