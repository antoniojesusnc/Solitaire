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
        
        private void Start()
        {
            UpdateButtonAvailability();
            
            EventBusService.Instance.GetEventMessage<OnAddUndoCommandEvent>().AddListener(UpdateButtonAvailability);
            EventBusService.Instance.GetEventMessage<OnMakeUndoCommandEvent>().AddListener(UpdateButtonAvailability);
        }

        private void OnDestroy()
        {
            EventBusService.Instance?.GetEventMessage<OnAddUndoCommandEvent>()?.RemoveListener(UpdateButtonAvailability);
            EventBusService.Instance?.GetEventMessage<OnMakeUndoCommandEvent>()
                ?.RemoveListener(UpdateButtonAvailability);
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
