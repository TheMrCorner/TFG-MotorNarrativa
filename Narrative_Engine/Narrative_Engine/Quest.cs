using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    public class Quest
    {
        private int currentScene;
        private Place origin;
        private Place destination;
        public List<StoryScene> scenes { get; } = new List<StoryScene>();

		public string m_id { get; }
        public string m_next { get; }
        public List<string> m_scenes { get; }

        public Quest(string m_id, List<string>m_scenes, string m_next)
        {
            this.m_id = m_id;
            this.m_scenes = m_scenes;
            this.m_next = m_next;
        }

        public StoryScene GetCurrentScene()
        {
            return scenes[currentScene];
        }

        internal Place GetOrigin()
        {
            return origin;
        }

        internal Place GetDestination()
        {
            return destination;
        }

        public StoryScene GetNextScene()
        {
            StoryScene scene = null;
            if (currentScene + 1 < scenes.Count)
            {
                scene = scenes[currentScene + 1];
            }
            return scene;
        }
        internal void SetCurrentScene(int currentScene)
        {
            if (currentScene < scenes.Count)
            {
                this.currentScene = currentScene;
            }
        }
        internal void SetOrigin(Place origin)
        {
            this.origin = origin;
        }

        internal void SetDestination(Place destination)
        {
            this.destination = destination;
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
        public StoryScene NextScene()
        {
            StoryScene scene = GetNextScene();
            if (scene != null)
            {
                currentScene++;
            }
            return scene;
        }
    }
}
