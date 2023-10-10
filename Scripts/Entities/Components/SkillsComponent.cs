using System;
using System.Collections.Generic;

namespace ThronesEra.Scripts.Entities.Components;

public partial class SkillsComponent : Component
{
    public List<Skill> Skills { get; set; }

    public SkillsComponent(List<Skill> abilities)
    {
        Skills = abilities;
        Name = "SkillsComponent";
    }

    public void AddSkill(Skill skill)
    {
        Skills.Add(skill);
    }

    public void RemoveAbility(Skill skill)
    {
        Skills.Remove(skill);
    }
}

/// <summary>
/// Interface for Skills.
/// </summary>
public abstract class Skill
{
    public abstract string Name { get; set; }
    public abstract string Description { get; set; }
    public abstract SkillMode SkillMode { get; set; }
    public int SkillLevel { get; set; } = 0;
    public abstract List<int> DamageScaleLevels { get; set; }
    
    public void SetSkillLevel(int newSkillLevel)
    {
        // TODO: Proper Handling of max level, response back to UI that level is at max
        var dmgScaleLevels = DamageScaleLevels.Count;
        SkillLevel = Math.Clamp(newSkillLevel + 1, 0, dmgScaleLevels);
    }
}    
public enum SkillMode
     {
         Active = 0,
         Passive = 1
     }