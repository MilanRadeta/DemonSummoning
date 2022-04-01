using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public static Card CurrentCard { get; set; }

    public Player Owner { get; set; }
    public bool IsBlockCard { get; set; }
    public bool FaceUp = false;
    public bool IsMoving { get { return cardTransform.IsMoving; } }
    public bool SatisfiesCondition { get { return condition.SatisfiesRequirements(Owner.OpenCards); } }
    public CardComponents components { get; private set; }
    public Vector3 TargetPosition
    {
        get { return cardTransform.TargetPosition; }
        set { cardTransform.TargetPosition = value; }
    }
    public Vector3 TargetRotation
    {
        get { return cardTransform.TargetRotation; }
        set { cardTransform.TargetRotation = value; }
    }
    public bool IsConfirmable { get; set; } = false;
    public bool Confirmed { get; set; } = false;
    public bool Selected { get; set; } = false;
    public bool Clickable { get { return OnClicked != null; } }
    public CardOutlineColor outlineColors;
    public CardTransform cardTransform;
    public CardAffinity affinity;
    public CardType type;
    public string cardName;
    [TextArea(5, 5)]
    public string description;
    public List<int> triggerNumbers;
    [NotNull]
    public CardAction startingAction;
    public CardCondition condition;

    public delegate void ClickAction(Card card);
    public event ClickAction OnClicked;

    public CardText cardText;
    public MeshRenderer meshRenderer;


    void OnValidate()
    {
        cardText.SetText(this);
    }

    void Awake()
    {
        components = new CardComponents(this);
        var validator = new CardValidator(this);
        validator.CheckForUnusedActions();
    }

    void Update()
    {
        components.Update();
    }

    public void CreateUiCopy(Transform parent)
    {
        var newCard = Instantiate(this.transform.GetChild(0));
        Layers.ChangeLayers(newCard.gameObject, LayerMask.NameToLayer(Layers.UI));
        var transform = newCard.transform;
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
    }

    public void OnPointerClick()
    {
        if (FaceUp && OnClicked != null)
        {
            OnClicked(this);
        }
    }

    public void OnPointerEnter()
    {
        if (FaceUp)
        {
            CardDescription.Instance.Show(this);
        }

    }

    public void OnPointerExit()
    {
        if (FaceUp)
        {
            CardDescription.Instance.Hide();
        }
    }

    public IEnumerator Execute()
    {
        Card.CurrentCard = this;
        yield return startingAction.Execute();
        Card.CurrentCard = null;
    }

    public bool CanExecute(int roll)
    {
        return triggerNumbers.Contains(roll) && (type != CardType.DEMON || Players.Instance.ActivePlayer == Owner);
    }
}
