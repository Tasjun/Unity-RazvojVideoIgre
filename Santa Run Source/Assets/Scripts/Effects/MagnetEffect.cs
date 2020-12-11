using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffect : AbstractEffect
{
    private float duration = 0;
    public MagnetEffect(float duration)
    {
        this.duration = duration;
    }

    private float currentDuration = 0;

    public override void Tick(Player player, float deltaTime)
    {
        Collider[] coinPickUps = Physics.OverlapSphere(player.pController.transform.position, 10f, 1 << LayerMask.NameToLayer("Coins"));
        foreach (var coin in coinPickUps)
        {
            Pickup pickup = coin.gameObject.GetComponent<Pickup>();
            pickup.MagnetToPlayer(player.pController);
        }

        currentDuration += Time.deltaTime;

        if (currentDuration > duration)
        {
            player.RemoveEffect(this);
            return;
        }
    }
}
