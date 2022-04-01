using System.Collections;
using System.Linq;

public class AwardRollSouls : CardAction
{
    
    public override IEnumerator Execute()
    {
        yield return Game.RollDice();
        var rolls = Game.dice.Numbers;
        if (rolls[0] != rolls[1])
        {
            card.Owner.Souls += rolls.Aggregate((acc, x) => acc + x);
        }
        yield return ExecuteNext();
    }
}
