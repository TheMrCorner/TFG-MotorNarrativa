using System;
using Narrative_Engine;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager fileManager = new FileManager("Stories", "Dialogs", "Characters\\Characters.json", "Items", "Places");
            //fileManager.makeExampleFiles();

            Console.WriteLine("Loaded");
            return;
        }
    }
}
