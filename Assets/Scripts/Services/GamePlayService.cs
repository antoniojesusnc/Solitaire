using Solitaire.Utils;
using UnityEngine;

namespace Solitaire
{
    public class GamePlayService : SingletonUnity<GamePlayService>
    {
        [SerializeField] private GamePlayServiceConfig _config;

        public Sprite GetCardImage(int cardNumber, CardSuitsTypes cardSuit)
        {
            var suitInfo = _config.DeckData.CardConfigs.Find(info => info.Suit == cardSuit);
            return suitInfo.CardsImagesByOrder[cardNumber - 1];
        }
    }
}
