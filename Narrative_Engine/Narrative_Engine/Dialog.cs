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

        public Node(Character c, int next, string tx, Option[] opts)
        {
            character = c;
            nextNode = next;
            text = tx;
            options = opts;
        }

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

        public string GetText() => text;
    } // Node

    class Dialog
    {
        public Character initCharacter; // Character that initializes the dialog
        public bool rewarded = true;
        public Node[] nodeList;

        public Dialog(Character init, Node[] list)
        {
            initCharacter = init;
            nodeList = list;
        }

        public bool GetRewarded() => rewarded;

        public Character GetCharacter() => initCharacter;

        public Node[] GetNodes() => nodeList;
    } // Dialog
} // namespace
