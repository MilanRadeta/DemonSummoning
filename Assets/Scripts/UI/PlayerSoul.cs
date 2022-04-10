using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSoul : MonoBehaviour
{

    public Player Player
    {
        get { return player; }
        set
        {
            player = value;
            souls = player.Souls;
            nameText.text = player.name;
            soulsText.text = soulsPrefix + player.Souls;
        }
    }
    public RectTransform rectTransform { get; private set; }
    public Text nameText;
    public Text soulsText;
    public string soulsPrefix;
    private readonly float textChangeSpeedInSeconds = 0.25f;
    private Player player;
    private int souls;
    private Image image;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }
        UpdateSouls();
        image.color = player == Players.Instance.ActivePlayer ? Color.green : Color.white;

    }

    public void Init() 
    {
        image = image ?? GetComponent<Image>();
        rectTransform = rectTransform ?? GetComponent<RectTransform>();
    }

    public void SetTransform(float posY)
    {
        this.transform.SetParent(PlayerSouls.Instance.transform);
        rectTransform.localScale = Vector3.one;
        rectTransform.localEulerAngles = Vector3.zero;
        rectTransform.anchoredPosition3D = new Vector3(0, posY, 0);
        rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y);
        rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y);
    }

    private void UpdateSouls()
    {
        if (player.Souls != souls)
        {
            var diff = Mathf.RoundToInt((player.Souls - souls) * Time.deltaTime / textChangeSpeedInSeconds);
            souls += Math.Max(1, Math.Abs(diff)) * Math.Sign(player.Souls - souls);
            soulsText.text = soulsPrefix + (int)souls;
        }

    }

}
