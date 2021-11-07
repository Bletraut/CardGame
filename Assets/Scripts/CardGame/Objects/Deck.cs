using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;

using CardGame.Data;

namespace CardGame.Objects
{
    public class Deck : MonoBehaviour
    {
        [SerializeField]
        private Card cardPrefab;

        private List<Card> cardsPool;
        private List<string> _excludedCardIdentifiers;
        public ReadOnlyCollection<string> ExcludedCardIdentifiers => _excludedCardIdentifiers.AsReadOnly();

        private CardData validCardData = null;
        public string ValidCardIdentifier
        {
            get
            {
                if (validCardData == null) return string.Empty;
                return validCardData.Identifier;
            }
        }

        // Use this for initialization
        void Start()
        {
            cardsPool = new List<Card>();
            _excludedCardIdentifiers = new List<string>();
        }

        public List<Card> GetRandomCards(CardBundleData cardBundle, int count)
        {
            if (count >= cardBundle.CardsData.Length)
            {
                Debug.LogError("The number of cards is greater than the number of cards in the bundle.");
                return new List<Card> { };
            }

            ResetPool();
            validCardData = null;

            var newCardsData = cardBundle.CardsData.OrderBy(n => Random.value).Take(count).ToList();
            var hasValidCard = newCardsData.Any(n => !_excludedCardIdentifiers.Contains(n.Identifier));
            if (hasValidCard)
            {
                var validCardsData = newCardsData.Where(n => !_excludedCardIdentifiers.Contains(n.Identifier)).ToList();
                validCardData = validCardsData[Random.Range(0, validCardsData.Count())];
            }
            else
            {
                validCardData = cardBundle.CardsData.First(n => !_excludedCardIdentifiers.Contains(n.Identifier));
                newCardsData[Random.Range(0, count)] = validCardData;
            }

            var cards = new List<Card>();
            newCardsData.ForEach(cardData =>
            {
                var card = GetCardFromPool() ?? AddCardToPool();
                card.gameObject.SetActive(true);
                card.SetCardData(cardData);
                cards.Add(card);
            });

            return cards;
        }

        public void ExcludeCardIdentifier(string identifier) => _excludedCardIdentifiers.Add(identifier);
        public void ResetExcludedCardIdentifiers() => _excludedCardIdentifiers.Clear();

        private void ResetPool() => cardsPool.ForEach(n => n.gameObject.SetActive(false));
        private Card GetCardFromPool() => cardsPool.FirstOrDefault(n => !n.gameObject.activeSelf);
        private Card AddCardToPool()
        {
            var card = Instantiate(cardPrefab);
            cardsPool.Add(card);

            return card;
        }
    }
}