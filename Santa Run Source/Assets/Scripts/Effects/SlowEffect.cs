using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SlowEffect : AbstractEffect
{
    private float currentDuration = 0f;
    private float oldPlayerSpeed = 0f;
    private bool initialized = false;

    private float duration;
    private float speedModifier;

    public SlowEffect(float duration, float speedModifier)
    {
        this.duration = duration;
        this.speedModifier = speedModifier;
    }

    public override void Tick(Player player, float deltaTime)
    {
        if (!initialized)
        {
            currentDuration = 0f;

            oldPlayerSpeed = player.PlayerSpeed;
            player.PlayerSpeed *= speedModifier;

            initialized = true;
        }

        currentDuration += deltaTime;

        if (currentDuration >= duration)
        {
            // Ovde je kraj
            initialized = false;

            player.PlayerSpeed = oldPlayerSpeed;
            player.RemoveEffect(this);
            return;
        }
    }
}
