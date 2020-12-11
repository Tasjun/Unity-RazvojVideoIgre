using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : AbstractEffect
{
    private float duration = 0;
    public ShieldEffect(float duration)
    {
        this.duration = duration;
    }

    private float currentDuration = 0;
    private bool initialized = false;

    public override void Tick(Player player, float deltaTime)
    {
        if (!initialized)
        {
            player.IsShielded = true;
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Obstacles"), LayerMask.NameToLayer("Player"), true);
        }

        currentDuration += Time.deltaTime;

        if (currentDuration > duration)
        {
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Obstacles"), LayerMask.NameToLayer("Player"), false);
            player.RemoveEffect(this);
            player.IsShielded = false;
            return;
        }
    }
}
