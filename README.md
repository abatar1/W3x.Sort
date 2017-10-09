# W3x.Sort
Sorter for w3x maps written on c#.

# Command line parameters
* Parameters to process all files to directories named by max player number:
```sh
-t All
```
* Parameters to process files except files with keyword. Files which contains keyword moving to directory with %keyword% name.
```sh
-t ByName -k %keyword%
```
* Parameters to process files except files with keywords described in ignore.json. Files which contains keywords moving to corresponding directory with "Name" name.
```sh
-t Ignore
```
An example of ignore.json structure:
```sh
[
   {
      "Name":"Anime",
      "Keywords":[
         "anime",
         "naruto",
         "narutoarena",
         "narutobalance"
      ]
   },
   {
      "Name":"Stuff",
      "Keywords":[
         "petri",
         "petribalance",
         "dota allstar"
      ]
   }
]
```
