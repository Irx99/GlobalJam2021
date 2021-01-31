using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public AudioSource launchSource;
    public AudioClip[] lowHits, midHits, highHits;
    public enum Size { SMALL, MEDIUM, HUGE }
    public Size mySize;

    private float timeStoped = 0;
    private bool isSetup = false;

    private void Update()
    {
        if(isSetup)
        {
            if(this.GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
            {
                timeStoped += Time.deltaTime;

                if(timeStoped > 3)
                {
                    Destroy(this.GetComponent<CollisionSound>());
                }
            }
            else
            {
                timeStoped = 0;
            }
        }
    }

    public void Setup(AudioSource source, AudioClip[] lows, AudioClip[] mids, AudioClip[] highs, Size size)
    {
        launchSource = source;

        lowHits = lows;
        midHits = mids;
        highHits = highs;
        
        mySize = size;

        isSetup = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (mySize == Size.SMALL)
        {
            launchSource.PlayOneShot(highHits[UnityEngine.Random.Range(0, highHits.Length)]);
        }
        else if (mySize == Size.HUGE)
        {
            launchSource.PlayOneShot(lowHits[UnityEngine.Random.Range(0, lowHits.Length)]);
        }
        else
        {
            launchSource.PlayOneShot(midHits[UnityEngine.Random.Range(0, midHits.Length)]);
        }
    }
}
