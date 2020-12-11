using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEffect
{
    public abstract void Tick(Player player, float deltaTime);
}
