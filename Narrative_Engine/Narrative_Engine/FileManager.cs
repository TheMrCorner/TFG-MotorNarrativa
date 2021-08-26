﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using SimpleJSON;
using QuikGraph;
using QuikGraph.Algorithms;
using QuikGraph.Algorithms.ShortestPath;

namespace Narrative_Engine
{
    class FileManager
    {
        static FileManager s_instance;
        string m_generalPath;
        string m_storyFolder;
        string m_dialogFolder;
        string m_charactersPath;
        string m_itemsPath;
        string m_placesPath;
        string m_chaptersPath;
        string m_scenesPath;

        List<string> m_items;


        public FileManager(string generalPath, string storyFolder, string chapterPath, string scenePath, string dialogFolder, string charactersPath, string itemsPath, string placesPath)
        {
            // TEST
            m_generalPath = "../../../" + generalPath + "/";
            // m_generalPath = generalPath + "/";
            m_storyFolder = m_generalPath + storyFolder;
            m_dialogFolder = m_generalPath + dialogFolder;
            m_charactersPath = m_generalPath + charactersPath;
            m_itemsPath = m_generalPath + itemsPath;
            m_placesPath = m_generalPath + placesPath;
            m_chaptersPath = m_generalPath + chapterPath;
            m_scenesPath = m_generalPath + scenePath;
        }

        static void initConfiguration(string configurationPath)
        {
            var jsonstring = File.ReadAllText(configurationPath);
            //s_instance = JsonSerializer.Deserialize<FileManager>(jsonstring);
        }

        public static FileManager getInstance()
        {
            return s_instance;
        }

