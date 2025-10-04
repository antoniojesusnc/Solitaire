using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Utils;

namespace Solitaire
{
    [CreateAssetMenu(fileName = "DeckConfig", menuName = "Solitaire/DeckConfig", order = 1)]
    public class DeckConfig : ScriptableObject
    {
        [field: SerializeField] 
        public List<DeckConfigInfo> CardConfigs { get; private set; } = new List<DeckConfigInfo>();
    }
    
    [Serializable]
    public class DeckConfigInfo
    {
        [field: SerializeField]
        public CardSuitsTypes Suit { get; private set; }
        [field: SerializeField]
        public CardColorTypes Color { get; private set; }

        [field: SerializeField, PreviewSprite]
        public List<Sprite> CardsImagesByOrder { get; private set; }
        
    }
}
