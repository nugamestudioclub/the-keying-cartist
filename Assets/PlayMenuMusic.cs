using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuMusic : MonoBehaviour
{
    [SerializeField]
    private AudioSource openingAudioSource;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip musicOpening;

    [SerializeField]
    private AudioClip musicLoop;

    private float openingLength;

    // Start is called before the first frame update
    void Start()
    {
        openingAudioSource.clip = musicOpening;
        audioSource.clip = musicLoop;
        openingLength = musicOpening.length;
        Debug.Log(openingLength);
        openingAudioSource.Play();
        StartCoroutine(PlayMainMenuMusic());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PlayMainMenuMusic()
    {
        yield return new WaitForSecondsRealtime(openingLength + 0.25f);
        audioSource.Play();
    }
}
