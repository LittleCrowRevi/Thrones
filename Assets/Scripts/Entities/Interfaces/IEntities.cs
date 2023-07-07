using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thrones.Entities.Interfaces {
    public abstract class IEntities: MonoBehaviour
    {
        [SerializeReference]
        int _Level;
        public int Level { 
            get {
                return _Level;
            } 
            set {
                _Level = value;
            } 
        }
        [SerializeReference]
        int _MaxHealthPoints;
        public int MaxHealthPoints {
            get {
                return _MaxHealthPoints;
            }
            set {
                _MaxHealthPoints = value;
                CurrentHealthPoints += value;
            }
        }
        [SerializeReference]
        int _CurrentHealthPoints;
        public int CurrentHealthPoints { 
            get {
                return _CurrentHealthPoints;
            }
            set {
                _CurrentHealthPoints = value;
            }
         }
        [SerializeReference]
        int _AttackPoints;
        public int AttackPoints { 
            get {
                return _AttackPoints;
            }
            set {
                _AttackPoints = value;
            }
         }


    }
    
}
