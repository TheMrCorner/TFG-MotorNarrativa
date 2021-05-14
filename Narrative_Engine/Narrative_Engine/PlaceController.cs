using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    class PlaceController
    {
        static private Dictionary<string, Place> m_places { get; set; }

        static public void completePlaces()
        {
            foreach(var place in m_places)
            {
                place.Value.searchAdjacent(m_places);
            }
        }

    }
}
