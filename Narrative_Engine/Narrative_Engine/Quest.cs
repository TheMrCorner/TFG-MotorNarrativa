using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    class Quest
    {
        private int currentScene;
        private Place origin;
        private Place destination;
        private List<StoryScene> scenes;

		public string m_id { get; }
        public List<string> m_scenes { get; }

        public Quest(string m_id, List<string>m_scenes)
        {
            this.m_id = m_id;
            this.m_scenes = m_scenes;
        }

        public StoryScene GetCurrentScene()
        {
            return scenes[currentScene];
        }

        public Place GetOrigin()
        {
            return origin;
        }

        public Place GetDestination()
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
        public void SetCurrentScene(int currentScene)
        {
            if (currentScene < scenes.Count)
            {
                this.currentScene = currentScene;
            }
        }
        public void SetOrigin(Place origin)
        {
            this.origin = origin;
        }

        public void SetDestination(Place destination)
        {
            this.destination = destination;
        }

        public void AddScene(StoryScene scene)
        {
            if (!scenes.Contains(scene))
            {
                scenes.Add(scene);
            }
        }
        public void RemoveScene(StoryScene scene)
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
