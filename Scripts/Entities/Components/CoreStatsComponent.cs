namespace ThronesEra.Scripts.Entities.Components;

public partial class CoreStatsComponent : Component
{
    #region Constructor

    public CoreStatsComponent(int intelligence, int strength, int constitution, int agility)
    {
        Name = "CoreStats";
        Intelligence = intelligence;
        Strength = strength;
        Constitution = constitution;
        Agility = agility;
    }

    #endregion

    #region Data Fields

    public int Intelligence { get; set; }

    public int Strength { get; set; }

    public int Constitution { get; set; }

    public int Agility { get; set; }

    #endregion
}