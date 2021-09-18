using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    public class Quest
    {
        public List<StoryScene> scenes { get; } = new List<StoryScene>();

		public string id { get; }
        public string next { get; }
        public List<string> sceneNames { get; }

        public Quest(string id, List<string>sceneNames, string next)
        {
            this.id = id;
            this.sceneNames = sceneNames;
            this.next = next;
        }

        internal void AddScene(StoryScene scene)
        {
            if (!scenes.Contains(scene))
            {
                scenes.Add(scene);
            }
        }
        internal void RemoveScene(StoryScene scene)
        {
            if (scenes.Contains(scene))
            {
                scenes.Remove(scene);
            }
        }
    }
}
