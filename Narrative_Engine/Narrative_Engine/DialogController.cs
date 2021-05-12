using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Narrative_Engine
{
    class DialogController
    {

        /// <summary>
        /// 
        /// Dictionary containing all dialogs and a flag notifying wether 
        /// the dialog has been used or not. 
        /// 
        /// </summary>
        private Dictionary<string, Tuple<Dialog, bool>> dialogMap;
        private NarrativeEngine listener; // Control and comms

        public DialogController(NarrativeEngine ne, FileManager man)
        {
            //dialogMap = new Dictionary<Dialog, bool>();

            // TODO: Maybe using the name of the file...
            // Search for all available files of dialog and generate a list with the names and paths
            // to those files.
            List<Dialog> readDialogs = null; // Future version etc.

            foreach(Dialog d in readDialogs)
            {
                dialogMap.Add(d.GetCharacter().GetCharacterName(), new Tuple<Dialog, bool>(d, false));
            } // foreach*/
        } // Constructor

        /*public Dialog GetDialog(string d)
        {
            if (dialogMap[d].Item2)
            {
                return null;
            } // if
            else
            {
                return dialogMap[d].Item1;
            } // else
        } // GetDialog

        public bool IsDialogConsumed(string d)
        {
            return dialogMap[d].Item2;
        } // IsDialogConsumed

        public void DialogEnded(string d)
        {
            Tuple<Dialog, bool> diag = dialogMap[d];

            // Notify Engine to update scenes and quests
        } // Dialog Ended */
    } // DialogController
        
} // namespace