        public void readFiles()
        {
            var jsonstring = File.ReadAllText(m_charactersPath);
            var charactersJson = JSONDecoder.Decode(jsonstring).ArrayValue;
            var charactersList = new Dictionary<string, Character>();
            uint totalImportance = 0;
            foreach (var character in charactersJson)
            {
                charactersList.Add((string)character["m_name"], new Character((string)character["m_name"], (uint)character["m_relevance"]));
                totalImportance += (uint)character["m_relevance"];
            }
            CharacterController.characters = charactersList;
            CharacterController.totalImportance = totalImportance;

            jsonstring = File.ReadAllText(m_itemsPath);
            var items = JSONDecoder.Decode(jsonstring).ArrayValue;
            m_items = new List<string>();
            foreach (var item in items)
                m_items.Add((string)item);

            jsonstring = File.ReadAllText(m_placesPath);
            var placesJsonList = JSONDecoder.Decode(jsonstring).ArrayValue;

            var placesList = new List<Place>();
            List<Edge<string>> edges = new List<Edge<string>>();
            foreach (var placeJson in placesJsonList)
            {
                var adjacentJson = placeJson["m_adjacentPlacesNames"].ArrayValue;
                var adjacentPlaces = new List<string>();
                foreach (var adjacent in adjacentJson)
                {
                    adjacentPlaces.Add((string)adjacent);
                    edges.Add(new Edge<string>((string)placeJson["m_name"], (string)adjacent));
                }

                List<string> genericDialogs = new List<string>();
                foreach(var genericDialog in placeJson["m_genericDialogs"].ArrayValue)
                    genericDialogs.Add((string)genericDialog);

                placesList.Add(new Place((string)placeJson["m_name"], adjacentPlaces, genericDialogs));
            }
            PlaceController.graph = edges.ToAdjacencyGraph<string, Edge<string>>();
            /* Dijktra con pesos 1 equivale a BFS
             * Habría que hacer cada recorrido de cada par de escenas
             * Se podrían dar muchas repeticiones de casos, por lo que
             * conviene usar programación dinámica: FloydWarshall
             * TryFunc<string, IEnumerable<Edge<string>>> tryGetPath = PlaceController.graph.ShortestPathsDijkstra((x) => 1, "Fyrst");
            string target = "Dara";
            if(tryGetPath(target, out IEnumerable<Edge<string>> path))
            {
                foreach(var edge in path)
                {
                    
                    Console.WriteLine(edge);
                }
            }*/

            // Encuentra todos los caminos de coste menor entre cada par de vértices
            PlaceController.pathsFloyd = new FloydWarshallAllShortestPathAlgorithm<string, Edge<string>>(PlaceController.graph, (x) => 1);
            PlaceController.pathsFloyd.Compute();
            /* Pintar los caminos
             * foreach (string source in PlaceController.graph.Vertices)
            {
                foreach (string target in PlaceController.graph.Vertices)
                {
                    Console.WriteLine("from " + source + " to " + target);
                    Console.WriteLine("");
                    if (paths.TryGetPath(source, target, out IEnumerable<Edge<string>> path))
                    {
                        foreach (var edge in path)
                        {
                            Console.WriteLine(edge);
                        }
                    }
                }
            }*/
            PlaceController.m_places = new Dictionary<string, Place>();
            foreach (var place in placesList)
                PlaceController.m_places.Add(place.m_name, place);

            PlaceController.completePlaces();

            jsonstring = File.ReadAllText(m_chaptersPath);
            var questJsonList = JSONDecoder.Decode(jsonstring).ArrayValue;

            foreach (var questJson in questJsonList)
            {
                var scenesJson = questJson["m_scenes"].ArrayValue;

                var sceneList = new List<string>();
                foreach (var scene in scenesJson)
                    sceneList.Add((string)scene);

                var questId = (string)questJson["m_id"];

                StoryController.m_chapters.Add(questId, new Quest(questId, sceneList, (string)questJson["m_next"]));
            }

            jsonstring = File.ReadAllText(m_scenesPath);
            var sceneJsonList = JSONDecoder.Decode(jsonstring).ArrayValue;

            foreach(var sceneJson in sceneJsonList)
            {
                var dialoguesJson = sceneJson["m_dialogs"].ArrayValue;

                var dialogueList = new List<string>();
                foreach (var dialogue in dialoguesJson)
                    dialogueList.Add((string)dialogue);

                var sceneId = (string)sceneJson["m_id"];

                StoryController.m_storyScenes.Add(sceneId, new StoryScene(sceneId, (string)sceneJson["m_place"], (string)sceneJson["m_next"], (string) sceneJson["m_itemToGive"], (string)sceneJson["m_itemToTake"], dialogueList));
            }

            jsonstring = File.ReadAllText(m_storyFolder);
            var storiesJsonList = JSONDecoder.Decode(jsonstring).ArrayValue;

            foreach (var storyJson in storiesJsonList)
            {
                var chaptersJson = storyJson["m_chapters"].ArrayValue;


                HashSet<string> placesInvolved = new HashSet<string>();
                int totalScenes = 0;

                var chapterList = new List<string>();
                string source = null;
                foreach (var chapter in chaptersJson) { 
                    chapterList.Add((string)chapter);
                    totalScenes += StoryController.m_chapters[(string)chapter].m_scenes.Count;
                    foreach (var scene in StoryController.m_chapters[(string)chapter].m_scenes)
                    {
                        if (source != null)
                        {
                            var places = PlaceController.PathBetweenPlaces(source, StoryController.m_storyScenes[scene].m_place);
                            placesInvolved.UnionWith(places);
                        }
                        source = StoryController.m_storyScenes[scene].m_place;
                    }
                }

                var storyCharactersJson = storyJson["m_characters"].ArrayValue;
                var storyCharactersList = new List<string>();
                uint storyCharacterImportance = 0;
                foreach (var c in storyCharactersJson)
                {
                    storyCharactersList.Add((string)c);
                    storyCharacterImportance += CharacterController.characters[(string)c].relevance;
                }

                // StoryController.m_stories.Add(new Story((StoryType)(byte)storyJson["m_storyType"], chapterList));
                StoryController.m_stories.Add(new Story(chapterList, storyCharactersList, placesInvolved, totalScenes, storyCharacterImportance));
            }
            for(int i = 0; i < StoryController.m_stories.Count; i++)
            {
                Console.WriteLine(StoryController.m_stories.ElementAt(i).m_chapters[0] + " " + StoryController.m_stories.ElementAt(i).m_importance.ToString());
            }
            //Console.WriteLine("Most Important story: " + StoryController.m_stories.First().m_chapters[0] + " " + StoryController.m_stories.First().m_importance.ToString());
            //Console.WriteLine("Least Important story: " + StoryController.m_stories.Last().m_chapters[0] + " " + StoryController.m_stories.Last().m_importance.ToString());

            PlaceController.CompleteQuestsInPlace();
        }

