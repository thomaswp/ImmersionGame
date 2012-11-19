using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Immersion
{
    public interface IPathed
    {
        Vector2 GetPosition(float degree);
    }
}
