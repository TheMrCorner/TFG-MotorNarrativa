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

        public Option(int nodePtr, string text)
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
        public int character { get; }
        public int emotion { get; }
        public int nextNode { get; }
        public string text { get; }
        public List<Option> options { get; }

        public Node(int character, int emotion, int nextNode, string text, List<Option> options)
        {
            this.emotion = emotion;
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
        public int init { get; } // Character that initializes the dialog
        public List<Node> nodes { get; }

        public Dialog(int init, List<Node> nodes)
        {
            this.init = init;
            this.nodes = nodes;
        } // Constructor
    } // Dialog
} // namespace
