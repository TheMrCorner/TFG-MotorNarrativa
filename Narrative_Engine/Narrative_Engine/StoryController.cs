using System;
using System.Collections.Generic;
using System.Text;

namespace Narrative_Engine
{
    internal class StoryController
    {
        internal static List<Story> m_stories { get; set; } = new List<Story>();
        internal static Dictionary<string, Quest> m_chapters { get; set; } = new Dictionary<string, Quest>();
        internal static Dictionary<string, StoryScene> m_storyScenes { get; set; } = new Dictionary<string, StoryScene>();

        internal static void assembleStory()
        {
            foreach (var story in m_stories)
                foreach (var chapterName in story.m_chapters)
                    story.addQuest(m_chapters[chapterName]);

            foreach (var quest in m_chapters.Values)
                foreach (var sceneName in quest.m_scenes)
                    quest.AddScene(m_storyScenes[sceneName]);
        }

        internal static Quest getChapterById(string chapter_id)
        {
            //cragar capítulo
            if (m_chapters.ContainsKey(chapter_id))
            {
                var chapter = m_chapters[chapter_id];
                //buscar siguiente
                if (chapter.m_next != "")
                {
                    if (m_chapters.ContainsKey(chapter.m_next))
                    {
                        var next_chapter = m_chapters[chapter.m_next];
                        //si existe cargar diálogos
                        List<string> scenes = next_chapter.m_scenes;

                        if (scenes.Count > 0)
                        {
                            foreach (string storyScene in scenes)
                            {
                                if (m_storyScenes.ContainsKey(storyScene))
                                {
                                    var scene = m_storyScenes[storyScene];
                                    NarrativeEngine.loadDialogues(scene);
                                    next_chapter.AddScene(scene);
                                }
                            }
                        }
                        return next_chapter;
                    }
                }
            }
            return null;
        }
    }
}
