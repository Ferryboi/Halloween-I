using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoundSystem", menuName = "Data/RoundSystem/Extreme")]
public class ExtremeRoundSystem : EndlessRoundSystem
{
    protected override void SystemSetup()
    {
        base.SystemSetup();

        List<Tombstone> tombstones = TombstoneManager.Instance.FindAllActiveTombstones();
        for(int i = 0; i < tombstones.Count; i++)
        {
            tombstones[i].DestroyTombstone();
        }
    }
}
