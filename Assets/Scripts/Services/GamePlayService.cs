using Solitaire.Utils;
using UnityEngine;

namespace Solitaire
{
    public class GamePlayService : SingletonUnity<GamePlayService>
    {
        [field: SerializeField] public GamePlayServiceConfig GameplayConfig { get; private set; }

        public Sprite GetCardImage(int cardNumber, CardSuitsTypes cardSuit)
        {
            var suitInfo = GameplayConfig.DeckData.CardConfigs.Find(info => info.Suit == cardSuit);
            return suitInfo.CardsImagesByOrder[cardNumber - 1];
        }
    }
}
