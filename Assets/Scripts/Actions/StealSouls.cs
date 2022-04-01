using System.Collections;
using System.Linq;

public class StealSouls : AwardSouls
{

    public override IEnumerator Execute()
    {
        var otherPlayers = Players.Instance.GetOtherPlayers(card.Owner);
        for (int i = 0; i < soulsToAward; i++)
        {
            otherPlayers = otherPlayers.Where(p => p.Souls > 0);
            Player player = null; // TODO choose player's stack
            if (player == null) 
            {
                break;
            }
        }
        yield return base.Execute();
    }
}
