
using UnityEngine;

[RequireComponent(typeof(Card), typeof(Animator))]
public class CardAnimator : MonoBehaviour
{
    private Animator animator;
    private Card card;
    private readonly string FACE_UP_KEY = "FaceUp";

    void Start()
    {
        card = GetComponent<Card>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (animator.isActiveAndEnabled && card.FaceUp != animator.GetBool(FACE_UP_KEY))
        {
            animator.SetBool(FACE_UP_KEY, card.FaceUp);
        }
    }

}