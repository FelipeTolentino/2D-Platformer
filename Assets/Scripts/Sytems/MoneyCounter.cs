using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyCounter : MonoBehaviour
{
    public static MoneyCounter instance;

    //[SerializeField] PlayerHealth playerHealth;
    //[SerializeField] TMP_Text money;
    //[SerializeField] EnemyHealth lastEnemy;

    int currentMoney = 0;
    int recordMoney = 0;
    bool updateDone;

    public int CurrentMoney { 
        get { return currentMoney; }
        set { currentMoney = value; }
    }

    public bool UpdateDone { 
        get { return updateDone; }
    }

    public int RecordMoney { 
        get { return recordMoney; }
        set { recordMoney = value; }
    }

    private void Awake()
    {
        //DontDestroyOnLoad(this);
        DontDestroyOnLoad(gameObject);
        //StartCoroutine(UpdateRecord());

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public int catchGold(int amount)
    {
        currentMoney += amount;
        //money.text = currentMoney.ToString();

        return currentMoney;
    }

    //IEnumerator UpdateRecord()
    //{
    //    updateDone = false;
    //    yield return new WaitUntil(() => playerHealth.Died || lastEnemy.Died); //************************************
    //    Debug.Log("Atualizando record");

    //    if (instance.currentMoney > instance.recordMoney)
    //        instance.recordMoney = instance.currentMoney;
    //    updateDone = true;
        
    //    Debug.Log("Atualização feita: " + updateDone + ", currentMoney" + currentMoney + ", recordMoney: " + recordMoney);
    //    Debug.Log("Atualização feita: " + updateDone + ", instance.currentMoney" + instance.currentMoney + ", instance.recordMoney: " + instance.recordMoney);
    //}
}
