using System;
using System.IO;
using System.Linq;
using System.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LogFileMaintenance
{
    class Program
    {
        static void Main(string[] args)
        {
            var import = StartUp();                 //Display header and import configuration settings from json
            CleanUp(import);                        //Clean up files for each path and subdirectories                    
            Console.ReadKey(true);                  //Delete line for scheduled task
        }

        static AppSettingsSection GenerateList()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection section = (AppSettingsSection)config.GetSection("settings");
            return section;            
        }

        static ConfigurationList StartUp()
        {
            Console.WriteLine("=================== File Maintenance ===================");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("--- This application functions to clean up log files ---");
            Console.WriteLine("----- or other specified file types in configured ------");
            Console.WriteLine("---- folders that are older than a specified date. -----");
            Console.WriteLine("-------- Created on 9/10/2019 by tangledbootlace -------");
            Console.WriteLine("======================================================== \n");
            string config = ConfigurationManager.AppSettings["ConfigurationPaths"];
            var configList = JsonConvert.DeserializeObject<ConfigurationList>(config);
            return configList;            
        }

        static void ProcessDirectory(DirectoryInfo subFolders, string indent, ConfigurationSingle x)
        {
            var dir = subFolders.GetDirectories();
            foreach(DirectoryInfo d in dir)                 //if directory has subfolders, include them, retain parent folder config settings, and indent their entries in the log
            {
                ProcessDirectory(d, indent + "\t", x);
            }
            Console.WriteLine($"{indent} Cleaning files of type: {x.FileType} \n {indent} In Directory: {subFolders.FullName} \n {indent} That are older than {x.RetainLength.ToString()} days. \n");            
            foreach (FileInfo f in subFolders.GetFiles())
            {               
                DateTime testDate = DateTime.Now.AddDays(-x.RetainLength);
                DateTime fileAge = f.LastWriteTime;
                if ((f.Extension == x.FileType || x.FileType == ".*") && fileAge < testDate)
                {
                    Console.WriteLine($"File {f.Name} is older than {x.RetainLength.ToString()} days. Deleted . . . \n");
                    File.Delete(f.FullName);
                }
            }
        }

        static void CleanUp(ConfigurationList configList)
        {
            foreach (var x in configList.Configs)
            {
                DirectoryInfo dir = new DirectoryInfo(x.Path);
                ProcessDirectory(dir, string.Empty, x);                                
                Console.WriteLine("-------------------------------------------------------- \n");
            }            
        }
    }

    class ConfigurationList
    {
        public List<ConfigurationSingle> Configs { get; set; }
    }

    class ConfigurationSingle
    {
        public string Path { get; set; }
        public string FileType { get; set; }
        public int RetainLength { get; set; }
    }
}
