using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : AbstractEffect
{
    [SerializeField]
    private int points = 1;

    public override void Tick(Player player, float deltaTime)
    {
        player.NumberOfCoins += points;
        //FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
        player.RemoveEffect(this);
    }
}
