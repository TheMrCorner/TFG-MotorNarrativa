using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    internal static class CharacterManager
    {
        internal static Dictionary<string, Character> characters { get; set; } = new Dictionary<string, Character>();
        internal static uint totalImportance { get; set; } = 0;
    }
}
