using UnityEngine;

public class Buttons : MonoBehaviour
{
    private TurnAction[] buttons;

    void OnValidate()
    {
        Init();
    }

    void Start()
    {
        Init();
    }

    void Init()
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
