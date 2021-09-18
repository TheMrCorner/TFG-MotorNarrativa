using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;
using QuikGraph.Algorithms.ShortestPath;

namespace Narrative_Engine
{
    internal class PlaceManager
    {
        internal static Dictionary<string, Place> places { get; set; }
        private static Dictionary<string, List<Quest>> questsInPlace { get; } = new Dictionary<string, List<Quest>>();
        private static Dictionary<string, List<Story>> storiesStartingInPlace { get; set; } = new Dictionary<string, List<Story>>();
        internal static AdjacencyGraph<string, Edge<string>> graph { get; set; }
        internal static FloydWarshallAllShortestPathAlgorithm<string, Edge<string>> pathsFloyd { get; set; }
        
        internal static void CompleteQuestsInPlace()
        {
            foreach (var story in NarrativeEngine.GetStories())
            {
                story.storyType = StoryType.Secondary;

                if(NarrativeEngine.GetQuests().ContainsKey(story.chapters[0]) &&
                    NarrativeEngine.GetQuests()[story.chapters[0]].sceneNames.Count > 0)
                {
                    string sceneId = NarrativeEngine.GetQuests()[story.chapters[0]].sceneNames[0];
                    if (NarrativeEngine.GetStoryScenes().ContainsKey(sceneId))
                    {
                        StoryScene scene = NarrativeEngine.GetStoryScenes()[sceneId];
                        if (!questsInPlace.ContainsKey(scene.place))
                        {
                            questsInPlace.Add(scene.place, new List<Quest>());
                        }
                        questsInPlace[scene.place].Add(NarrativeEngine.GetQuests()[story.chapters[0]]);
                        if (!storiesStartingInPlace.ContainsKey(scene.place))
                        {
                            storiesStartingInPlace.Add(scene.place, new List<Story>());
                        }
                        storiesStartingInPlace[scene.place].Add(story);
                    }
                }
            }

            StoryManager.stories.First().storyType = StoryType.Main;
        }

        internal static HashSet<string> PathBetweenPlaces(string source, string target)
        {
            HashSet<string> placesInPath = new HashSet<string>();
            if(pathsFloyd.TryGetPath(source, target, out IEnumerable<Edge<string>> path))
            {
                foreach(var edge in path)
                {
                    placesInPath.Add(edge.Source);
                    placesInPath.Add(edge.Target);
                }
            }
            return placesInPath;
        }

        internal static void CompletePlaces()
        {
            foreach(var place in places)
            {
                place.Value.SearchAdjacent(places);
            }
        }
        internal static List<Story> GetStoriesInPlace(string place)
        {
            if(storiesStartingInPlace.TryGetValue(place, out var storyList))
            {
                var unconsumedStories = storyList.FindAll((x) => !x.consumed);
                return unconsumedStories;
            }
            return new List<Story>();
        }
        internal static List<Quest> GetQuestsInPlace(string place)
        {
            // stories no consumidas -> random (1, stories.count)

            if (storiesStartingInPlace.TryGetValue(place, out var storyList))
            {
                var availableStories = storyList.FindAll((x) => !x.consumed);
                if(availableStories.Count > 0)
                {
                    Random random = new Random();
                    int randomCounter = random.Next(1, Math.Min(availableStories.Count, 3));
                    List<Quest> quests = new List<Quest>();
                    var sortedStories = StoryManager.stories.Intersect(availableStories);
                    for (int i = 0; i < randomCounter; i++)
                    {
                        var questId = sortedStories.FirstOrDefault().chapters[0];
                        Quest quest = NarrativeEngine.GetQuests()[questId];
                        quests.Add(quest);
                    }
                    return quests;
                }
            }

            return new List<Quest>();
        }

        internal static List<string> GetGenericDialogsInPlace(string place)
        {
            if (places.TryGetValue(place, out var placeObject))
                return placeObject.genericDialogs;
            
            return new List<string>();
        }
    }
}
