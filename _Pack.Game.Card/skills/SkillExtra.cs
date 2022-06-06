using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{

    public static class SkillExtra
    {
        public static Unit GetUnit(this InputNode n)
        {
            if (n.LayerID.HasValue == false) return null;
            return IDs<Unit>.Get(n.LayerID.Value);
        }
        public static bool HaveUnit(this InputNode n, out Unit to)
        {
            to = null;
            if (n.LayerID.HasValue == false) return false;
            to = IDs<Unit>.Get(n.LayerID.Value);
            if (to == null) return false;
            return true;
        }
        public static bool NodeIsFrientInHand(this Skill s, InputNode n)
        {
            if (n == null || n.Equals(null)) return false;

            if (s.up == null) return false;
            if (HaveUnit(n, out var to) == false) return false;
            if (to.IsFriend(s.up) == false) return false;
            if (to.IsCardInHand() == false) return false;
            return true;
        }
        public static bool NodeReachable(this Skill s, InputNode n, float disLimit)
        {
            if (n == null || n.Equals(null)) return false;

            if (n.Point.HasValue==false) return false;
            if (Vector3.Distance(s.unit.RealPoss, n.Point.Value) > disLimit) return false;
            return true;
        }
        public static bool CanAddFollower(this Skill s, InputNode n)
        {
            if (n == null || n.Equals(null)) return false;
            if (HaveUnit(n, out var to) == false) return false;
            if (s.up.CanAddFollower(to) == false) return false;
            return true;
        }
    }

}
