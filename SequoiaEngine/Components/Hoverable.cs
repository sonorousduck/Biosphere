using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequoiaEngine
{
    public class Hoverable : Component
    {
        public Hoverable(Action onHoverStart, Action onHoverEnd) 
        {
            this.OnHoverStart = onHoverStart; 
            this.OnHoverEnd = onHoverEnd;
        }

        public Action OnHoverStart;
        public Action OnHoverEnd;
    }
}
