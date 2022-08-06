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
        PlayerObserverManager.OnRubisChanged += UpdateRubiText;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnRubisChanged += UpdateRubiText;
    }

    private void UpdateRubiText(int newRubisValue)
    {
        rubiText.text = newRubisValue.ToString();
    }

}
