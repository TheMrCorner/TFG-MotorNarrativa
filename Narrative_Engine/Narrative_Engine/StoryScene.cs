using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrative_Engine
{
    class StoryScene
    {
        public Character[] characters;
        public Place place;
        public Dialog[] dialogs;
        public StoryScene[] nextScene; // This is set for different story branching and etc.
        public bool consumed;

        public StoryScene(Character[] c, Place p, Dialog[] d, StoryScene[] next)
        {
            characters = c;
            place = p;
            dialogs = d;
            nextScene = next;
            consumed = false;
        } // Constructor

        /// <summary>
        /// Set this scene as consumed.
        /// </summary>
        public void Consume()
        {
            consumed = true;
        } // Consume

        public Character GetCharacter(string name)
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

        public Place GetPlace() => place;

        public bool GetConsumed() => consumed;

        /// <summary>
        /// 
        /// Returns the dialog associated with a specific character.
        /// 
        /// </summary>
        /// <param name="c"> (Character) Character of the dialog. </param>
        /// <returns> (Dialog) Dialog if found, (null) when not found</returns>
        public Dialog GetDialogForCharacter(Character c)
        {
            foreach(Dialog d in dialogs)
            {
                if(d.GetCharacter() == c)
                {
                    return d;
                } // if
            } // foreach

            return null; // Return null if no dialog register for this character
        } // GetDialogForCharacter

        public StoryScene GetNextScene(int i) => nextScene[i];
    } // StoryScene
} // namespace
