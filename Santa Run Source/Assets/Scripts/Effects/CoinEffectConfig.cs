using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinEffect", menuName = "Effects/Coin", order = 1)]
public class CoinEffectConfig : AEffectConfig
{
    public override void ApplyEffect(Player player)
    {
        FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
        player.AddEffect(new CoinEffect());
    }
}
