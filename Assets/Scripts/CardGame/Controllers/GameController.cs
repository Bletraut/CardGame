using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;

using CardGame.Data;
using CardGame.Objects;

namespace CardGame.Controllers
{
    public class GameController : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField]
        private GameGrid grid;
        [SerializeField]
        private Deck deck;
        [SerializeField]
        private GameObject winEffect;

        [Header("Game data")]
        [SerializeField]
        private LevelsBundleData _levelsBundle;
        [SerializeField]
        private CardBundleData[] _cardBundles;

        [Header("Events")]
        public UnityEvent OnGameStarted;
        public StringEvent OnTargetChanged;
        public UnityEvent OnGameOver;

        public UnityEvent OnGameLoading;
        public UnityEvent OnGameLoaded;

        public enum GameStates
        {
            Play,
            LoadingLevel,
            GameOver
        }
        public GameStates GameState { get; private set; } = GameStates.Play;

        private int currentLevelIndex = 0;
        private GridData currentGridData;
        private List<Card> currentCards = new List<Card>();
        private string targetIdentifier = "";

        private float loadingLevelDelay = 1f;

        // Use this for initialization
        void Start()
        {
            LoadCurrentLevel();
            currentCards.ForEach(n => n.AppearEffect());

            OnGameStarted?.Invoke();
        }

        private void OnCardPicked(Card sender)
        {
            if (GameState != GameStates.Play) return;

            var isWin = sender.Identifier == targetIdentifier;
            if (isWin)
            {
                sender.BounceEffect();
                winEffect.SetActive(true);
                winEffect.transform.position = sender.transform.position;

                NextLevel();
            }
            else
            {
                sender.ShakeEffect();
            }
        }

        private void LoadCurrentLevel()
        {
            GameState = GameStates.Play;

            UpdateGrid();
            currentCards.ForEach(n => n.OnPicked.RemoveAllListeners());

            currentCards = GetNewLevelCards();
            currentCards.ForEach(n => n.OnPicked.AddListener(OnCardPicked));
            grid.Fill(currentCards);

            targetIdentifier = deck.ValidCardIdentifier;
            OnTargetChanged?.Invoke(targetIdentifier);

            OnGameLoaded?.Invoke();
        }
        private void NextLevel()
        {
            if (GameState != GameStates.Play) return;

            GameState = GameStates.LoadingLevel;
            StartCoroutine(NextLevelCor());
        }
        private IEnumerator NextLevelCor()
        {
            yield return new WaitForSeconds(loadingLevelDelay);

            currentLevelIndex++;
            if (currentLevelIndex < _levelsBundle.GridsData.Length) LoadCurrentLevel();
            else GameOver();
        }
        private void GameOver()
        {
            if (GameState == GameStates.GameOver) return;

            GameState = GameStates.GameOver;
            OnGameOver?.Invoke();
        }

        private CardBundleData GetRandomCardBundle() => _cardBundles[Random.Range(0, _cardBundles.Length)];
        private List<Card> GetNewLevelCards() => deck.GetRandomCards(GetRandomCardBundle(), currentGridData.Rows * currentGridData.Columns);
        private void UpdateGrid()
        {
            currentGridData = _levelsBundle.GridsData[currentLevelIndex];
            grid.SetGridData(currentGridData);
        }

        public void Restart()
        {
            if (GameState != GameStates.GameOver) return;

            GameState = GameStates.LoadingLevel;
            OnGameLoading?.Invoke();

            StartCoroutine(RestartCor());
        }
        private IEnumerator RestartCor()
        {
            yield return new WaitForSeconds(loadingLevelDelay * 2);

            currentLevelIndex = 0;
            deck.ResetExcludedCardIdentifiers();
            Start();
        }
    }

    [System.Serializable]
    public class StringEvent : UnityEvent<string> { }
}