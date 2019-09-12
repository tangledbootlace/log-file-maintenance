using System;
using System.IO;
using System.Linq;
using System.Configuration;

namespace LogFileMaintenance
{
    class Program
    {
        static void Main(string[] args)
        {
            AppSettingsSection configSettings = GenerateList();                         //Get settings from App.config
            StartUp(configSettings);                                                    //Display settings information
            CleanUp(configSettings);                                                    //Clean up files for each path
            Console.WriteLine("Maintenance Complete. Press any key to exit . . .");
            Console.ReadKey(true);
        }

        static AppSettingsSection GenerateList()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection section = (AppSettingsSection)config.GetSection("settings");
            return section;            
        }

        static void StartUp(AppSettingsSection settings)
        {
            Console.WriteLine("================= Log File Maintenance =================");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("--- This application functions to clean up log files ---");
            Console.WriteLine("----- or other specified file types in configured ------");
            Console.WriteLine("---- folders that are older than a specified date. -----");
            Console.WriteLine("-------- Created on 9/10/2019 by tangledbootlace -------");
            Console.WriteLine("========================================================");
            Console.WriteLine();

            Console.WriteLine(settings.SectionInformation.GetRawXml());
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Settings Loaded. Press any key to remove files . . .");
            Console.ReadKey(true);
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine();
        }

        static void CleanUp(AppSettingsSection settings)
        {
            int count = settings.Settings.AllKeys.Count();
            for (int i = 0; i < (count - 1); i++)                                       //Get 3 configuration settings for each entry in App.config
            {
                string setPath = settings.Settings.AllKeys.GetValue(i).ToString();
                DirectoryInfo dir = new DirectoryInfo(settings.Settings[setPath].Value);
                i++;
                string setFileType = settings.Settings.AllKeys.GetValue(i).ToString();
                string fileType = settings.Settings[setFileType].Value;
                i++;
                string setRetainLength = settings.Settings.AllKeys.GetValue(i).ToString();
                int retainLength = int.Parse(settings.Settings[setRetainLength].Value);

                Console.WriteLine("Cleaning files of type: " + fileType + "\n" + "In directory: " + dir.ToString() + "\n" + "That are older than " + retainLength.ToString() + " days.");
                Console.WriteLine();

                foreach (FileInfo f in dir.GetFiles())
                {
                    DateTime testDate = DateTime.Now.AddDays(-retainLength);
                    DateTime fileAge = f.LastWriteTime;
                    if (f.Extension == fileType && fileAge < testDate)
                    {
                        Console.WriteLine("File " + f.Name + " is older than " + retainLength.ToString() + " days. Deleted . . .");
                        File.Delete(f.FullName);
                    }
                }
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine();
            }
        }
    }
}
