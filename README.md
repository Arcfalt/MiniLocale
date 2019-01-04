# MiniLocale
A basic unity localization system for use in smaller scoped games/applications that are able to have all of their strings loaded in memory at once. 
Supports importing/exporting CSV files for spreadsheet editing of strings. 
Currently unfinished and not yet suitable for use. 

# Requirements
This requires the CsvHelper package (https://joshclose.github.io/CsvHelper/) to be installed in your unity project. MiniLocale only uses this in editor scripts, so unless you also require usage of the CsvHelper dll at runtime, it is reccomended to set your dll to only compile in the editor and not on any platforms.