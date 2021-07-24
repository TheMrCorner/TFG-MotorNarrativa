using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    public class StoryScene
    {
        internal Character[] characters;
        internal Place place;
        internal StoryScene[] nextScene; // This is set for different story branching and etc.
        internal bool consumed;

        public string m_id { get; }
        public string m_place { get; }
        public string m_next { get; }
        public string m_itemToGive { get; }
        public string m_itemToTake { get; }
        public List<string> m_dialogs { get; }
        public List<Dialog> dialogs { get; }

        internal StoryScene(string m_id, string m_place, string m_next, string m_itemToGive, string m_itemToTake, List<string> m_dialogs)
        {
            this.m_id = m_id;
            this.m_place = m_place;
            this.m_next = m_next;
            this.m_itemToGive = m_itemToGive;
            this.m_itemToTake = m_itemToTake;
            this.m_dialogs = m_dialogs;
            this.dialogs = new List<Dialog>();
        } // Constructor

        /// <summary>
        /// Set this scene as consumed.
        /// </summary>
        public void Consume()
        {
            consumed = true;
        } // Consume

        internal Character GetCharacter(string name)
        {
            for(int i = 0; i < characters.Length; i++)
            {
                if(characters[i].GetCharacterName() == name)
                {
                    return characters[i];
                } // if
            } // for

            return null; // Return null if character not exists
        } // GetCharacter

        internal Place GetPlace() => place;

        public bool GetConsumed() => consumed;

        /// <summary>
        /// 
        /// Returns the dialog associated with a specific character.
        /// 
        /// </summary>
        /// <param name="c"> (Character) Character of the dialog. </param>
        /// <returns> (Dialog) Dialog if found, (null) when not found</returns>
        internal Dialog GetDialogForCharacter(Character c)
        {
            foreach(Dialog d in dialogs)
            {
                //if(d.GetCharacter() == c)
                //{
                //    return d;
                //} // if
            } // foreach

            return null; // Return null if no dialog register for this character
        } // GetDialogForCharacter

        public StoryScene GetNextScene(int i) => nextScene[i];
    } // StoryScene
} // namespace
