using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class InstructionsScreen : MonoBehaviour
{
    [SerializeField] GameObject previousMenu;
    [SerializeField] int waitTime = 10;
    [SerializeField] TMP_Text timerField;

    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - startTime;
        int remainingTime =  waitTime - (int)elapsedTime;

        timerField.text = remainingTime + "s";

        if (remainingTime == 0)
            Continue();
    }

    public void CallInstructionsScreen()
    {
        previousMenu.SetActive(false);
        startTime = Time.time;
        gameObject.SetActive(true);
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
    }
}
