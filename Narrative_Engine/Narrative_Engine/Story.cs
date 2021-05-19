using System;
using System.Collections.Generic;
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
    class Story
    {
        public StoryType m_storyType { get; }
        private List<Quest> m_quests = new List<Quest>();

        public List<string> m_chapters { get; }

        public Story(StoryType m_storyType, List<string> m_chapters)
        {
            this.m_storyType = m_storyType;
            this.m_chapters = m_chapters;
        } 

        public void addQuest(Quest quest)
        {
            if (!m_quests.Contains(quest))
                m_quests.Add(quest);
        }
    }
}
