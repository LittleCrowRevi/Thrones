using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Assets.Scripts.States
{
    public interface IState
    {
        public string Name { get; }
        public StateManager StateManager { get; set; }

        public void Enter();
        public void Exit();
        public void Execute();

    }
}
