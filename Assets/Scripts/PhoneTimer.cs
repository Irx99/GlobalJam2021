using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PhoneTimer : MonoBehaviour
{
    public LevelManager levelManager;

    public Image timeBar;
    public float levelTime;
    public bool counting;
    public AnimationCurve curvaDeTrucarElTiempo;
    float timeRemaining;
    float timeBarStartScale;
    Vector3 aux;

    private void Awake()
    {
        timeRemaining = levelTime;
        timeBarStartScale = timeBar.rectTransform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining <= 0)
        {
            levelManager.GameOver();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        if (counting)
        {
            timeRemaining -= Time.deltaTime;

            aux = timeBar.rectTransform.localScale;
            aux.x = Mathf.Lerp(0, timeBarStartScale, curvaDeTrucarElTiempo.Evaluate(1 - timeRemaining / levelTime));
            timeBar.color = Color.Lerp(Color.red, Color.white, curvaDeTrucarElTiempo.Evaluate(1- timeRemaining / levelTime));
            timeBar.transform.localScale = aux;
        }
    }

    public void StartTimer()
    {
        counting = true;
    }

    public void StopTheCount()
    {
        counting = false;
    }
}
