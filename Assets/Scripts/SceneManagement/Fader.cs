using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Coroutine currentActiveFade = null;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f);
            Debug.Log("Faded out");
            yield return FadeIn(1f);
            Debug.Log("Faded in");
        }

        public Coroutine FadeIn(float time)
        {
            return Fade(0f, time);
        }

        public Coroutine FadeOut(float time)
        {
            return Fade(1f, time);
        }

        public Coroutine Fade(float target, float time)
        {
            if (currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }
            currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentActiveFade;
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }



        private IEnumerator FadeInRoutine(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }

        public void FadeInImmediate()
        {
            canvasGroup.alpha = 0;
        }
    }

}