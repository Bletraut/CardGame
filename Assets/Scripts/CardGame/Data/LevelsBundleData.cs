using System.Collections;

using UnityEngine;

namespace CardGame.Data
{
    [CreateAssetMenu(fileName = "New LevelsBundleData", menuName = "Card Game/Levels Bundle Data", order = 11)]
    public class LevelsBundleData : ScriptableObject
    {
        [SerializeField]
        private GridData[] _gridsData;
        public GridData[] GridsData => _gridsData;
    }
}