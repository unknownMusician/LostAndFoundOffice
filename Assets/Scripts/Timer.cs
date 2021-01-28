using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public sealed class Timer : MonoBehaviour {

    [SerializeField] private TextMesh Time = default;
    [SerializeField] private Material timerMaterial = default;

    public UnityAction TimeOver;

    private static Timer _timer = null;

    private Timer() { }

    public static Timer GetSingleton() {
        if (_timer == null) { _timer = new Timer(); }
        return _timer;
    }

    public void StartTimer(float seconds) => StartCoroutine(TimerTick(seconds));

    private void Tick(float currentTime, float startTime) {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        Time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerMaterial.SetFloat("Lerp", currentTime / startTime);
    }

    private IEnumerator TimerTick(float startTime) {
        for (float currentTime = startTime; currentTime > 0; currentTime -= UnityEngine.Time.deltaTime) {
            Tick(currentTime, startTime);
            yield return null;
        }
        Tick(0, startTime);
        TimeOver?.Invoke();
    }

    private void OnDestroy() { timerMaterial.SetFloat("Lerp", 0); }
}