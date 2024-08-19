 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDClass : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] TMP_Text money;
    [SerializeField] EnemyHealth lastEnemy;

    int dinheiroAtual;
    int ultimoValor;

    // Start is called before the first frame update
    void Start()
    {
        dinheiroAtual = MoneyCounter.instance.CurrentMoney;
        ultimoValor = dinheiroAtual;
        AtualizarCurrentMoney();
    }

    // Update is called once per frame
    void Update()
    {
        dinheiroAtual = MoneyCounter.instance.CurrentMoney;
        if (ultimoValor < dinheiroAtual)
        {
            ultimoValor = dinheiroAtual;
            AtualizarCurrentMoney();
        }
        if (playerHealth.Died || lastEnemy.Died)
        {
            if (MoneyCounter.instance.CurrentMoney > MoneyCounter.instance.RecordMoney)
                MoneyCounter.instance.RecordMoney = MoneyCounter.instance.CurrentMoney;
        }
            
    }

    void AtualizarCurrentMoney()
    {
        money.text = MoneyCounter.instance.CurrentMoney.ToString();
    }


    //IEnumerator UpdateRecord()
    //{
    //    //updateDone = false;
    //    yield return new WaitUntil(() => playerHealth.Died || lastEnemy.Died); //************************************
    //    Debug.Log("Atualizando record");

    //    if (MoneyCounter.instance.CurrentMoney > MoneyCounter.instance.RecordMoney)
    //        MoneyCounter.instance.RecordMoney = MoneyCounter.instance.CurrentMoney;
    //    //updateDone = true;

    //    //Debug.Log("Atualização feita: " + updateDone + ", currentMoney" + currentMoney + ", recordMoney: " + recordMoney);
    //    //Debug.Log("Atualização feita: " + updateDone + ", instance.currentMoney" + instance.currentMoney + ", instance.recordMoney: " + instance.recordMoney);
    //}
}
