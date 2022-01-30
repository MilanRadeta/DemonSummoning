using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CardGenerator
{

    public static void RegenerateOnValidate(MonoBehaviour obj, IEnumerable<DeckConfig.CardCount> config, System.Action callback)
    {
        RegenerateOnValidate(obj?.transform, config, callback);
    }

    public static void RegenerateOnValidate(GameObject obj, IEnumerable<DeckConfig.CardCount> config, System.Action callback)
    {
        RegenerateOnValidate(obj?.transform, config, callback);
    }

    public static void RegenerateOnValidate(Transform transform, IEnumerable<DeckConfig.CardCount> config, System.Action callback)
    {
        if (transform != null && config?.Count() > 0)
        {
            UnityEditor.EditorApplication.delayCall += () =>
              {
                  if (transform == null)
                  {
                      return;
                  }
                  DeleteChildren(transform);
                  callback();
              };
        };
    }

    public static List<Card> GenerateCards(IEnumerable<DeckConfig.CardCount> config, bool faceUp = true)
    {
        var cards = new List<Card>();
        foreach (var item in config)
        {
            for (int i = 0; i < item.count; i++)
            {
                var card = MonoBehaviour.Instantiate(item.card);
                card.FaceUp = faceUp;
                cards.Add(card);
                card.gameObject.SetActive(true);
            }
        }
        return cards;
    }

    public static void DeleteChildren(Transform transform)
    {
        while (transform?.childCount > 0)
        {
            var child = transform.GetChild(0).gameObject;
            Remover.Destroy(child);
        }
    }


}
