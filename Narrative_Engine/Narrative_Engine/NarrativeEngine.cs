using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    public class NarrativeEngine
    {
        internal static List<Story> m_stories { get; set; } = new List<Story>();
        internal static Dictionary<string, Quest> m_chapters { get; set; } = new Dictionary<string, Quest>();
        internal static Dictionary<string, StoryScene> m_storyScenes { get; set; } = new Dictionary<string, StoryScene>();

        private static DialogController dialogController;


        public static void init(string generalPath)
        {
            FileManager fileManager = new FileManager(generalPath, "Story.json", "Chapters.json", "Scenes.json", "Dialogs", "Characters.json", "Items.json", "Place.json");
            //fileManager.makeExampleFiles();
            fileManager.readFiles();
            assembleStory();
            dialogController = new DialogController(fileManager);
        }

        static void assembleStory()
        {
            foreach (var quest in m_chapters.Values)
                foreach (var sceneName in quest.m_scenes)
                    quest.AddScene(m_storyScenes[sceneName]);

            foreach (var story in m_stories)
                foreach (var chapterName in story.m_chapters)
                    story.addQuest(m_chapters[chapterName]);
        }

        static Story getStory()
        {
            var rand = new Random();
            int index = rand.Next(0, m_stories.Count());
            var story = m_stories[index];
            m_stories.RemoveAt(index);
            return story;
        }

        public static List<Quest> getChaptersByPlace(string place)
        {
            //LLamar al PlaceCOntroller
            
            return new List<Quest>();
        }

        public static Quest getChapterById(string chapter_id)
        {
            var chapter = m_chapters[chapter_id];
            if (chapter != null)
            {
                return m_chapters[chapter_id];
            }
            return null;
        }

        public static void loadDialogues(StoryScene scene)
        {
            foreach(string filePath in scene.m_dialogs)
            {
                scene.dialogs.Add(dialogController.GetDialog(filePath));
            }
        }
    }
}
