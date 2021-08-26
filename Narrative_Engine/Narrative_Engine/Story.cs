using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    public enum StoryType
    {
        MAIN = 0,
        SECONDARY = 1
    }

   

    public class Story : IComparable
    {
        static float s_characterImportanceWeight = 34f;
        static float s_storyLenghtWeight = 33f;
        static float s_placesVisitedWeight = 33f;


        internal StoryType m_storyType { get; set; }
        private List<Quest> m_quests = new List<Quest>();

        internal List<string> m_chapters { get; }
        internal List<string> m_characters { get; }

        internal bool consumed { get; set; } = false;

        internal uint m_charactersImportance = 0;
        internal int m_totalScenes = 0;
        internal HashSet<string> m_placesInvolved = new HashSet<string>();

        internal float m_importance;

        internal Story(StoryType m_storyType, List<string> m_chapters)
        {
            this.m_storyType = m_storyType;
            this.m_chapters = m_chapters;
        }

        internal Story(List<string> m_chapters, List<string> m_characters, HashSet<string> m_placesInvolved, int m_totalScenes, uint m_charactersImportance)
        {
            this.m_chapters = m_chapters;
            this.m_characters = m_characters;
            this.m_placesInvolved = m_placesInvolved;
            this.m_totalScenes = m_totalScenes;
            this.m_charactersImportance = m_charactersImportance;

            calculateImportance();
        }

        internal void addQuest(Quest quest)
        {
            if (!m_quests.Contains(quest))
                m_quests.Add(quest);
        }

        internal Dictionary<string, object> toDictionary()
        {
            var ret = new Dictionary<string, object>();

            ret.Add("m_storyType", m_storyType);
            ret.Add("m_chapters", m_chapters);

            return ret;
        }

        private void calculateImportance()
        {
            //TODO: get Character Importance

            /*m_importance = (m_characters.Count * s_characterImportanceWeight
                + m_totalScenes * s_storyLenghtWeight + m_placesInvolved.Count * s_placesVisitedWeight) 
                / (s_characterImportanceWeight + s_storyLenghtWeight + s_placesVisitedWeight);*/

            float characterImportance = m_charactersImportance * 100f / CharacterController.totalImportance;
            float sceneCount = m_totalScenes * 100f / StoryController.m_storyScenes.Count;
            float placesCount = m_placesInvolved.Count * 100f / PlaceController.m_places.Count;

            m_importance = (characterImportance + sceneCount + placesCount) / 3;
        }


        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Story otherStory = obj as Story;
            if (otherStory != null)
                return this.m_importance.CompareTo(otherStory.m_importance);
            else
                throw new ArgumentException("Object is not a Story");
        }
    }

    internal class StoryComparer : IComparer<Story>
    {
        public int Compare([AllowNull] Story x, [AllowNull] Story y)
        {
            return y.CompareTo(x);
        }
    }
}
