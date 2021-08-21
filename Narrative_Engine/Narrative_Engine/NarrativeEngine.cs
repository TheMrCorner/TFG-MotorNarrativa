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


        public static void init(string generalPath)
        {
            FileManager fileManager = new FileManager(generalPath, "Story.json", "Chapters.json", "Scenes.json", "Dialogs", "Characters.json", "Items.json", "Place.json");
            //fileManager.makeExampleFiles();
            fileManager.readFiles();
            StoryController.assembleStory();
            m_dc = new DialogController(fileManager);
            // TEST
            // StoryController.m_stories[0].consumed = true;
            // CharacterController.characters["Mayor"].GetCharacterName();
            // loadGenericDialogs("Fyrst");
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

        public static Quest getNextChapterById(string chapter_id)
        {
            return StoryController.getNextChapterById(chapter_id);
        }

        public static void loadDialogues(StoryScene scene)
        {
            foreach(string filePath in scene.m_dialogs)
            {
                scene.dialogs.Add(m_dc.GetDialog(filePath + ".json"));
            }
        }

        public static List<Dialog> loadGenericDialogs(string place)
        {
            List<Dialog> dialogs = new List<Dialog>();
            List<string> dialogIds = PlaceController.GetGenericDialogsInPlace(place);
            foreach (string dialog in dialogIds)
                dialogs.Add(m_dc.GetDialog(dialog + ".json"));
            return dialogs;
        }

        public static List<Story> GetStories() => StoryController.m_stories;

        public static Dictionary<string, Quest> GetQuests() => StoryController.m_chapters;

        public static Dictionary<string, StoryScene> GetStoryScenes() => StoryController.m_storyScenes;
    }
}
