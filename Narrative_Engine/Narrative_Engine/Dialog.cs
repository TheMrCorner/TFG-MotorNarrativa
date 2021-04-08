using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    public class Option
    {
        public int nextNode;
        public string text;
    } // Option

    public class Node
    {
        public int character;
        public int nextNode;
        public string text;
        public Option[] options;

        public int GetCharacter()
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
        public bool rewarded = true;
        public Node[] nodeList; 

        public bool GetRewarded()
        {
            return rewarded;
        } // GetRewarded
    } // Dialog
} // namespace
