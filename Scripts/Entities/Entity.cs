using System.Collections.Generic;
using Godot;
using ThronesEra.Scripts.Entities.Components;

namespace ThronesEra.Scripts.Entities;

public interface IEntity 
{

    public IEnumerable<Component> QueryComponents();

}