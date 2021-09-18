using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    internal class Character
    {

        internal string characterName { get; }
        internal uint relevance { get; }

        public Character(string characterName, uint relevance)
        {
            this.characterName = characterName;
            this.relevance = relevance;
        }
    }
}