using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class TurnAction : MonoBehaviour
{
    public static bool HasCurrentAction { get { return currentAction != null; } }
    public static bool IsCurrentActionCancelable { get { return HasCurrentAction && currentAction.IsCancelable; } }
    private static TurnAction currentAction = null;
    
    public bool IsCancelable;
    protected GameController Game { get { return GameController.Instance; } }
    protected Image image;

    public static void Cancel()
    {
        currentAction.StopAllCoroutines();
        currentAction.OnCancel();
        currentAction = null;
    }

    public virtual void OnCancel()
    {

    }

    public virtual bool CanExecute()
    {
        return Game.Actions.Contains(this) && !Game.IsAnythingMoving() && TurnAction.currentAction == null;
    }

    public virtual IEnumerator Execute()
    {
        Game.Actions.Remove(this);
        yield return this;
        currentAction = null;
    }

    public virtual void OnClicked()
    {
        if (CanExecute())
        {
            currentAction = this;
            StartCoroutine(Execute());
        }
    }

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.color = CanExecute() ? Color.green : Color.red;
    }

}
