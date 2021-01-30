using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private AudioSource source;
    [SerializeField] private AudioClip startClip;
    [SerializeField] private AudioClip repeatClip;
    [SerializeField] private AudioClip endClip;

    public static UnityAction OnRepeatMusicStart;

    private void Awake() {
        source = GetComponent<AudioSource>();
        source.clip = startClip;
        source.Play();
        StartCoroutine(WaitToStartRepeat());
    }

    private IEnumerator WaitToStartRepeat() {
        //while (source.time != source.clip.length) { // TODO
        //    yield return null;
        //}
        yield return new WaitUntil(() => source.time > source.clip.length - 0.03f);
        source.clip = repeatClip;
        source.loop = true;
        source.Play();
        OnRepeatMusicStart?.Invoke();
    }
}
