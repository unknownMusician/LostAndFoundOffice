using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private AudioSource source;
    [SerializeField] private AudioClip startClip;
    [SerializeField] private AudioClip repeatClip;
    [SerializeField] private AudioClip endClip;

    private void Awake() {
        source = GetComponent<AudioSource>();
        source.clip = startClip;
        source.Play();
        StartCoroutine(WaitToStartRepeat());
    }

    private void Update() {
        print($"{source.time == source.clip.length}");
    }

    private IEnumerator WaitToStartRepeat() {
        //while (source.time != source.clip.length) { // TODO
        //    yield return null;
        //}
        yield return new WaitUntil(() => source.time == source.clip.length);
        print("FUCK");
        source.clip = repeatClip;
        source.loop = true;
        source.Play();
    }
}
