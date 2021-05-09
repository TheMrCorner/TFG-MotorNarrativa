using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    class NarrativeEngine
    {
        private static List<Story> m_stories = new List<Story>();
        

        
        public static Story getStory()
        {
            var rand = new Random();
            int index = rand.Next(0, m_stories.Count());
            var story = m_stories[index];
            m_stories.RemoveAt(index);
            return story;
        }
    }
}
