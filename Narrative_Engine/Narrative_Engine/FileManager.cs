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
    class FileManager
    {
        static FileManager s_instance;

        string m_storyFolder;
        string m_dialogFolder;
        string m_charactersPath;
        string m_itemsPath;
        string m_placesPath;

        List<Story> m_stories;
        List<Dialog> m_dialogues;
        List<string> m_characters;
        List<string> m_items;

        Dictionary<string, Place> m_places;

        public FileManager(string storyFolder, string dialogFolder, string charactersPath, string itemsPath, string placesPath)
        {
            m_storyFolder = storyFolder;
            m_dialogFolder = dialogFolder;
            m_charactersPath = charactersPath;
            m_itemsPath = itemsPath;
            m_placesPath = placesPath;
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

            foreach (var place in placesList)
                m_places.Add(place.m_name, place);

            int i = 0;

            while (File.Exists(m_storyFolder + i.ToString()))
            {
                jsonstring = File.ReadAllText(m_storyFolder + i.ToString());
                m_stories = JsonSerializer.Deserialize<List<Story>>(jsonstring);
                ++i;
            }

            while (File.Exists(m_dialogFolder + i.ToString()))
            {
                jsonstring = File.ReadAllText(m_dialogFolder + i.ToString());
                m_dialogues = JsonSerializer.Deserialize<List<Dialog>>(jsonstring);
                ++i;
            }
        }

        public void makeExampleFiles()
        {
            List<Story> example_stories = new List<Story>();

            example_stories.Add(new Story(StoryType.SECONDARY));
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


            var jsonString = JsonSerializer.Serialize(example_characters);
            File.WriteAllText("Example_characters.json", jsonString);


            jsonString = JsonSerializer.Serialize(example_places);
            File.WriteAllText("Example_places.json", jsonString);
        }
    }
}
