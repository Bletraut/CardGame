using System.Collections;

using UnityEngine;

namespace CardGame.Data
{
    [CreateAssetMenu(fileName = "New CardBundleData", menuName = "Card Game/Card Bundle Data", order = 10)]
    public class CardBundleData : ScriptableObject
    {
        [SerializeField]
        private CardData[] _cardsData;
        public CardData[] CardsData => _cardsData;
    }
}