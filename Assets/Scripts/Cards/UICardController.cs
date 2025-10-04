using System.Collections.Generic;
using deVoid.Utils;
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
        private CardConfig _cardConfig;
        private Vector2 _finalDragPosition;
        
        public UICardController uiCardChild => ChildParent.GetComponentInChildren<UICardController>();

        private Vector2 _movementDelta;
        private Vector3 _rotationDelta;
        private bool _isDragging;

        private void Start()
        {
            _generalCanvas = GetComponentInParent<Canvas>();
            ImageCanvas = _image.GetComponent<Canvas>();
            CardRectTransform = GetComponentInParent<RectTransform>();
            uiCardSlot = GetComponentInParent<UICardSlot>();

            _cardConfig = GamePlayService.Instance.GameplayConfig.CardConfig;
            if (_cardSuitsDebug != CardSuitsTypes.None)
            {
                SetCardModel(new CardModel(_cardNumberDebug, _cardSuitsDebug, _cardColorDebug));
            }
        }

        private void Update()
        {
            ApplyImageRotationWhenMove();
            ApplyFollowCard();
        }

        private void ApplyFollowCard()
        {
            if (_isDragging)
            {
                CardRectTransform.anchoredPosition = Vector2.Lerp(CardRectTransform.anchoredPosition, _finalDragPosition, _cardConfig.FollowSpeed * Time.deltaTime);
            }
        }

        private void ApplyImageRotationWhenMove()
        {
            if (!_isDragging)
            {
                return;
            }

            Vector2 movement = (CardRectTransform.anchoredPosition - _finalDragPosition);
            _movementDelta = Vector2.Lerp(_movementDelta, movement, 25 * Time.deltaTime);
            Vector2 movementRotation = (_isDragging ? _movementDelta : Vector2.zero) * _cardConfig.RotationAmount;
            _rotationDelta = Vector3.Lerp(_rotationDelta, movementRotation, _cardConfig.RotationSpeed * Time.deltaTime);
            _image.transform.eulerAngles = new Vector3(_image.transform.eulerAngles.x, _image.transform.eulerAngles.y, Mathf.Clamp(_rotationDelta.x, -_cardConfig.MaxRotation, _cardConfig.MaxRotation));
            ChildParent.transform.eulerAngles = new Vector3(ChildParent.transform.eulerAngles.x, ChildParent.transform.eulerAngles.y, Mathf.Clamp(_rotationDelta.x, -_cardConfig.MaxRotation, _cardConfig.MaxRotation));
        }

        public void SetCardModel(CardModel cardModel)
        {
            CardModel = cardModel;
            _image.sprite = GamePlayService.Instance.GetCardImage(cardModel.Number, cardModel.Suit);
            gameObject.name = string.Format(CARD_NAME_FORMAT, cardModel.Number, cardModel.Suit);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            EventBusService.Instance?.GetEventMessage<OnCardBeginDraggingEvent>().Dispatch(this);
            
            _movementDelta = Vector2.zero;
            _rotationDelta = Vector2.zero;
            
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;


            SetCardLayerToMoving();
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            _finalDragPosition = CardRectTransform.anchoredPosition + eventData.delta / _generalCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            EventBusService.Instance?.GetEventMessage<OnCardFinishDraggingEvent>().Dispatch(this);
            
            _image.transform.eulerAngles = Vector3.zero;
            ChildParent.transform.eulerAngles = Vector3.zero;
            
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
                childs[i].ImageCanvas.sortingOrder = UISortingOrder.CardMovingSortingOrder + i+1;
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
