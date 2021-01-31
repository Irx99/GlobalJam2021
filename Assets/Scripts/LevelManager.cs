using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public PhoneTimer phoneTimer;
    public PlayerController playerController;
    public AudioSource phoneAudioSource;

    public Text introText;
    public GameObject pointer;

    public Image blackMomento;
    public IntroScript introScript;
    // Start is called before the first frame update
    void Start()
    {
        playerController.StopMovementInput();
        blackMomento.color = Color.black;
        StartCoroutine(Intro());
    }

    private IEnumerator Intro()
    {
        // Sistema patentado de esperar a que pasen cosas de adri
        // Si quereis hace una espera con bool es asi -> yield return new WaitUntil(() => bool)
        // "Pink Diamond" esta hecho con esto, Mauro deberia tener acceso al git

        introText.text = "A few days later...";
        yield return new WaitForSeconds(2);
        blackMomento.CrossFadeAlpha(0, 1f, true);
        yield return new WaitForSeconds(0.4f);
        introText.text = "3";
        yield return new WaitForSeconds(0.4f);
        introText.text = "2";
        yield return new WaitForSeconds(0.4f);
        introText.text = "1";
        phoneAudioSource.Play();
        yield return new WaitForSeconds(0.4f);
        introText.text = "Find your phone!";

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
        StartCoroutine(LoseCr());
    }

    IEnumerator LoseCr()
    {
        blackMomento.CrossFadeAlpha(1f, 1f, true);
        introText.text = "Game Over";
        yield return new WaitForSeconds(1f);

        introText.text = "Restarting";
        yield return new WaitForSeconds(0.3f);
        introText.text = "Restarting.";
        yield return new WaitForSeconds(0.3f);
        introText.text = "Restarting..";
        yield return new WaitForSeconds(0.3f);
        introText.text = "Restarting...";
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Win()
    {
        phoneTimer.StopTheCount();
        phoneAudioSource.Stop();
        introScript.enabled = true;
        //TODO que pasa cuando ganas
    }
}
