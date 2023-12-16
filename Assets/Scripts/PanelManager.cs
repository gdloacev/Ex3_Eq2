using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    private bool gameIsPaused;
    [SerializeField] private GameObject
        winPanel, winText, losePanel, loseText, bloodFrame, uiPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("V");
            WinFuction();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L");
            LoseFuction();
        }
    }

    public void ReturnMenu(int index = 0) //Regresar a menú principal
    {
        SceneManager.LoadScene(index);
    }

    public void WinFuction() //Función de victoria
    {
        //Time.timeScale = 0;
        //Camera.main.GetComponent<AudioSource>().mute = true;
        winPanel.SetActive(true);
        LeanTween.scale(winText, new Vector3(0.9f, 0.9f, 0.9f), 2f).setDelay(0.1f).setEase(LeanTweenType.easeOutElastic);
        uiPanel.SetActive(false);
    }

    public void LoseFuction() //Función de derrota
    {
        //Time.timeScale = 0;
        //Camera.main.GetComponent<AudioSource>().mute = true;
        losePanel.SetActive(true);
        LeanTween.moveY(bloodFrame, 570f, 4.0f).setDelay(0.5f);
        LeanTween.scale(loseText, new Vector3(1f, 1f, 1f), 2f).setDelay(0.1f).setEase(LeanTweenType.easeOutQuart);
        uiPanel.SetActive(false);
    }
}
