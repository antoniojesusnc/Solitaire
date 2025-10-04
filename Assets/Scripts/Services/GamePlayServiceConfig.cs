
using UnityEngine;
using UnityEngine.Serialization;

namespace Solitaire
{
    [CreateAssetMenu(fileName = "GamePlayServiceConfig", menuName = "Solitaire/GamePlayServiceConfig", order = 1)]
    public class GamePlayServiceConfig : ScriptableObject
    {
        [field: Header("Deck Data")]
        [field: SerializeField]
        public DeckConfig DeckData { get; private set; }
        
        [field: Header("Card Config")]
        [field: SerializeField]
        public CardConfig CardConfig { get; private set; }
    }
}
