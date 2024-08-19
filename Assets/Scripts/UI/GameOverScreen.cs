using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] MoneyCounter moneyCounter;

    [SerializeField] TMP_Text currentMoney;
    [SerializeField] TMP_Text recordMoney;
    [SerializeField] MusicManager jukebox;

    //public IEnumerator CallGameOverScreen()
    //{
    //    Debug.Log("Record atualiazdo? "+moneyCounter.Up);
    //    yield return new WaitUntil(() => moneyCounter.UpdateDone);
    //    gameObject.SetActive(true);
    //    currentMoney.text = moneyCounter.CurrentMoney.ToString() + " MOEDAS";
    //    recordMoney.text = moneyCounter.RecordMoney.ToString() + " MOEDAS";
    //}

    public void CallGameOverScreen()
    {
        gameObject.SetActive(true);
        //currentMoney.text = moneyCounter.CurrentMoney.ToString() + " MOEDAS";
        //recordMoney.text = moneyCounter.RecordMoney.ToString() + " MOEDAS";
        currentMoney.text = MoneyCounter.instance.CurrentMoney.ToString() + " MOEDAS";
        recordMoney.text = MoneyCounter.instance.RecordMoney.ToString() + " MOEDAS";
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        //moneyCounter.CurrentMoney = 0;
        MoneyCounter.instance.CurrentMoney = 0;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //moneyCounter.CurrentMoney = 0;
        MoneyCounter.instance.CurrentMoney = 0;

        jukebox.Muted = true;

    }
}
