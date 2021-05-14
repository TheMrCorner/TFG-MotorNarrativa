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
    public class DialogController
    {

        private NarrativeEngine listener; // Control and comms
        private Dialog currentD;
        private string currentDFile;
        private Tuple<string, bool>[] filePaths;
        private FileManager fman;

        public DialogController(FileManager man)
        {
            fman = man;

            string[] files = fman.locateDialogFiles();

            filePaths = new Tuple<string, bool>[files.Length];

            int i = 0;

            foreach (string f in files)
            {
                Tuple<string, bool> file = new Tuple<string, bool>(f, false);

                filePaths[i] = file;

                i++;
            } // foreach
        } // Constructor

        public Dialog GetDialog(string d)
        {
            foreach(var data in filePaths)
            {
                if(data.Item1.Contains(d) && !data.Item2)
                {
                    Dialog diag = fman.ReadDialogFile(data.Item1);
                    return diag;
                } // if
            } // foreach

            return null;
        } // GetDialog

        public bool IsDialogConsumed(string d)
        {
            foreach (var data in filePaths)
            {
                if (data.Item1.Contains(d))
                {
                    return data.Item2;
                } // if
            } // foreach

            return false;
        } // IsDialogConsumed

        public void DialogEnded(string d)
        {
            // TODO: Implement, this should notify when a dialog has ended.
        } // Dialog Ended
    } // DialogController
        
} // namespace
