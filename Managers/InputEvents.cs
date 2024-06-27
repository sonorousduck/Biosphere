using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosphere
{
    /// <summary>
    /// This will contain all the action definitions that call the event that gets propogated to the individual components
    /// </summary>
    public class InputEvents
    {

        public event Action<int> OnScrollAction;

        public void OnScroll()
        {

        }


    }
}
