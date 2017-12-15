@Echo off 
"C:\Program Files\7-Zip\7Z.EXE" a "U:\PROJECTS\Code BackUp\%DATE:/=-%_%TIME::=-%_Backup.7z" *.* -x!*.BAT 
PAUSE
