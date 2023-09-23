namespace ThronesEra.Scripts.Entities.Components;

public class CoreStatsComponent : Component
{
    #region Data Fields
    
    public int Intelligence { get; set; }
    public int CalculatedInt { get; set; }
    
    public int Strength { get; set; }
    public int CalculatedStrength { get; set; }
    
    public int Constitution { get; set; }
    public int CalculatedConst { get; set; }
    
    public int Agility { get; set; }
    public int CalculatedAgility { get; set; }

    #endregion
    
    #region Constructor
    
    public CoreStatsComponent(int intelligence, int strength, int constitution, int agility)
    {
        Intelligence = intelligence;
        Strength = strength;
        Constitution = constitution;
        Agility = agility;
    }

    #endregion
    
    #region Methods
    
    private int CalculateField()
    {
        return 0;
    }
    
    #endregion
}