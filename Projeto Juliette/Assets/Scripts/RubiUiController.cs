using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RubiUiController : MonoBehaviour
{
    [SerializeField] private TMP_Text rubiText;

    private void OnEnable()
    {
    	// Se inscreve no canal de rubis
        PlayerObserverManager.OnRubisChanged += UpdateRubiText;
    }

    private void OnDisable()
    {
		// Retira a inscrição no canal de rubis
        PlayerObserverManager.OnRubisChanged += UpdateRubiText;
    }

	// Função usada para tratar a notificação do canal de rubis
    private void UpdateRubiText(int newRubisValue)
    {
        rubiText.text = newRubisValue.ToString();
    }

}
