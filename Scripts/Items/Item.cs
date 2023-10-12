using System.Collections.Generic;

namespace ThronesEra.Scripts.Items;

public interface Item
{
    public string Name { get; set; }
    public string Descirption { get; set; }
    public ItemType ItemType { get; set; }
    public KeyValuePair<StatType, int>[] Stats { get; set; }
    public string[] Buffs { get; set; }
}

public enum StatType
{
    Intelligence = 0,
    Strength = 1,
    Constitution = 2
}

public enum ItemType
{
    Weapon = 0,
    Armor = 1,
    Necklace = 2,
    Ring = 3
}