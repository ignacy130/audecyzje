#Before running the script:
#
#1.Create folder for each PDF downloaded and you want to read and put the files inside (Check code in WebCrawler)
#2.Download pdfimages.exe and place it in home folder next to this script //not sure- check it
#3.Download and install magick convert (command-line tool)
#4.Download and Install tesseract (4.0.0 was used last time)
#5.Download pol.traineddata and check if tesseract found it (you may need to add folder to PATH)
#Remeber -> Tesseract has problems with reading FILENAMES with characters different than Latin/English

# Extract images from each pdf in directories. Have pdfimages.exe in root!
Get-ChildItem -Recurse -Directory | ForEach-Object {
   $folderPath = $_.FullName
   $dirName = $_.Name
   cd $_.BaseName
   Get-ChildItem -Recurse -File | ForEach-Object {
     # ..\.\pdfimages.exe -raw $_.Name .\
	 $filePath = Join-Path $folderPath $_.Name
	 $result = "$dirName.jpg"
	 magick convert -geometry 3200x3200 -density 300x300 -quality 100 "$filePath" $result
   }
   cd ..
}

# OCR each image in each folder and return text to file named {iterator}-directoryname.txt
Get-ChildItem -Recurse -Directory | ForEach-Object {
    $folder = $_.FullName
    $folderName = $_.Name
    cd $_.BaseName
    $i = 0
    Get-ChildItem -Recurse -File | ForEach-Object {
       If ($_.extension -eq ".jpg") {
         $dirpath = Join-Path $folder $_.Name
         #% {$dirpath}
         $txtPath = Join-Path $folder "$i-$folderName.txt"
         #% {$txtPath}
         Invoke-Expression "& `"C:\Program Files (x86)\Tesseract-OCR\tesseract.exe`" $dirpath $txtPath --oem 3 -l pol"
         $i++
       }
    }
    cd ..
}