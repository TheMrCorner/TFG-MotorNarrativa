using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    public enum StoryType
    {
        MAIN,
        SECONDARY
    }
    class Story
    {
        public readonly StoryType m_storyType;
        private List<Quest> m_quests;

        public List<string> m_chapters;

        public Story(StoryType m_storyType, List<string> m_chapters)
        {
            this.m_storyType = m_storyType;
            this.m_chapters = m_chapters;
        }    
    }
}
