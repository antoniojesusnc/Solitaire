using UnityEngine;
using UnityEngine.UI;

namespace Solitaire
{
    public class UIUndoButton : MonoBehaviour
    {
        [SerializeField] 
        private Image _buttonBackgronud;
        
        private void Start()
        {
            
        }

        public void Click()
        {
            UndoService.Instance.Undo();
        }
    }
}
