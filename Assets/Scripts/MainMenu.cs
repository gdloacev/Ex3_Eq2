using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject monoRojo;
    [SerializeField] private GameObject monoAmarillo;
    [SerializeField] private GameObject monoAzul;

    private void Start()
    {
        // LeanTween.scale(title, new Vector3(1, 1, 1),1).setEase(LeanTweenType.easeInExpo).setDelay(1);
        LeanTween.scale(title, new Vector3(1, 1, 1), 2f).setLoopType(LeanTweenType.pingPong);
        /*var sequence = LeanTween.sequence();
        sequence.append(LeanTween.moveX(monoAmarillo, 1800f, 1.0f));
        sequence.append(LeanTween.moveY(monoAzul, 350.0f, 0.5f));
        sequence.append(LeanTween.moveX(monoAzul, 50.0f, 0.5f));
        sequence.append(LeanTween.moveY(monoRojo, 50.0f, 1f).setEase(LeanTweenType.easeOutQuart));*/
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
