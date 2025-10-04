
using UnityEngine;
using UnityEngine.Serialization;

namespace Solitaire
{
    [CreateAssetMenu(fileName = "GamePlayServiceConfig", menuName = "Solitaire/GamePlayServiceConfig", order = 1)]
    public class GamePlayServiceConfig : ScriptableObject
    {
        [field: SerializeField]
        public DeckConfig DeckData { get; private set; }
    }
}
