using UnityEngine;

namespace Solitaire
{
    [CreateAssetMenu(fileName = "UndoServiceConfig", menuName = "Solitaire/UndoServiceConfig", order = 1)]
    public class UndoServiceConfig : ScriptableObject
    {
        [field: SerializeField]
        public int MaxUndos { get; private set; }
    }
}
