using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    public class NarrativeEngine
    {
        internal static PlaceController m_pc;
        internal static DialogController m_dc;
        internal static CharacterController m_cc;
        internal static ItemController m_ic;

        internal static List<Story> m_stories { get; set; } = new List<Story>();
        internal static Dictionary<string, Quest> m_chapters { get; set; } = new Dictionary<string, Quest>();
        internal static Dictionary<string, StoryScene> m_storyScenes { get; set; } = new Dictionary<string, StoryScene>();

        public static void init(string generalPath)
        {
            FileManager fileManager = new FileManager(generalPath, "Story.json", "Chapters.json", "Scenes.json", "Dialogs", "Characters.json", "Items.json", "Place.json");
            //fileManager.makeExampleFiles();
            fileManager.readFiles();
            StoryController.assembleStory();
            m_dc = new DialogController(fileManager);
        }

        static Story getStory()
        {
            var rand = new Random();
            int index = rand.Next(0, StoryController.m_stories.Count());
            var story = StoryController.m_stories[index];
            StoryController.m_stories.RemoveAt(index);
            return story;
        }

        public static List<Quest> getChaptersByPlace(string place)
        {
            return PlaceController.GetQuestsInPlace(place);
        }

        public static Quest getChapterById(string chapter_id)
        {
            return StoryController.getChapterById(chapter_id);
        }

        public static void loadDialogues(StoryScene scene)
        {
            foreach(string filePath in scene.m_dialogs)
            {
                scene.dialogs.Add(m_dc.GetDialog(filePath));
            }
        }

        public static List<Story> GetStories() => StoryController.m_stories;

        public static Dictionary<string, Quest> GetQuests() => StoryController.m_chapters;

        public static Dictionary<string, StoryScene> GetStoryScenes() => StoryController.m_storyScenes;
    }
}
