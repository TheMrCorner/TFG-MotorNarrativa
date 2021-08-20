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
        static public Dictionary<string, List<Story>> m_storiesStartingInPlace { get; set; } = new Dictionary<string, List<Story>>();

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
                        if (!m_storiesStartingInPlace.ContainsKey(scene.m_place))
                        {
                            m_storiesStartingInPlace.Add(scene.m_place, new List<Story>());
                        }
                        m_storiesStartingInPlace[scene.m_place].Add(story);
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
            /*if (m_questsInPlace.TryGetValue(place, out var questList))
                return questList;*/

            // stories no consumidas -> random (0, stories.count)

            if (m_storiesStartingInPlace.TryGetValue(place, out var storyList))
            {
                var availableStories = storyList.FindAll((x) => !x.consumed);
                if(availableStories.Count > 0)
                {
                    Random random = new Random();
                    int randomCounter = random.Next(1, Math.Min(availableStories.Count, 3));
                    List<Quest> quests = new List<Quest>();
                    for(int i = 0; i < randomCounter; i++)
                    {
                        // Story s = ClaseDeIdentificacionDeHistorias.GetHigherPriorityStory();
                        // quests.Add(s.m_quests[0]);
                    }
                    return quests;
                }
            }

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
