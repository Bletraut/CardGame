using System;

using UnityEngine;

namespace CardGame.Data
{
    [Serializable]
    public class GridData
    {
        [SerializeField]
        private string _identifier;
        public string Identifier => _identifier;

        [SerializeField]
        private int _rows;
        public int Rows => _rows;

        [SerializeField]
        private int _columns;
        public int Columns => _columns;
    }
}