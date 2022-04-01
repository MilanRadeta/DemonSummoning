using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    private TurnAction[] buttons;

    void OnValidate()
    {
        buttons = GetComponentsInChildren<TurnAction>();
        PositionButtons();
    }

    void Start()
    {
        buttons = GetComponentsInChildren<TurnAction>();
        PositionButtons();
    }

    void PositionButtons()
    {
        var buttonHeight = 60;
        var startY = (buttons.Length - 1) / 2f * buttonHeight;
        var i = 0;
        foreach (var button in buttons)
        {
            var rectTransform = button.GetComponent<RectTransform>();
            rectTransform.anchoredPosition3D = new Vector3(0, startY - buttonHeight * i, 0);
            i++;
        }

    }

}
