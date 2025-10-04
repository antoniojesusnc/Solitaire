using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Solitaire
{
    public class UICardController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private const string CARD_NAME_FORMAT = "Card {0} of {1}";
        
        [SerializeField] 
        private CanvasGroup canvasGroup;

        [field: SerializeField] 
        public Image _image;
        [field: SerializeField] 
        public Canvas ImageCanvas { get; private set; }
        
        [field: SerializeField] 
        public Transform ChildParent { get; private set; }
        
        public RectTransform CardRectTransform { get; private set; }
        public UICardSlot uiCardSlot { get; private set; }
        
        [Header("Debug Card")]
        [SerializeField] private CardSuitsTypes _cardSuitsDebug;
        [SerializeField] private int _cardNumberDebug;
        [SerializeField] private CardColorTypes _cardColorDebug;
        
        public CardModel CardModel { get; private set; }

        private Canvas _generalCanvas;

        public UICardController uiCardChild => ChildParent.GetComponentInChildren<UICardController>();

        private void Start()
        {
            _generalCanvas = GetComponentInParent<Canvas>();
            ImageCanvas = _image.GetComponent<Canvas>();
            CardRectTransform = GetComponentInParent<RectTransform>();
            uiCardSlot = GetComponentInParent<UICardSlot>();

            if (_cardSuitsDebug != CardSuitsTypes.None)
            {
                SetCardModel(new CardModel(_cardNumberDebug, _cardSuitsDebug, _cardColorDebug));
            }
        }

        public void SetCardModel(CardModel cardModel)
        {
            CardModel = cardModel;
            _image.sprite = GamePlayService.Instance.GetCardImage(cardModel.Number, cardModel.Suit);
            gameObject.name = string.Format(CARD_NAME_FORMAT, cardModel.Number, cardModel.Suit);
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
            ImageCanvas.overrideSorting = true;
            ImageCanvas.sortingOrder = UISortingOrder.CardMovingSortingOrder;

            var childs = GetChilds();
            for (int i = 0; i < childs.Count; i++)
            {
                childs[i].ImageCanvas.overrideSorting = true;
                childs[i].ImageCanvas.sortingOrder = UISortingOrder.CardMovingSortingOrder + i;
            }
        }
        
        private void SetCardLayerToIdle()
        {
            ImageCanvas.overrideSorting = false;
            foreach (UICardController cardChild in GetChilds())
            {
                cardChild.ImageCanvas.overrideSorting = false;
            }
        }

        public void SetDeck(UICardSlot uiCardSlot)
        {
            this.uiCardSlot = uiCardSlot;
        }

        public List<UICardController> GetChilds()
        {
            List<UICardController> childs = new List<UICardController>();
            if (uiCardChild == null)
            {
                return childs;
            }

            childs.Add(uiCardChild);
            childs.AddRange(uiCardChild.GetChilds());
            return childs;
        }

        public void ResetToOriginalPosition()
        {
            CardRectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
