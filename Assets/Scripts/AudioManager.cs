using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Range(0,1)]
    public float MusicVolume;
    [Range(0, 1)]
    public float GameVolume;
    //public GameObject audioPrefab;

    AudioPool audioPool;

    public AudioClip lastDuelMusicClip;

    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogWarning("More than one instance of audiomanger found.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        audioPool = AudioPool.instance;
        PlaySound(lastDuelMusicClip, 1 * MusicVolume, "Music", true);
    }


    public void PlaySound(AudioClip clip, float Volume, string tag, bool StopWhenClipEnds = default(bool))
    {
        float _Volume = GameVolume * Volume;

        GameObject go = AudioPool.instance.SpawnFromPool(tag, transform.position, Quaternion.identity);
        AudioSource audiosource = go.GetComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.volume = _Volume;
        audiosource.Play();
        
        if (StopWhenClipEnds == default(bool))
        {
        StartCoroutine(WaitForClipToEnd(clip.length + 0.1f, go));
            Debug.Log("test");
        }
    }

    public void PlaySoundRandomPitch(AudioClip clip, float Volume, float minPitch, float maxPitch, string tag, bool StopWhenClipEnds = default(bool))
    {
        float _Volume = GameVolume * Volume;

        GameObject go = AudioPool.instance.SpawnFromPool(tag, transform.position, Quaternion.identity);
        AudioSource audiosource = go.GetComponent<AudioSource>();
        audiosource.pitch = Random.Range(minPitch, maxPitch);
        audiosource.clip = clip;
        audiosource.volume = _Volume;
        audiosource.Play();

        if (StopWhenClipEnds == default(bool))
            StartCoroutine(WaitForClipToEnd(clip.length + 0.1f, go));
    }
    IEnumerator WaitForClipToEnd(float time, GameObject go)
    {

        yield return new WaitForSeconds(time);
        go.SetActive(false);
    }


}
