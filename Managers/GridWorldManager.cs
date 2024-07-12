using Microsoft.Xna.Framework;
using SequoiaEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosphere.Managers
{
    public static class GridWorldManager
    {

        public static int GridWidth = 80;
        public static int GridHeight = 65;
        public static Vector2 GridSize = new Vector2(GridWidth, GridHeight);
        public static Vector2 MaxGrid = new Vector2();

        public static bool FirstUpdate = true;
      

        public static Vector2 GetPositionOfGrid(Vector2 positionInWorld)
        {
            if (FirstUpdate)
            {
                MaxGrid = GameManager.Instance.RenderWindowSize / GridSize;
            }

            Vector2 position = new Vector2();

            // Step 1: Divide the Render Space by the Grid Sizes. This gives the number of grid elements
            Vector2 gridBasedLocation = (positionInWorld / GameManager.Instance.RenderWindowSize) * MaxGrid;
            gridBasedLocation.Round();
            position = gridBasedLocation * (GameManager.Instance.RenderWindowSize / MaxGrid);



            return position;
        }

    }
}
