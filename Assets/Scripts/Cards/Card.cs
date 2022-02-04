using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Player Owner { get; set; }
    public bool IsBlockCard { get; set; }
    public bool FaceUp
    {
        set
        {
            animator.SetBool("FaceUp", value);
        }
    }
    public bool IsMoving { get { return cardTransform.IsMoving; } }
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

    public CardTransform cardTransform;
    public CardAffinity affinity;
    public CardType type;
    public string cardName;
    public List<int> triggerNumbers;
    public CardAction[] actions;

    public TMPro.TextMeshPro text;
    public List<TMPro.TextMeshPro> triggerNumberText;
    public MeshRenderer meshRenderer;
    public Animator animator;
    private GameController game;

    void OnValidate()
    {
        text.text = cardName;
        for (int i = 0; i < triggerNumberText.Count; i++)
        {
            var text = triggerNumberText[i];
            if (i < triggerNumbers.Count)
            {
                var value = triggerNumbers[i];
                text.text = value.ToString();
                text.transform.parent.gameObject.SetActive(true);
                continue;
            }
            text.text = "";
            text.transform.parent.gameObject.SetActive(false);

        }
        for (int i = 0; i < actions.Length - 1; i++)
        {
            actions[i].Next = actions[i + 1];
        }
    }

    void Start()
    {
        game = FindObjectOfType<GameController>();
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked card");
        Debug.Log(game);
        game.BuyBlockCardForActivePlayer(this);
    }

    public IEnumerator Execute(int roll)
    {
        if (CanExecute(roll))
        {
            yield return Execute();
        }
    }

    public IEnumerator Execute()
    {
        yield return actions[0].ExecuteChain();
    }

    public bool CanExecute(int roll)
    {
        return triggerNumbers.Contains(roll);
    }
}
