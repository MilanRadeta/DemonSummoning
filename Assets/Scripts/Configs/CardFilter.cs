using System.Linq;

[System.Serializable]
public class CardFilter
{
    public CardType[] allowedTypes;
    public CardAffinity[] allowedAffinities;

    public bool IsAllowed(Card card)
    {
        return IsAllowed(allowedTypes, card.type) &&
                IsAllowed(allowedAffinities, card.affinity);
    }

    private bool IsAllowed<T>(T[] list, T value)
    {
        return list.Count() == 0 || list.Contains(value);
    }
}
