using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardText))]
[RequireComponent(typeof(CardValidator))]
[RequireComponent(typeof(CardAnimator))]
[RequireComponent(typeof(CardTransform))]
[RequireComponent(typeof(CardEventHandler))]
[RequireComponent(typeof(CardOutlineColor))]
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
    public MeshRenderer meshRenderer;

    public event CardEventHandler.ClickAction OnClicked;

    public Player Owner { get; set; }
    public bool IsMoving { get { return cardTransform.IsMoving; } }
    public bool SatisfiesCondition { get { return condition.SatisfiesRequirements(Owner.OpenCards); } }
    public bool IsConfirmable { get; set; } = false;
    public bool IsConfirmed { get; set; } = false;
    public bool IsSelected { get; set; } = false;
    public bool IsClickable { get { return OnClicked != null; } }

    private CardTransform cardTransform;

    void Awake()
    {
        cardTransform = GetComponent<CardTransform>();
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

    public void SetTransform(Transform root, Vector3 position, Vector3 rotation)
    {
        cardTransform.SetTransform(root, position, rotation);
    }
}
