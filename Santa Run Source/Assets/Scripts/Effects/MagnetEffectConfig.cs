using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagnetEffect", menuName = "Effects/Magnet", order = 1)]
public class MagnetEffectConfig : AEffectConfig
{
    [SerializeField]
    private float duration = 1.0f;

    public override void ApplyEffect(Player player)
    {
        player.AddEffect(new MagnetEffect(duration));
    }
}
