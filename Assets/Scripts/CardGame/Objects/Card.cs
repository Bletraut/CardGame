using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;

using CardGame.Data;

namespace CardGame.Objects
{
    public class Card : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _cardSprite;
        public SpriteRenderer SpriteRenderer => _cardSprite;

        public string Identifier { get; private set; }

        public CardEvent OnPicked;

        private Vector3 cardSpriteScale;

        void Start()
        {
            cardSpriteScale = _cardSprite.transform.localScale;
        }
        void OnDisable()
        {
            ClearEffects();
        }

        void OnMouseDown()
        {
            OnPicked?.Invoke(this);
        }

        public void SetCardData(CardData cardData)
        {
            Identifier = cardData.Identifier;
            SpriteRenderer.sprite = cardData.Sprite;
            SpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, cardData.RotationOffset);
        }

        public void AppearEffect()
        {
            var startScale = transform.localScale;
            transform.localScale = Vector3.zero;
            transform.DOScale(startScale, 1).SetEase(Ease.OutBounce);
        }
        public void ShakeEffect()
        {
            ClearEffects();
            _cardSprite.transform.DOPunchPosition(new Vector3(0.35f, 0, 0), 1);
        }
        public void BounceEffect()
        {
            ClearEffects();
            _cardSprite.transform.DOPunchScale(new Vector3(-0.15f, -0.15f, 0), 1, 5);
        }
        private void ClearEffects()
        {
            _cardSprite.transform.DOKill();
            _cardSprite.transform.localScale = cardSpriteScale;
            _cardSprite.transform.localPosition = Vector3.zero;
        }
    }

    [System.Serializable]
    public class CardEvent : UnityEvent<Card> { }
}