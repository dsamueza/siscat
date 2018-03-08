using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
namespace sisadoc.Tasks.Seguridad
{
    public class DeleteFile
    {

        public bool borrarArch(FileInfo[] fileNames) {
            bool ok = false;
            string excluirArch = ConfigurationSettings.AppSettings["excluirArch"];
            string[] words = excluirArch.Split(';');
            bool bbtex = true;
            foreach (FileInfo file in fileNames)
            {
              
                try
                {
                   
                    //foreach (string ursist in ))
                    ////{
                    ////    if (file.Name == ursist)
                    ////        bbtex = false;
                    ////}
                    bbtex = words.Contains(file.Name);
                    if (!bbtex)
                    {
                        file.Delete();
                        

                    }
                    bbtex = true;
                    //    esvalido = util.IsValidFileType(SourceStream, (string.IsNullOrEmpty(file.Extension) ? "" : file.Extension.Substring(1, file.Extension.Length - 1)));

                }
                catch (Exception ex)
                {
                    ok = false;
                
                }
                ok = true;

            }

            return ok;
        }

        public bool borrarDir(DirectoryInfo[] dirInfos)
        {
            bool ok = false;
             
            //DirectoryInfo[] dirInfos = dirInfo.GetDirectories("*.*");
            //FileInfo[] fileNames = dirInfo.GetFiles("*.*");
            foreach (DirectoryInfo dir in dirInfos){
       
                dir.Delete(true);
               
            }


            return ok;
        }
        public  string DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
              string ok = "das";
            try
            {
                // Get the subdirectories for the specified directory.
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);

                if (!dir.Exists)
                {
                    throw new DirectoryNotFoundException(
                        "Directorio no exite: "
                        + sourceDirName);
                }

                DirectoryInfo[] dirs = dir.GetDirectories();
                // If the destination directory doesn't exist, create it.
                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                }

                // Get the files in the directory and copy them to the new location.
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, true);
                }

                // If copying subdirectories, copy them and their contents to new location.
                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string temppath = Path.Combine(destDirName, subdir.Name);
                        DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                    }
                }
                return ok = "1";
            }catch(Exception e)
            {
                return ok = e.ToString();
            }
        }

    }
}