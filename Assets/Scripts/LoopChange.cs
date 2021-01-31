using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoopChange : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixerSnapshot[] roomSnapshots;
    public AudioSource changeLoopAudioSource;
    public AudioClip[] changeClipAudioSource;

    private void Start()
    {
        StartCoroutine(LogicScript());
    }

    private IEnumerator LogicScript()
    {
        while(true)
        {
            yield return new WaitForSeconds(7.93333333333f - 0.49583333333f);
            changeLoopAudioSource.PlayOneShot(changeClipAudioSource[Random.Range(0, changeClipAudioSource.Length)]);
            roomSnapshots[Random.Range(0, roomSnapshots.Length)].TransitionTo(0.49583333333f);
            yield return new WaitForSeconds(0.49583333333f);
        }
    }
}
