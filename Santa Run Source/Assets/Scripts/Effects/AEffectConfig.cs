using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEffectConfig : ScriptableObject
{
    public abstract void ApplyEffect(Player player);
}
