using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Scriptable Objects/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    public Dictionary<Stat, float> Stats = new Dictionary<Stat, float>(); 

    public void SetStat(Stat stat, float amount)
    {
        if (Stats.TryGetValue(stat, out var value))
        {
            Stats[stat] += amount;
        }
        else
        {
            Debug.Log($"No stat {stat} found for character!");
        }
    }

    public void GetStat(Stat stat)
    {
        if (Stats.TryGetValue(stat, out var value))
        {
            Stats[stat] = value;
        }
        else
        {
            Debug.Log($"No stat {stat} found for character!");
        }
    }
}
