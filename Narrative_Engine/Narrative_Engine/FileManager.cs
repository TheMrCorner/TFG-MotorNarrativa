using System;
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
        string m_storyFolder;
        string m_dialogFolder;
        string m_charactersPath;
        string m_itemsPath;
        string m_placesPath;
        string m_chaptersPath;
        string m_scenesPath;

        List<string> items;


        public FileManager(string generalPath, string storyFolder, string chapterPath, string scenePath, string dialogFolder, string charactersPath, string itemsPath, string placesPath)
        {
            // TEST
            string generalPath1 = "../../../" + generalPath + "/";
            // m_generalPath = generalPath + "/";
            m_storyFolder = generalPath1 + storyFolder;
            m_dialogFolder = generalPath1 + dialogFolder;
            m_charactersPath = generalPath1 + charactersPath;
            m_itemsPath = generalPath1 + itemsPath;
            m_placesPath = generalPath1 + placesPath;
            m_chaptersPath = generalPath1 + chapterPath;
            m_scenesPath = generalPath1 + scenePath;
        }

        public void ReadFiles()
        {
            var jsonString = File.ReadAllText(m_charactersPath);
            var charactersJson = JSONDecoder.Decode(jsonString).ArrayValue;
            var charactersList = new Dictionary<string, Character>();
            uint totalImportance = 0;
            foreach (var character in charactersJson)
            {
                charactersList.Add((string)character["m_name"], new Character((string)character["m_name"], (uint)character["m_relevance"]));
                totalImportance += (uint)character["m_relevance"];
            }
            CharacterManager.characters = charactersList;
            CharacterManager.totalImportance = totalImportance;

            jsonString = File.ReadAllText(m_itemsPath);
            var itemList = JSONDecoder.Decode(jsonString).ArrayValue;
            items = new List<string>();
            foreach (var item in itemList)
                items.Add((string)item);

            jsonString = File.ReadAllText(m_placesPath);
            var placesJsonList = JSONDecoder.Decode(jsonString).ArrayValue;

            var placesList = new List<Place>();
            var edges = new List<Edge<string>>();
            foreach (var placeJson in placesJsonList)
            {
                var adjacentJson = placeJson["m_adjacentPlacesNames"].ArrayValue;
                var adjacentPlaces = new List<string>();
                foreach (var adjacent in adjacentJson)
                {
                    adjacentPlaces.Add((string)adjacent);
                    edges.Add(new Edge<string>((string)placeJson["m_name"], (string)adjacent));
                }

                var genericDialogs = new List<string>();
                foreach(var genericDialog in placeJson["m_genericDialogs"].ArrayValue)
                    genericDialogs.Add((string)genericDialog);

                placesList.Add(new Place((string)placeJson["m_name"], adjacentPlaces, genericDialogs));
            }
            PlaceManager.graph = edges.ToAdjacencyGraph<string, Edge<string>>();
            /* Dijktra con pesos 1 equivale a BFS
             * Habría que hacer cada recorrido de cada par de escenas
             * Se podrían dar muchas repeticiones de casos, por lo que
             * conviene usar programación dinámica: FloydWarshall
            }*/

            // Encuentra todos los caminos de coste menor entre cada par de vértices
            PlaceManager.pathsFloyd = new FloydWarshallAllShortestPathAlgorithm<string, Edge<string>>(PlaceManager.graph, (x) => 1);
            PlaceManager.pathsFloyd.Compute();
            
            PlaceManager.places = new Dictionary<string, Place>();
            foreach (var place in placesList)
                PlaceManager.places.Add(place.name, place);

            PlaceManager.CompletePlaces();

            jsonString = File.ReadAllText(m_chaptersPath);
            var questJsonList = JSONDecoder.Decode(jsonString).ArrayValue;

            foreach (var questJson in questJsonList)
            {
                var scenesJson = questJson["m_scenes"].ArrayValue;

                var sceneList = new List<string>();
                foreach (var scene in scenesJson)
                    sceneList.Add((string)scene);

                var questId = (string)questJson["m_id"];

                NarrativeEngine.GetQuests().Add(questId, new Quest(questId, sceneList, (string)questJson["m_next"]));
            }

            jsonString = File.ReadAllText(m_scenesPath);
            var sceneJsonList = JSONDecoder.Decode(jsonString).ArrayValue;

            foreach(var sceneJson in sceneJsonList)
            {
                var dialoguesJson = sceneJson["m_dialogs"].ArrayValue;

                var dialogueList = new List<string>();
                foreach (var dialogue in dialoguesJson)
                    dialogueList.Add((string)dialogue);

                var sceneId = (string)sceneJson["m_id"];

                NarrativeEngine.GetStoryScenes().Add(sceneId, new StoryScene(sceneId, 
                    (string)sceneJson["m_place"], (string)sceneJson["m_next"], (string) 
                    sceneJson["m_itemToGive"], (string)sceneJson["m_itemToTake"], dialogueList));
            }

            jsonString = File.ReadAllText(m_storyFolder);
            var storiesJsonList = JSONDecoder.Decode(jsonString).ArrayValue;

            foreach (var storyJson in storiesJsonList)
            {
                var chaptersJson = storyJson["m_chapters"].ArrayValue;


                HashSet<string> placesInvolved = new HashSet<string>();
                int totalScenes = 0;

                var chapterList = new List<string>();
                string source = null;
                foreach (var chapter in chaptersJson) { 
                    chapterList.Add((string)chapter);
                    totalScenes += NarrativeEngine.GetQuests()[(string)chapter].sceneNames.Count;
                    foreach (var scene in NarrativeEngine.GetQuests()[(string)chapter].sceneNames)
                    {
                        if (source != null)
                        {
                            var places = PlaceManager.PathBetweenPlaces(source, NarrativeEngine.GetStoryScenes()[scene].place);
                            placesInvolved.UnionWith(places);
                        }
                        source = NarrativeEngine.GetStoryScenes()[scene].place;
                    }
                }


                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("----------------------Story: " + chapterList[0] + "---------------------------");
                Console.WriteLine("-------------------------------------------------------------");
                foreach (var place in placesInvolved)
                {
                    Console.WriteLine(place);
                }
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("-------------------------------------------------------------");

                var storyCharactersJson = storyJson["m_characters"].ArrayValue;
                var storyCharactersList = new List<string>();
                uint storyCharacterImportance = 0;
                foreach (var c in storyCharactersJson)
                {
                    storyCharactersList.Add((string)c);
                    storyCharacterImportance += CharacterManager.characters[(string)c].relevance;
                }

                StoryManager.stories.Add(new Story(chapterList, storyCharactersList, placesInvolved, totalScenes, storyCharacterImportance));
            }
            for(int i = 0; i < StoryManager.stories.Count; i++)
            {
                Console.WriteLine(StoryManager.stories.ElementAt(i).chapters[0] + " " + StoryManager.stories.ElementAt(i).importance.ToString());
            }

            PlaceManager.CompleteQuestsInPlace();
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
        } // readDialogFile
        
        /// <summary>
        /// 
        /// Searchs for all dialog files available in the dialog folder. Returns a 
        /// list with all the paths to those files.
        /// 
        /// </summary>
        /// <returns> (string[]) Files' paths list. </returns>
        public string[] LocateDialogFiles()
        {
            return Directory.GetFiles(m_dialogFolder, "*.json", SearchOption.AllDirectories);
        } // locateDialogFiles

        public void MakeExampleFiles()
        {
            //var options = new JsonSerializerOptions
            //{
            //    WriteIndented = true,
            //};

            List<Story> example_stories = new List<Story>();

            List<string> example_quest = new List<string>();
            example_quest.Add("C1");
            example_quest.Add("C2");
            example_stories.Add(new Story(StoryType.Secondary, example_quest));

            var example_stories_dict = new List<Dictionary<string, object>>();

            foreach (var story in example_stories)
                example_stories_dict.Add(story.ToDictionary());

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
    }
}
