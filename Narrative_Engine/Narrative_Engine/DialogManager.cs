using System;
using System.Collections.Generic;
using System.IO;


namespace Narrative_Engine
{
    internal class DialogManager
    {
        private struct DialogFileData
        {
            public string path { get; }
            public bool Consumed;

            public DialogFileData(string p)
            {
                path = p;
                Consumed = false;
            } // Constructor
        }; // DialogFileData

        private Dictionary<string, DialogFileData> filesData;
        
        private FileManager fileManager;

        public DialogManager(FileManager man)
        {
            fileManager = man;

            string[] files = fileManager.LocateDialogFiles();

            filesData = new Dictionary<string, DialogFileData>();

            foreach (string f in files)
            {
                DialogFileData d = new DialogFileData(f);
                string name = Path.GetFileName(f);

                filesData.Add(name, d);                
            } // foreach
        } // Constructor


        public Dialog GetDialog(string d)
        {
            try
            {
                DialogFileData dat = filesData[d];

                if (!dat.Consumed)
                {
                    Dialog diag = fileManager.ReadDialogFile(dat.path);
                    return diag;
                } // if
                else
                {
                    return null;
                } // else
            } // try
            catch(Exception e)
            {
                return null;
            } // catch
        } // GetDialog
    } // DialogManager
        
} // namespace
