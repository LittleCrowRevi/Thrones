namespace ThronesEra.Scripts.Entities.Components;

public class VitalStatsComponent : Component
{
    public VitalStatsComponent(int currentHp, int currentMp, int currentEp)
    {
        CurrentHp = currentHp;
        CurrentMp = currentMp;
        CurrentEp = currentEp;
    }

    #region Data Fields

    public int TotalHp { get; set; }
    public int CurrentHp { get; set; }

    public int TotalMp { get; set; }
    public int CurrentMp { get; set; }

    public int TotalEp { get; set; }
    public int CurrentEp { get; set; }

    #endregion
}