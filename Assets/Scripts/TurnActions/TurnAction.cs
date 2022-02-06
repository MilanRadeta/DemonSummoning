using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TurnAction : MonoBehaviour
{
    protected GameController Game { get { return GameController.Instance; } }
    private Material material;
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

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        material.color = CanExecute() ? Color.green : Color.red;
    }

    void OnMouseDown()
    {
        if (CanExecute())
        {
            coroutine = Execute();
            StartCoroutine(coroutine);
        }
    }

}
