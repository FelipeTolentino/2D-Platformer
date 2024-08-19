using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] MoneyCounter moneyCounter;
    [SerializeField] TMP_Text currentMoney;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] MusicManager jukebox;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (!pauseMenu.activeSelf)
                Pause();
            else
                Unpause();
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        jukebox.Muted = true;
        SceneManager.LoadScene("MainMenu");
        MoneyCounter.instance.CurrentMoney = 0;
        
    }

    void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        currentMoney.text = MoneyCounter.instance.CurrentMoney.ToString() + " MOEDAS";
    }


}
