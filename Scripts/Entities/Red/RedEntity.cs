using Godot;

namespace ThronesEra.Scripts.Entities.Player
{
    public partial class RedEntity : CharacterBody2D
    {
        public override void _Ready()
        {
            Name = "Red";
            Position = new Vector2(40f, 50f);
        }
    }
}