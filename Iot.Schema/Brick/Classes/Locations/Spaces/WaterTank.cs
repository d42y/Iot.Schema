using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Schema.Brick.Classes.Locations.Spaces
{
    public class WaterTank : BrickSchema
    {
        public override bool CanContain(BrickSchema entity)
        {
            // Use reflection to check if the entity can be contained
            return CanContainType(entity.GetType());
        }
    }
}
