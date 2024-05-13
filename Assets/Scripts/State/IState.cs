using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LMS.User;
using LMS.General;

namespace LMS.State
{
    public interface IState<T> where T : Entity
    {
        public bool Idle(T obj);
        public bool Move(T obj);
        public bool Attack(T obj);
        public bool Hit(T obj);
        public bool Dead(T obj);
        

        public void Enter(T obj) { }
        public void Action(T obj) { }
        public void Exit(T obj) { }
    }
}
