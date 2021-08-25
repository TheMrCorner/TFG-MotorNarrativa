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
            // getChaptersByPlace("Ayuntamiento_Fyrst");
        }

        public static List<Story> GetStoriesByPlace(string place)
        {
            List<Story> stories = new List<Story>();
            var storiesInPlace = PlaceController.GetStoriesInPlace(place);
            if(storiesInPlace.Count > 0)
            {
                var sortedStories = StoryController.m_stories.Intersect(storiesInPlace);
                Random random = new Random();
                int nStories = random.Next(1, Math.Min(storiesInPlace.Count, 3));
                for(int i = 0; i < nStories; i++)
                {
                    stories.Add(sortedStories.ElementAt(i));
                    sortedStories.ElementAt(i).consumed = true;
                }
            }
            return stories;
        }

        public static List<Quest> getChaptersByPlace(string place)
        {
            List<Quest> quests = new List<Quest>();
            var stories = GetStoriesByPlace(place);
            foreach(var s in stories)
            {
                quests.Add(StoryController.m_chapters[s.m_chapters[0]]);
            }
            return quests;
        }

        public static Quest GetMainQuest()
        {
            var mainStory = StoryController.m_stories.First();
            mainStory.consumed = true;
            return StoryController.m_chapters[mainStory.m_chapters[0]];
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

        public static List<Story> GetStories() => StoryController.m_stories.ToList();

        public static Dictionary<string, Quest> GetQuests() => StoryController.m_chapters;

        public static Dictionary<string, StoryScene> GetStoryScenes() => StoryController.m_storyScenes;
    }
}
