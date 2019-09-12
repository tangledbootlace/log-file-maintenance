# log-file-maintenance
C# Console App used to delete log files or other specified file types in configured folders that are older than a specified date.

To use, access App.config and edit keys/values in <settings> to match the paths, filetypes, and retain length in days you desire.

Path key = File path of desired folder.
FileType = File type of desired items.
RetainLength = Number of days the application should wait before deleting the file. For example, with a value of 15,
				the application will delete items that were created 15 days or more in the past.
				
Note: Be sure to enter keys/values for each of the above variables for your selection.

I will be removing the Console.ReadKey() lines to eliminate need for user input and am using this application as a scheduled task
to clean up log files for numerous websites that receive high levels of traffic.
