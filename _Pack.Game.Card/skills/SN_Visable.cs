using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{
    public class Visable_OnCard : Func0Node<Skill, bool>
    {
        public override void Invoke()
        {
            if (self.unit.State.Value != UnitState.Card) re(false);
        }
    }
    public class Visable_OnSpace : Func0Node<Skill, bool>
    {
        public override void Invoke()
        {
            if (self.unit.State.Value != UnitState.Space) re(false);

        }
    }


}
