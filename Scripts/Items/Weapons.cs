using System.Collections.Generic;

namespace ThronesEra.Scripts.Items;

public class Weapons
{
    public class SimpleSword : Item
    {
        public string Name { get; set; } = "Simple Sword";
        public string Descirption { get; set; } = "A simple sword.";
        public ItemType ItemType { get; set; } = ItemType.Weapon;

        public KeyValuePair<StatType, int>[] Stats { get; set; } =
        {
            new(StatType.Strength, 5),
        };
        
        public string[] Buffs { get; set; }
    }
}