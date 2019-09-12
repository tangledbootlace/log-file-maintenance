# log-file-maintenance

log-file-maintenance is a C# Console App used to delete log files or other specified file types in configured folders that are older than a specified date.

## Usage

Access App.config to edit keys/values to match the paths, file types, and retain length in days you desire. 

```C#
<add key="Path1" value="..\\TestData\\Example Folder 1" /> //File path of desired folder
<add key="FileType1" value=".log" />                       //File type of desired items to be deleted
<add key="RetainLength1" value="15" />                     //Number of days to keep files before deletion
```
Note: Be sure to enter keys/values for EACH of the 3 variables above.

I will be removing the Console.ReadKey() lines to eliminate need for user input and am using this application as a scheduled task to clean up log files for a number of company websites that receive high levels of traffic.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
