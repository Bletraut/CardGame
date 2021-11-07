using System;

using UnityEngine;

namespace CardGame.Data
{
    [Serializable]
    public class CardData
    {
        [SerializeField]
        private string _identifier;
        public string Identifier => _identifier;

        [SerializeField]
        private Sprite _sprite;
        public Sprite Sprite => _sprite;

        [SerializeField]
        private float _rotationOffset;
        public float RotationOffset => _rotationOffset;

    }
}
