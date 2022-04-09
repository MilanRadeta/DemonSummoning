using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardTransform), typeof(CardEventHandler))]
public class Card : MonoBehaviour
{
    public static Card CurrentCard { get; private set; }

    public string cardName;
    [TextArea(5, 5)]
    public string description;
    public bool FaceUp = false;
    public CardAffinity affinity;
    public CardType type;
    public List<int> triggerNumbers;
    [NotNull]
    public CardAction startingAction;
    public CardCondition condition;
    public CardOutlineColor outlineColors;
    public CardText cardText;
    public MeshRenderer meshRenderer;

    public event CardEventHandler.ClickAction OnClicked;

    public Player Owner { get; set; }
    public bool IsMoving { get { return components.CardTransform.IsMoving; } }
    public bool SatisfiesCondition { get { return condition.SatisfiesRequirements(Owner.OpenCards); } }
    public CardComponents components { get; private set; }
    public bool IsConfirmable { get; set; } = false;
    public bool IsConfirmed { get; set; } = false;
    public bool IsSelected { get; set; } = false;
    public bool IsClickable { get { return OnClicked != null; } }
    public Vector3 TargetPosition { set { components.TargetPosition = value; } }
    public Vector3 TargetRotation { set { components.TargetRotation = value; } }

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

    public void ExecuteOnClicked()
    {
        if (OnClicked != null)
        {
            OnClicked(this);
        }
    }
}
