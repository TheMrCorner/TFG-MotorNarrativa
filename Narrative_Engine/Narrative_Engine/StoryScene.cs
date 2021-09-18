using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    public class StoryScene
    {
        public string id { get; }
        public string place { get; }
        public string next { get; }
        public string itemToGive { get; }
        public string itemToTake { get; }
        public List<string> dialogNames { get; }
        public List<Dialog> dialogs { get; }

        internal StoryScene(string id, string place, string next, string itemToGive, string itemToTake, List<string> dialogNames)
        {
            this.id = id;
            this.place = place;
            this.next = next;
            this.itemToGive = itemToGive;
            this.itemToTake = itemToTake;
            this.dialogNames = dialogNames;
            this.dialogs = new List<Dialog>();
        } // Constructor
    } // StoryScene
} // namespace
