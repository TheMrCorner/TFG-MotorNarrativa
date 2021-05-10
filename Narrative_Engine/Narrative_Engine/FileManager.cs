using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Narrative_Engine
{
    public class FileManager
    {
        static FileManager s_instance;

        string m_storyFolder;
        string m_dialogFolder;
        string m_charactersPath;
        string m_itemsPath;
        string m_placesPath;
        string m_chaptersPath;
        string m_scenesPath;

        List<Story> m_stories;
        List<Dialog> m_dialogues;
        List<string> m_characters;
        List<string> m_items;

        Dictionary<string, Place> m_places;
        Dictionary<string, Quest> m_chapters;
        Dictionary<string, StoryScene> m_storyScenes;

        public FileManager(string storyFolder, string chapterPath, string scenePath, string dialogFolder, string charactersPath, string itemsPath, string placesPath)
        {
            m_storyFolder = storyFolder;
            m_dialogFolder = dialogFolder;
            m_charactersPath = charactersPath;
            m_itemsPath = itemsPath;
            m_placesPath = placesPath;
            m_chaptersPath = chapterPath;
            m_scenesPath = scenePath;
        }

        static void initConfiguration(string configurationPath)
        {
            var jsonstring = File.ReadAllText(configurationPath);
            s_instance = JsonSerializer.Deserialize<FileManager>(jsonstring);
        }

        public static FileManager getInstance()
        {
            return s_instance;
        }

        public void readFiles()
        {
            var jsonstring = File.ReadAllText(m_charactersPath);
            m_characters = JsonSerializer.Deserialize<List<string>>(jsonstring);

            jsonstring = File.ReadAllText(m_itemsPath);
            m_items = JsonSerializer.Deserialize<List<string>>(jsonstring);

            jsonstring = File.ReadAllText(m_placesPath);
            var placesList = JsonSerializer.Deserialize<List<Place>>(jsonstring);

            m_places = new Dictionary<string, Place>();
            foreach (var place in placesList)
                m_places.Add(place.m_name, place);

            jsonstring = File.ReadAllText(m_storyFolder);
            var storyList = JsonSerializer.Deserialize<List<Story>>(jsonstring);

            jsonstring = File.ReadAllText(m_chaptersPath);
            var chaptersList = JsonSerializer.Deserialize<List<Quest>>(jsonstring);

            m_chapters = new Dictionary<string, Quest>();
            foreach (var quest in chaptersList)
                m_chapters.Add(quest.m_id, quest);

            jsonstring = File.ReadAllText(m_scenesPath);
            var scenesList = JsonSerializer.Deserialize<List<StoryScene>>(jsonstring);

            m_storyScenes = new Dictionary<string, StoryScene>();
            foreach (var scene in scenesList)
                m_storyScenes.Add(scene.m_id, scene);

            int i = 0;

            while (File.Exists(m_dialogFolder + i.ToString()))
            {
                jsonstring = File.ReadAllText(m_dialogFolder + i.ToString());
                m_dialogues = JsonSerializer.Deserialize<List<Dialog>>(jsonstring);
                ++i;
            }
        }

        public void makeExampleFiles()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            List<Story> example_stories = new List<Story>();

            List<string> example_quest = new List<string>();
            example_stories.Add(new Story(StoryType.SECONDARY, example_quest));
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

            example_places.Add("Tethealla", new Place("Tethealla", tethePlaces));
            example_places.Add("Sylvarant", new Place("Sylvarant", sylvaPlaces));
            example_places.Add("Tower of Salvation", new Place("Tower of Salvation", towerPlaces));


            var jsonString = JsonSerializer.Serialize(example_characters, options);
            File.WriteAllText("Example_characters.json", jsonString);


            jsonString = JsonSerializer.Serialize(example_places, options);
            File.WriteAllText("Example_places.json", jsonString);
        }

        public List<string> getItems()
        {
            return m_items;
        }
    }
}
