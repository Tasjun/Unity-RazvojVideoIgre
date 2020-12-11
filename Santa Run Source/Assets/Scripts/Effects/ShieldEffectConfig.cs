using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldEffect", menuName = "Effects/Shield", order = 1)]
public class ShieldEffectConfig : AEffectConfig
{
    [SerializeField]
    private float duration = 1.0f;

    public override void ApplyEffect(Player player)
    {
        player.AddEffect(new ShieldEffect(duration));
    }
}
