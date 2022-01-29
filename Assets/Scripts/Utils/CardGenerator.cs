using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CardGenerator
{

    public static void RegenerateOnValidate(MonoBehaviour obj, IEnumerable<DeckConfig.CardCount> config, System.Action callback)
    {
        if (obj != null && config?.Count() > 0)
        {

            UnityEditor.EditorApplication.delayCall += () =>
              {
                  if (obj == null)
                  {
                      return;
                  }
                  Delete(obj);
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

    public static void Delete(MonoBehaviour obj)
    {
        while (obj.transform.childCount > 0)
        {
            var child = obj.transform.GetChild(0).gameObject;
            Remover.Destroy(child);
        }
    }


}
