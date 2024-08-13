using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Schema.Brick.Classes.Locations
{
    public class Building : BrickSchema
    {
        public Floor Floor => new Floor();
        public Space Space => new Space();
        public Storey Storey => new Storey();
        public override bool CanContain(BrickSchema entity)
        {
            // Use reflection to check if the entity can be contained
            return CanContainType(entity.GetType());
        }
    }
}
