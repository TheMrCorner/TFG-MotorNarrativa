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
        // private List<Quest> m_quests;

        public Story(StoryType type)
        {
            m_storyType = type;
        }     
    }
}
