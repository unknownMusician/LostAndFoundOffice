using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace UI {
    public sealed class Timer : MonoBehaviour {

        #region Instance

        private Coroutine ticking = null;

        public static Timer instance { get; set; } = null;
        private void Awake() {
            instance = this;
            DataManager.Manager.Fin += () => {
                timeTextMesh.text = "";
                if (ticking != null) { StopCoroutine(ticking); }
            };
        }
        private void OnDestroy() {
            instance = null;
            timerMaterial.SetFloat("Lerp", 0);
        }

        #endregion

        [SerializeField] private TextMesh timeTextMesh = default;
        [SerializeField] private Material timerMaterial = default;

        public static UnityAction TimeOver;

        public void StartTimer(float seconds) => ticking = StartCoroutine(TimerTick(seconds));

        private void Tick(float currentTime, float startTime) {
            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);
            timeTextMesh.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerMaterial.SetFloat("Lerp", currentTime / startTime);
        }

        private IEnumerator TimerTick(float startTime) {
            for (float currentTime = startTime; currentTime > 0; currentTime -= UnityEngine.Time.deltaTime) {
                Tick(currentTime, startTime);
                yield return null;
            }
            Tick(0, startTime);
            TimeOver?.Invoke();
            ticking = null;
        }
    }
}