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
    public class Story
    {
        internal StoryType m_storyType { get; }
        private List<Quest> m_quests = new List<Quest>();

        internal List<string> m_chapters { get; }

        internal Story(StoryType m_storyType, List<string> m_chapters)
        {
            this.m_storyType = m_storyType;
            this.m_chapters = m_chapters;
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
    }
}
