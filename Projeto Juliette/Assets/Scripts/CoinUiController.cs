using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUiController : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;

    private void OnEnable()
    {
        // Se inscreve no canal de coins
        PlayerObserverManager.OnCoinsChanged += UpdateCoinText;
    }

    private void OnDisable()
    {
        // Retira a inscrição no canal de coins
        PlayerObserverManager.OnCoinsChanged += UpdateCoinText;
    }
    
    // Função usada para tratar a notificação do canal de coins
    private void UpdateCoinText(int newCoinsValue)
    {
        coinText.text = newCoinsValue.ToString();
    }
}
