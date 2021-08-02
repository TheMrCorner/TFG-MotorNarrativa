using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    internal class PlaceController
    {
        static public Dictionary<string, Place> m_places { get; set; }
        static public Dictionary<string, List<Quest>> m_questsInPlace { get; set; } = new Dictionary<string, List<Quest>>();

        static public void CompleteQuestsInPlace()
        {
            foreach (var story in NarrativeEngine.GetStories())
            {
                if(NarrativeEngine.GetQuests().ContainsKey(story.m_chapters[0]) &&
                    NarrativeEngine.GetQuests()[story.m_chapters[0]].m_scenes.Count > 0)
                {
                    string scene_id = NarrativeEngine.GetQuests()[story.m_chapters[0]].m_scenes[0];
                    if (NarrativeEngine.GetStoryScenes().ContainsKey(scene_id))
                    {
                        StoryScene scene = NarrativeEngine.GetStoryScenes()[scene_id];
                        if (!m_questsInPlace.ContainsKey(scene.m_place))
                        {
                            m_questsInPlace.Add(scene.m_place, new List<Quest>());
                        }
                        m_questsInPlace[scene.m_place].Add(NarrativeEngine.GetQuests()[story.m_chapters[0]]);
                            
                    }
                }
            }
        }

        static public void completePlaces()
        {
            foreach(var place in m_places)
            {
                place.Value.searchAdjacent(m_places);
            }
        }

        static public List<Quest> GetQuestsInPlace(string place)
        {
            if (m_questsInPlace.TryGetValue(place, out var questList))
                return questList;
            
            return new List<Quest>();
        }

        static public List<string> GetGenericDialogsInPlace(string place)
        {
            if (m_places.TryGetValue(place, out var placeObject))
                return placeObject.genericDialogs;
            
            return new List<string>();
        }

    }
}
