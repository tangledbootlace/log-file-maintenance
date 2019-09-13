# log-file-maintenance

log-file-maintenance is a C# Console App used to delete log files or other specified file types in configured folders that are older than a specified date.

Created: 9/10/2019

## Usage

Access App.config to edit/add values to Json object that match the paths, file types, and retain length in days you desire.

 

```C#
<appSettings>
    <add key="ConfigurationPaths" value="{'Configs':[ {'path':'..\\TestData\\Example Folder 1', 'fileType': '.log', 'retainLength': 15 },
                                                    {'path':'..\\TestData\\Example Folder 2', 'fileType': '.log', 'retainLength': 10 },
                                                    {'path':'..\\TestData\\Example Folder 3', 'fileType': '.log', 'retainLength': 7 } ]}"/>
  </appSettings>
  
  //'path' = Directory containing files/subfolders
  //'fileType' = File extension on desired files. Use '.*'  if you wish to clean up files of all types
  //'retainLength' = Number of days to keep files before deltion
```
Note: Be sure to enter keys/values for EACH of the 3 variables above.

I will be removing the Console.ReadKey() lines to eliminate need for user input and am using this application as a scheduled task to clean up log files for a number of company websites that receive high levels of traffic.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
