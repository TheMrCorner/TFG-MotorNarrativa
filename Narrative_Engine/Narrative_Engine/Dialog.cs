using System.Collections.Generic;

namespace Narrative_Engine
{
    /// <summary>
    /// 
    /// Option object. Represents the option that is going to be shown to the player, having
    /// the text that will be written and the node that will load next. 
    /// 
    /// </summary>
    public class Option
    {
        public int nodePtr { get; }
        public string text { get; }

        internal Option(int nodePtr, string text)
        {
            this.nodePtr = nodePtr;
            this.text = text;
        } // Constructor
    } // Option

    /// <summary>
    /// 
    /// Node object. Represents the minimum expression of a dialog, which represents the
    /// character, the next node in the conversation, the options that it has and the text
    /// of the current node. 
    /// 
    /// </summary>
    public class Node
    {
        public string character { get; }
        public int nextNode { get; }
        public string text { get; }
        public List<Option> options { get; }

        internal Node(string character, int nextNode, string text, List<Option> options)
        {
            this.character = character;
            this.nextNode = nextNode;
            this.text = text;
            this.options = options;
        } // Constructor
    } // Node

    /// <summary>
    /// 
    /// Dialog object. Holds a list with all the different Nodes that shape the dialog and
    /// the name of the character that initiates it. 
    /// 
    /// </summary>
    public class Dialog
    {
        public string init { get; } // Character that initializes the dialog
        public List<Node> nodes { get; }

        internal Dialog(string init, List<Node> nodes)
        {
            this.init = init;
            this.nodes = nodes;
        } // Constructor
    } // Dialog
} // namespace
