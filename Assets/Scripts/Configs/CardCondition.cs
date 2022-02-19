using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CardCondition
{
    public enum LogicOperator { AND, OR };

    public LogicOperator logicOperator = LogicOperator.OR;
    public List<CardTypeCount> requirements;

    public bool SatisfiesRequirements(IEnumerable<Card> cards)
    {
        if (requirements.Count == 0)
        {
            return true;
        }

        System.Func<CardTypeCount, bool> predicate = r => r.CompareValues(
            cards
            .Where(c => r.CompareType(c.type))
            .Where(c => r.CompareAffinity(c.affinity))
            .Count());
        switch (logicOperator)
        {
            case LogicOperator.AND:
                return requirements.All(predicate);
            case LogicOperator.OR:
                return requirements.Any(predicate);
        }
        return false;
    }
}
