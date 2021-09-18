using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    internal class Place
    {
        internal string name { get; }
        internal List<string> adjacentPlacesNames { get; }
        private List<Place> adjacentPlaces;
        internal List<string> genericDialogs;

        internal Place(string name, List<string> adjacentPlacesNames, 
            List<string> genericDialogs)
        {
           this.name = name;
           this.adjacentPlacesNames = adjacentPlacesNames;
           this.genericDialogs = genericDialogs;
           adjacentPlaces = new List<Place>();
        }

        internal void SearchAdjacent(Dictionary<string, Place> places)
        {
            foreach (var placeName in adjacentPlacesNames)
            {
                if (places.TryGetValue(placeName, out var place))
                    adjacentPlaces.Add(place);
            }
        }


    }
}
