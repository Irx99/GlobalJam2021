using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public PhoneTimer phoneTimer;
    public PlayerController playerController;
    public AudioSource phoneAudioSource;

    public Text introText;
    public GameObject pointer;

    // Start is called before the first frame update
    void Start()
    {
        playerController.StopMovementInput();

        StartCoroutine(Intro());
    }

    private IEnumerator Intro()
    {
        // Sistema patentado de esperar a que pasen cosas de adri
        // Si quereis hace una espera con bool es asi -> yield return new WaitUntil(() => bool)
        // "Pink Diamond" esta hecho con esto, Mauro deberia tener acceso al git

        yield return new WaitForSeconds(1f);
        introText.text = "3";
        yield return new WaitForSeconds(1f);
        introText.text = "2";
        yield return new WaitForSeconds(1f);
        introText.text = "1";
        phoneAudioSource.Play();
        yield return new WaitForSeconds(1f);
        introText.text = "Find your phone";

        playerController.AllowMovementInput();
        phoneTimer.StartTimer();
        pointer.SetActive(true);

        yield return new WaitForSeconds(1f);
        introText.text = "";
    }

    public void GameOver()
    {
        playerController.StopMovementInput();
        phoneAudioSource.Stop();

        //TODO que pasa cuando pierdes
    }

    public void Win()
    {
        phoneTimer.StopTheCount();
        phoneAudioSource.Stop();

        //TODO que pasa cuando ganas
    }
}
