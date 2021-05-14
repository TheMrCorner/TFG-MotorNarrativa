using System;
using Narrative_Engine;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager fileManager = new FileManager("Story.json", "Chapters.json", "Scenes.json", "Dialogs.json", "Characters.json", "Items.json", "Place.json");
            fileManager.makeExampleFiles();
			//fileManager.readFiles();
            return;
        }
    }
}
