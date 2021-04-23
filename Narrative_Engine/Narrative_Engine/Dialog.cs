namespace Narrative_Engine
{
    class Option
    {
        public int nextNode;
        public string text;
    } // Option

    class Node
    {
        public Character character;
        public int nextNode;
        public string text;
        public Option[] options;

        public Character GetCharacter()
        {
            return character;
        } // GetCharacter

        public int GetNextNode()
        {
            return nextNode;
        } // GetNextNode

        public int GetNumOptions()
        {
            return options.Length;
        } // GetNumOptions
    } // Node

    class Dialog
    {
        public Character initCharacter; // Character that initializes the dialog
        public bool rewarded = true;
        public Node[] nodeList;

        public bool GetRewarded() => rewarded;

        public Character GetCharacter() => initCharacter;
    } // Dialog
} // namespace
