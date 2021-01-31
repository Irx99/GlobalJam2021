using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class IntroScript : MonoBehaviour
{
    public Image blackPanel;
    public TextMeshPro txt;
    public GameObject screenQuad;
    public float charsPerSecond = 10f;
    public List<string> dialogues = new List<string>();
    public string nextSceneName;

    void Awake()
    {
        blackPanel.color = new Color(0,0,0,1);
    }

    IEnumerator Start()
    {
        blackPanel.CrossFadeAlpha(0, 2f, true);
        yield return new WaitForSeconds(2);
        foreach (string str in dialogues)
        {
            txt.text = "";
            foreach (char let in str)
            {
                txt.text += let;
                yield return new WaitForSeconds(1 / charsPerSecond);
            }
            yield return new WaitForSeconds(1);
        }
        screenQuad.SetActive(false);
        blackPanel.CrossFadeAlpha(1, 2f, true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nextSceneName);
    }
}
