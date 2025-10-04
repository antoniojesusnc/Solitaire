using System;
using deVoid.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Solitaire
{
    public class UIUndoButton : MonoBehaviour
    {
        [SerializeField] 
        private Button _button;
        [SerializeField] 
        private CanvasGroup _canvasGroup;
        
        private void Start()
        {
            UpdateButtonAvailability();

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventBusService.Instance.GetEventMessage<OnAddUndoCommandEvent>().AddListener(UpdateButtonAvailability);
            EventBusService.Instance.GetEventMessage<OnMakeUndoCommandEvent>().AddListener(UpdateButtonAvailability);
            EventBusService.Instance.GetEventMessage<OnCardBeginDraggingEvent>().AddListener(OnCardBeginDragging);
            EventBusService.Instance.GetEventMessage<OnCardFinishDraggingEvent>().AddListener(OnCardFinishDragging);
        }

        private void OnCardBeginDragging(UICardController cardController)
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
        
        private void OnCardFinishDragging(UICardController cardController)
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            UpdateButtonAvailability();
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
        
        private void UnsubscribeToEvents()
        {
            EventBusService.Instance?.GetEventMessage<OnAddUndoCommandEvent>()?.RemoveListener(UpdateButtonAvailability);
            EventBusService.Instance?.GetEventMessage<OnMakeUndoCommandEvent>()?.RemoveListener(UpdateButtonAvailability);
            EventBusService.Instance?.GetEventMessage<OnCardBeginDraggingEvent>()?.RemoveListener(OnCardBeginDragging);
            EventBusService.Instance?.GetEventMessage<OnCardFinishDraggingEvent>()?.RemoveListener(OnCardFinishDragging);
        }

        private void UpdateButtonAvailability()
        {
            _button.interactable = UndoService.Instance.IsUndoAvailable;
        }

        public void Click()
        {
            UndoService.Instance.Undo();
        }
    }
}
