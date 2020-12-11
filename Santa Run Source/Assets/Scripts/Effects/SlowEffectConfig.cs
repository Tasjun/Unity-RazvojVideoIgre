using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlowEffect", menuName = "Effects/Slow", order = 1)]
public class SlowEffectConfig : AEffectConfig
{
    [SerializeField]
    private float duration = 1.5f;
    [SerializeField]
    private float speedModifier = 0.5f;

    public override void ApplyEffect(Player player)
    {
        player.AddEffect(new SlowEffect(duration, speedModifier));
    }
}
