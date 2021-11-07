using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace CardGame.Utils
{
    public static class TweenEffects
    {
        public static TweenerCore<Color, Color, ColorOptions> FadeOut(this Graphic graphic, float duration) => graphic.Fade(0, duration);
        public static TweenerCore<Color, Color, ColorOptions> FadeIn(this Graphic graphic, float duration) => graphic.Fade(1, duration);
        public static TweenerCore<Color, Color, ColorOptions> ForceFadeOut(this Graphic graphic, float duration)
        {
            graphic.FadeIn(0);
            return graphic.FadeOut(duration);
        }
        public static TweenerCore<Color, Color, ColorOptions> ForceFadeIn(this Graphic graphic, float duration)
        {
            graphic.FadeOut(0);
            return graphic.FadeIn(duration);
        }

        public static TweenerCore<Color, Color, ColorOptions> Fade(this Graphic graphic, float value, float duration)
        {
            graphic.DOKill(true);
            return graphic.DOFade(value, duration);
        }
    }
}
