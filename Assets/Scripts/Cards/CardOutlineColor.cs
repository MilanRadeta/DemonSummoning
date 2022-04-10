using UnityEngine;

[RequireComponent(typeof(Card), typeof(Outline))]
public class CardOutlineColor : MonoBehaviour
{
    public Color selectable;
    public Color deselectable;
    private Card card;
    private Outline outline;

    void Start()
    {
        card = GetComponent<Card>();
        outline = GetComponent<Outline>();
    }

    void Update()
    {
        outline.enabled = card.IsClickable;
        var outlineColor = card.IsSelected ? this.deselectable : this.selectable;
        if (!outline.OutlineColor.Equals(outlineColor))
        {
            outline.OutlineColor = outlineColor;
        }
    }
}
