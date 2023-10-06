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
    
}    
public enum SkillMode
     {
         Active = 0,
         Passive = 1
     }