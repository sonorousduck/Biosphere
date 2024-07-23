using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequoiaEngine
{
    public class TiledMapComponent : Component
    {
        public TiledMap TiledMap;

        public TiledMapComponent(TiledMap map)
        {
            this.TiledMap = map;
        }

    }
}
