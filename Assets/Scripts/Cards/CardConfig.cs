using UnityEngine;

namespace Solitaire
{
    [CreateAssetMenu(fileName = "CardConfig", menuName = "Solitaire/CardConfig", order = 1)]
    public class CardConfig : ScriptableObject
    {

        [field: Header("Card Effects")]
        [field: SerializeField]
        public float FollowSpeed { get; private set; }
        [field: SerializeField]
        public float MovementMod { get; private set; }
        
        [field: Header("Rotation Parameters")]
        [field: SerializeField] public float RotationAmount { get ; private set; }
        [field: SerializeField] public float RotationSpeed { get ; private set; }
        [field: SerializeField] public float MaxRotation { get ; private set; }
    }
    
    
}
