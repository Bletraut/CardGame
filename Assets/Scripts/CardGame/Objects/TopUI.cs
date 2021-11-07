using UnityEngine;
using UnityEngine.UI;

using TMPro;

using CardGame.Utils;

namespace CardGame.Objects
{
    public class TopUI : MonoBehaviour
    { 
        [SerializeField]
        private TMP_Text missionText;
        [SerializeField]
        private Image blackOverlay;
        [SerializeField]
        private Button restartBtn;
        [SerializeField]
        private Image loadingLayout;

        private float defaultBlackOverlayValue;

        void Start()
        {
            defaultBlackOverlayValue = blackOverlay.color.a;
        }

        public void UpdateMissionText(string text)
        { 
            missionText.text = $"Find {text}";
        }

        public void ShowMissionText() => missionText.ForceFadeIn(1);
        public void HideMissionText() => missionText.ForceFadeIn(1);

        public void ShowGameOverLayout()
        {
            blackOverlay.gameObject.SetActive(true);
            blackOverlay.FadeOut(0);
            blackOverlay.Fade(defaultBlackOverlayValue, 1);

            restartBtn.gameObject.SetActive(true);
        }
        public void HideGameOverLayout()
        {
            blackOverlay.gameObject.SetActive(false);
            restartBtn.gameObject.SetActive(false);
        }

        public void ShowLoadingLayout()
        {
            loadingLayout.gameObject.SetActive(true);
            loadingLayout.ForceFadeIn(1);
        }
        public void HideLoadingLayout()
        {
            HideGameOverLayout();
            loadingLayout.gameObject.SetActive(false);
        }
    }
}