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
        Main = 0,
        Secondary = 1
    }

   

    public class Story : IComparable
    {
        private static float characterImportanceWeight = 34f;
        private static float storyLenghtWeight = 33f;
        private static float placesVisitedWeight = 33f;


        internal StoryType storyType { get; set; }
        private List<Quest> quests = new List<Quest>();

        internal List<string> chapters { get; }
        private List<string> characters;

        internal bool consumed { get; set; } = false;

        private uint charactersImportance = 0;
        private int totalScenes = 0;
        private HashSet<string> placesInvolved = new HashSet<string>();

        internal float importance;

        internal Story(StoryType storyType, List<string> chapters)
        {
            this.storyType = storyType;
            this.chapters = chapters;
        }

        internal Story(List<string> chapters, List<string> characters, HashSet<string> placesInvolved, int totalScenes, uint charactersImportance)
        {
            this.chapters = chapters;
            this.characters = characters;
            this.placesInvolved = placesInvolved;
            this.totalScenes = totalScenes;
            this.charactersImportance = charactersImportance;

            CalculateImportance();
        }

        internal void AddQuest(Quest quest)
        {
            if (!quests.Contains(quest))
                quests.Add(quest);
        }

        internal Dictionary<string, object> ToDictionary()
        {
            var ret = new Dictionary<string, object>();

            ret.Add("m_storyType", storyType);
            ret.Add("m_chapters", chapters);

            return ret;
        }

        private void CalculateImportance()
        {
            //TODO: get Character Importance

            /*m_importance = (m_characters.Count * s_characterImportanceWeight
                + m_totalScenes * s_storyLenghtWeight + m_placesInvolved.Count * s_placesVisitedWeight) 
                / (s_characterImportanceWeight + s_storyLenghtWeight + s_placesVisitedWeight);*/

            float characterImportance = charactersImportance * 100f / CharacterManager.totalImportance;
            float sceneCount = totalScenes * 100f / StoryManager.storyScenes.Count;
            float placesCount = placesInvolved.Count * 100f / PlaceManager.places.Count;

            importance = (characterImportance + sceneCount + placesCount) / 3;
        }


        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Story otherStory = obj as Story;
            if (otherStory != null)
                return this.importance.CompareTo(otherStory.importance);
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
