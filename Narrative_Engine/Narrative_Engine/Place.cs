using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    public class Place
    {
        public string m_name { get; }
        public List<string> m_adjacentPlacesNames { get; }
        List<Place> m_adjacentPlaces;
        public List<string> m_itemsFound;

        public Place(string m_name, List<string> m_adjacentPlacesNames)
        {
           this.m_name = m_name;
           this.m_adjacentPlacesNames = m_adjacentPlacesNames;
        }

        public void searchAdjacent(Dictionary<string, Place> places)
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
