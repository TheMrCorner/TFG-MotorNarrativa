using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    public class NarrativeEngine
    {
        static private DialogManager dialogManager;


        public static void Init(string generalPath)
        {
            FileManager fileManager = new FileManager(generalPath, "Story.json",
                "Chapters.json", "Scenes.json", "Dialogs", "Characters.json",
                "Items.json", "Place.json");
            //fileManager.makeExampleFiles();
            fileManager.ReadFiles();
            StoryManager.AssembleStory();
            dialogManager = new DialogManager(fileManager);
        }

        private static List<Story> GetStoriesByPlace(string place)
        {
            List<Story> stories = new List<Story>();
            var storiesInPlace = PlaceManager.GetStoriesInPlace(place);
            if(storiesInPlace.Count > 0)
            {
                var sortedStories = StoryManager.stories.Intersect(storiesInPlace);
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

        public static List<Quest> GetChaptersByPlace(string place)
        {
            List<Quest> quests = new List<Quest>();
            var stories = GetStoriesByPlace(place);
            foreach(var s in stories)
            {
                quests.Add(StoryManager.chapters[s.chapters[0]]);
            }
            return quests;
        }

        public static Quest GetMainQuest()
        {
            var mainStory = StoryManager.stories.First();
            mainStory.consumed = true;
            return StoryManager.chapters[mainStory.chapters[0]];
        }

        public static Quest GetChapterById(string chapterId)
        {
            return StoryManager.GetChapterById(chapterId);
        }

        public static Quest GetNextChapterById(string chapterId)
        {
            return StoryManager.GetNextChapterById(chapterId);
        }

        public static void LoadDialogues(StoryScene scene)
        {
            foreach(string filePath in scene.dialogNames)
            {
                scene.dialogs.Add(dialogManager.GetDialog(filePath + ".json"));
            }
        }

        public static List<Dialog> LoadGenericDialogs(string place)
        {
            List<Dialog> dialogs = new List<Dialog>();
            List<string> dialogIds = PlaceManager.GetGenericDialogsInPlace(place);
            foreach (string dialog in dialogIds)
                dialogs.Add(dialogManager.GetDialog(dialog + ".json"));
            return dialogs;
        }

        public static List<Story> GetStories() => StoryManager.stories.ToList();

        public static Dictionary<string, Quest> GetQuests() => StoryManager.chapters;

        public static Dictionary<string, StoryScene> GetStoryScenes() => StoryManager.storyScenes;
    }
}
