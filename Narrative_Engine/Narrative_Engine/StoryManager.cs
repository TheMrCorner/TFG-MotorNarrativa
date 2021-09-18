using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Narrative_Engine
{
    internal class StoryManager
    {
        internal static SortedSet<Story> stories { get; set; } = new SortedSet<Story>(new StoryComparer());
        internal static Dictionary<string, Quest> chapters { get; set; } = new Dictionary<string, Quest>();
        internal static Dictionary<string, StoryScene> storyScenes { get; set; } = new Dictionary<string, StoryScene>();
        internal static void AssembleStory()
        {
            foreach (var story in stories)
                foreach (var chapterName in story.chapters)
                    story.AddQuest(chapters[chapterName]);

            foreach (var quest in chapters.Values)
                foreach (var sceneName in quest.sceneNames)
                    quest.AddScene(storyScenes[sceneName]);
        }

        internal static Quest GetChapterById(string chapterId)
        {
            //cragar capítulo
            if (chapters.ContainsKey(chapterId))
            {
                var chapter = chapters[chapterId];
                //si existe cargar diálogos
                List<string> scenes = chapter.sceneNames;

                if (scenes.Count > 0)
                {
                    foreach (string storyScene in scenes)
                    {
                        if (storyScenes.ContainsKey(storyScene))
                        {
                            var scene = storyScenes[storyScene];
                            //NarrativeEngine.loadDialogues(scene);
                            chapter.AddScene(scene);
                        }
                    }
                }
                return chapter;
            }
            return null;
        }

        internal static Quest GetNextChapterById(string chapterId)
        {
            //cragar capítulo
            if (chapters.ContainsKey(chapterId))
            {
                var chapter = chapters[chapterId];
                //buscar siguiente
                if (chapter.next != "")
                {
                    if (chapters.ContainsKey(chapter.next))
                    {
                        var nextChapter = chapters[chapter.next];
                        //si existe cargar diálogos
                        List<string> scenes = nextChapter.sceneNames;

                        if (scenes.Count > 0)
                        {
                            foreach (string storyScene in scenes)
                            {
                                if (storyScenes.ContainsKey(storyScene))
                                {
                                    var scene = storyScenes[storyScene];
                                    //NarrativeEngine.loadDialogues(scene);
                                    nextChapter.AddScene(scene);
                                }
                            }
                        }
                        return nextChapter;
                    }
                }
            }
            return null;
        }
    }
}
