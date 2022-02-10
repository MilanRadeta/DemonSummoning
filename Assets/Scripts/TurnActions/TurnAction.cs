using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class TurnAction : MonoBehaviour
{
    protected GameController Game { get { return GameController.Instance; } }
    private Image image;
    private static IEnumerator coroutine = null;

    public virtual bool CanExecute()
    {
        return Game.Actions.Contains(this) && !Game.IsAnythingMoving() && TurnAction.coroutine == null;
    }

    public virtual IEnumerator Execute()
    {
        Game.Actions.Remove(this);
        yield return this;
        coroutine = null;
    }

    public void OnClicked()
    {
        if (CanExecute())
        {
            coroutine = Execute();
            StartCoroutine(coroutine);
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
