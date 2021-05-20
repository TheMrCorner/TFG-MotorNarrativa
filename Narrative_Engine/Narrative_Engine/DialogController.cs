using System;
using System.Collections.Generic;
using System.IO;


namespace Narrative_Engine
{
    internal class DialogController
    {
        struct DialogFileData
        {
            public string path;
            public bool consumed;

            public DialogFileData(string p)
            {
                path = p;
                consumed = false;
            } // Constructor
        }; // DialogFileData

        private string currentDName;
        private Dictionary<string, DialogFileData> filesData;
        
        private FileManager fman;

        public DialogController(FileManager man)
        {
            fman = man;

            string[] files = fman.locateDialogFiles();

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

                if (!dat.consumed)
                {
                    Dialog diag = fman.ReadDialogFile(dat.path);
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

        public bool IsDialogConsumed(string d)
        {
            try
            {
                DialogFileData dat = filesData[d];
                return dat.consumed;
            } // try
            catch (Exception e)
            {
                return false;
            } // catch
        } // IsDialogConsumed

        public void StartDialog(string p)
        {
            currentDName = p;
        } // StartDialog

        public void DialogEnded()
        {
            try
            {
                DialogFileData dat = filesData[currentDName];
                dat.consumed = true;
                filesData[currentDName] = dat;
            } // try
            catch (Exception e)
            {
                throw new Exception("Dialog could not be ended.", e);
            } // catch
        } // Dialog Ended
    } // DialogController
        
} // namespace
