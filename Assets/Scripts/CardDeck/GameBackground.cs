using UnityEngine;
using UnityEngine.EventSystems;

namespace Solitaire
{
    public class GameBackground : MonoBehaviour, IDropHandler
    {
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
            
            cardController.ResetToOriginalPosition();
        }
    }
}