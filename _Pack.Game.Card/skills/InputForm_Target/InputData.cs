using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UnityEngine;


    

    public struct InputDataPoint : IInputData
    {
        public N<Vector3> Point { get; set; }
        public N<int> LayerId => null;
    }
    public struct InputDataUnit : IInputData
    {
        public void SetData(Unit u)
        {
            unitID = u.ID;
            _unit = u;
        }
        public int unitID;
        Unit _unit;
        public N<Vector3> Point
        {
            get

            {
                if (_unit == null) _unit = IDs<Unit>.Get(unitID);
                if (_unit == null) return null;
                return _unit.RealPoss;
            }
        }

        public N<int> LayerId => unitID;
    }
