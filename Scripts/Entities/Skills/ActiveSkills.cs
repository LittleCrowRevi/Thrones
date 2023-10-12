using System.Collections.Generic;
using ThronesEra.Scripts.Entities.Components;

namespace ThronesEra.Scripts.Skills;

public static class ActiveSkills
{
    public class Fireball : Skill
    {
        public override string Name { get; set;  } = "Fireball";
        public override string Description { get; set; } = "Fires a ball of fire at the target";
        public override SkillMode SkillMode { get; set; } = SkillMode.Active;

        public override List<int> DamageScaleLevels { get; set; } = new()
        {
            100,
            110,
            120,
            150
        };
    }
}