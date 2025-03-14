# spac_uge_3

## todo
Wac dataformat in parser at LoadFromCSV with line[15]

## How to run
Unzip the folder *Data.zip* such that there are files in a folder named *Data* in this project. Do the same with the *Cerial pictures.zip* 
Add a .env file with: 
``` env
DBPASSWORD=
DBUSER=
DBNAME=
```
Run the *cerial.sql* on the database with the same name as you put in for the **DBNAME**
Run ```dotnet restore``` in terminal, if this thowrs a nuget error do [this](#nuget-error).  
Then to run ```dotnet run``` in terminal

### nuget error
Go in to %appdata%/Roaming/nuget and remove the file named nuget then retry