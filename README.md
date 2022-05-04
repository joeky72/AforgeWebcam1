# AforgeWebcam1
This is a project I took on to simplify a process for operators in my company and reduce error.
I built it because as an electronics engineering student, I want to learn programming and this was a great oppurtunity to start. 
I originally tried to create this as a UWP application, but learned that forms has extensive documentaion and is the place to start.  
  
This is a C# windows forms application using .NET Framework 4.7.2.
This project uses AForge to stream a camera preview to a picturebox.
The User Interface is designed to be as simple as possible for end users.
A user would open the program, select camera in the combobox, hit start camera, scan Lot ID barcode, then press save picture to capture the image preview to a file.  
Without this application, the image would be saved manually in the camera app, and the user would have to find and rename it themselves.  
  
Some obstacles I came accross were path permissions, memory leakage, instability, and GDI errors. The path permission was only an issue on the dev machine.
The memory leakage was occuring when the programmed attempted to save an image to a path that existed but that I did not have write permission to.
Using a seperate test path allowed me to verify the program works when deployed.
The instability/freezes/crashes were caused by two things, If the save button was pressed before start camera or if the camera has not finished initializing before pressing save.
The first issue was solved wiht an if else statement, checking if camera is running and restarting the program with a message if it is not. 
The other issue was fixed with Thread.Sleep(3000), which pauses the code for three seconds before attemting to save the image.  
If the file cannot be saved, an error message will appear with details.  
  
The file is signed with no password, I would recommend this file be signed properly and securely if it is to be deployed for more than a year. 
  
    -Joseph Conlan
