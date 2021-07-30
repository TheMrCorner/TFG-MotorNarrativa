using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    internal class Place
    {
        internal string m_name { get; }
        internal List<string> m_adjacentPlacesNames { get; }
        List<Place> m_adjacentPlaces;
        internal List<string> m_itemsFound;
        internal List<Dialog> m_genericDialogs;
        internal List<string> genericDialogs;

        internal Place(string m_name, List<string> m_adjacentPlacesNames, 
            List<string> genericDialogs)
        {
           this.m_name = m_name;
           this.m_adjacentPlacesNames = m_adjacentPlacesNames;
           this.genericDialogs = genericDialogs;
           m_adjacentPlaces = new List<Place>();
        }

        internal void searchAdjacent(Dictionary<string, Place> places)
        {
            foreach (var placeName in m_adjacentPlacesNames)
            {
                Place place;
                if (places.TryGetValue(placeName, out place))
                    m_adjacentPlaces.Add(place);
            }
        }


    }
}
