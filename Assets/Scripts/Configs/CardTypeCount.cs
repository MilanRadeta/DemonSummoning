[System.Serializable]
public class CardTypeCount
{
    public enum RelativeOperator { EQUAL, LESS, GREATER, NOT_EQUAL, LESS_OR_EQUAL, GREATER_OR_EQUAL };

    public CardAffinity affinity;
    public CardType type;
    public RelativeOperator relativeOperator = RelativeOperator.GREATER_OR_EQUAL;
    public int count;

    public bool CompareType(CardType actual)
    {
        if (type == CardType.KID)
        {
            return CompareType(actual, CardType.BOY) || CompareType(actual, CardType.GIRL);
        }
        return CompareType(actual, type);
    }

    public bool CompareAffinity(CardAffinity actual)
    {
        return affinity == CardAffinity.NEUTRAL || actual == affinity;
    }

    public bool CompareType(CardType actual, CardType expected)
    {
        return actual == expected;
    }

    public bool CompareValues(int actual)
    {
        var negativeCheck = false;
        var value = false;

        switch (relativeOperator)
        {
            case CardTypeCount.RelativeOperator.NOT_EQUAL:
                negativeCheck = true;
                relativeOperator = CardTypeCount.RelativeOperator.EQUAL;
                break;
            case CardTypeCount.RelativeOperator.GREATER_OR_EQUAL:
                negativeCheck = true;
                relativeOperator = CardTypeCount.RelativeOperator.LESS;
                break;
            case CardTypeCount.RelativeOperator.LESS_OR_EQUAL:
                negativeCheck = true;
                relativeOperator = CardTypeCount.RelativeOperator.GREATER;
                break;
        }

        switch (relativeOperator)
        {
            case CardTypeCount.RelativeOperator.EQUAL:
                value = count == actual;
                break;
            case CardTypeCount.RelativeOperator.GREATER:
                value = count < actual;
                break;
            case CardTypeCount.RelativeOperator.LESS:
                value = count > actual;
                break;
        }
        return negativeCheck ? !value : value;
    }
}
