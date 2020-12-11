using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public PlayerController pController;
    private int numberOfCoins = 0;
    public int NumberOfCoins
    {
        get
        {
            return numberOfCoins;
        }
        set
        {
            numberOfCoins = value;

            // Update visuals
            PlayerManager.numberOfCoins = numberOfCoins;
        }
    }

    public float PlayerSpeed { get; set; }

    public bool IsShielded { get; set; }

    public void Tick(float deltaTime)
    {
        // Remove effects
        foreach (var toRemove in effectsToRemove)
        {
            effects.Remove(toRemove);
        }

        effectsToRemove.Clear();

        // Add effects
        foreach (var toAdd in effectsToAdd)
        {
            effects.Add(toAdd);
        }

        effectsToAdd.Clear();

        // Update all effects
        foreach (var effect in effects)
        {
            effect.Tick(this, deltaTime);
        }
    }

    private List<AbstractEffect> effects = new List<AbstractEffect>();
    private List<AbstractEffect> effectsToRemove = new List<AbstractEffect>();
    private List<AbstractEffect> effectsToAdd = new List<AbstractEffect>();

    public void AddEffect(AbstractEffect effect)
    {
        effects.Add(effect);
    }

    public void RemoveEffect(AbstractEffect effect)
    {
        effectsToRemove.Add(effect);
    }

    public void AddEffectM(AbstractEffect effect)
    {
        effectsToAdd.Add(effect);
    }
}
