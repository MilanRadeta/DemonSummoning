using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Player Owner { get; set; }
    public CardAffinity affinity;
    public CardType type;
    public string cardName;
    public List<int> triggerNumbers;
    public CardAction[] actions;

    public TMPro.TextMeshPro text;
    public List<TMPro.TextMeshPro> triggerNumberText;
    public MeshRenderer meshRenderer;

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
    }

    public void MoveToParent()
    {
        StartCoroutine(MoveAnimation(1));
    }
    public void FlipUp()
    {
        StartCoroutine(FlipUpAnimation(1));
    }

    private IEnumerator MoveAnimation(float duration) {
        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = Vector3.zero;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t / duration);
            yield return null;
        }
        transform.localPosition = endPosition;
    }

    private IEnumerator FlipUpAnimation(float duration) {
        Quaternion startRotation = transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(Vector3.zero);
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            transform.localRotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
            yield return null;
        }
        transform.localRotation = endRotation;
    }

    public void Execute(int roll)
    {
        if (CanExecute(roll))
        {
            Execute();
        }
    }

    public void Execute()
    {
        foreach (var action in actions)
        {
            if (!action.Execute())
            {
                break;
            }
        }
    }

    public bool CanExecute(int roll)
    {
        return triggerNumbers.Contains(roll);
    }
}
