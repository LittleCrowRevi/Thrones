using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thrones.Scripts.States
{
    public abstract partial class IState : Node
    {
        public string Name { get; }
        public abstract StateManager StateManager { get; set; }
        public abstract GameManager GameManager { get; set; }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Execute();

    }
}
