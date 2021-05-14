using System;
using Narrative_Engine;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager fileManager = new FileManager("Story.json", "Chapters.json", "Scenes.json", "Dialogs", "Characters.json", "Items.json", "Place.json");
            fileManager.makeExampleFiles();

            
            DialogController test = new DialogController(fileManager);
            Dialog d = test.GetDialog("P1_S1_Messenger");

            int x = 0 + 1;
            return;
        }
    }
}