        /// <summary>
        /// 
        /// Function to read a specific Dialog file. Reads all the text 
        /// contained in that file (if it exists) and then creates a new
        /// Dialog object from that JSON text. 
        /// 
        /// </summary>
        /// <param name="filePath"> (string) Path to the file. </param>
        /// <returns> (Dialog) New dialog created from the JSON text. </returns>
        public Dialog ReadDialogFile(string filePath)
        {
            // Open and read file 
            string jsonString = File.ReadAllText(filePath);

            var dialogJson = JSONDecoder.Decode(jsonString);

            var nodesJsonList = dialogJson["nodes"].ArrayValue;
            var nodes = new List<Node>();

            foreach(var nodeJson in nodesJsonList)
            {
                var optionsJsonList = nodeJson["options"].ArrayValue;
                var options = new List<Option>();

                foreach (var optionJson in optionsJsonList)
                    options.Add(new Option((int)optionJson["nodePtr"], (string)optionJson["text"]));

                nodes.Add(new Node((string)nodeJson["character"], (int)nodeJson["nextNode"], (string)nodeJson["text"], options));
            }


            return new Dialog((string)dialogJson["init"], nodes);
            /*try
            {
            } // try
            catch(FileLoadException e1)
            {
                // If there is any problem loading the file, throw exception
                Console.Error.WriteLine("File not loaded correctly: " + filePath);

                throw new FileLoadException("File not loaded correctly: " + filePath, e1);
            } // catch
            catch(FileNotFoundException e2)
            {
                // If the file does not exist, throw exception
                Console.Error.WriteLine("File not found: " + filePath);

                throw new FileNotFoundException("File not found: " + filePath, e2);
            } // catch*/
        } // readDialogFile
        
        /// <summary>
        /// 
        /// Searchs for all dialog files available in the dialog folder. Returns a 
        /// list with all the paths to those files.
        /// 
        /// </summary>
        /// <returns> (string[]) Files' paths list. </returns>
        public string[] locateDialogFiles()
        {
            return Directory.GetFiles(m_dialogFolder, "*.json", SearchOption.AllDirectories);
        } // locateDialogFiles

        public void makeExampleFiles()
        {
            //var options = new JsonSerializerOptions
            //{
            //    WriteIndented = true,
            //};

            List<Story> example_stories = new List<Story>();

            List<string> example_quest = new List<string>();
            example_quest.Add("C1");
            example_quest.Add("C2");
            example_stories.Add(new Story(StoryType.SECONDARY, example_quest));

            var example_stories_dict = new List<Dictionary<string, object>>();

            foreach (var story in example_stories)
                example_stories_dict.Add(story.toDictionary());

            // TODO: Crear constructor con todos los valores de historia que irán en el JSON


            List<Dialog> example_dialogues = new List<Dialog>();
            // TODO: Crear constructor con todos los valores de Dialog que irán en el JSON

            List<string> example_characters = new List<string>();
            example_characters.Add("Mario");
            example_characters.Add("Luigi");

            Dictionary<string, Place> example_places = new Dictionary<string, Place>();
            var tethePlaces = new List<string>();
            tethePlaces.Add("Sylvarant");
            tethePlaces.Add("Tower of Salvation");

            var sylvaPlaces = new List<string>();
            sylvaPlaces.Add("Tower of Salvation");
            sylvaPlaces.Add("Tethealla");

            var towerPlaces = new List<string>();
            sylvaPlaces.Add("Sylvarant");
            sylvaPlaces.Add("Tethealla");

            example_places.Add("Tethealla", new Place("Tethealla", tethePlaces, new List<string>()));
            example_places.Add("Sylvarant", new Place("Sylvarant", sylvaPlaces, new List<string>()));
            example_places.Add("Tower of Salvation", new Place("Tower of Salvation", towerPlaces, new List<string>()));


            var jsonString = JSONEncoder.Encode(example_characters);
            File.WriteAllText("Example_characters.json", jsonString);

            jsonString = JSONEncoder.Encode(example_stories_dict);
            File.WriteAllText("Example_stories.json", jsonString);

            jsonString = JSONEncoder.Encode(example_places);
            File.WriteAllText("Example_places.json", jsonString);
        }

        public List<string> getItems()
        {
            return m_items;
        }
    }
}
