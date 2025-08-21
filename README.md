# TMD.Challenge
Please build a Windows console application that allows users to explore the contents of a folder, view media file details, and manage a list of favorite files using C# with .NET 8.

# INTRODUCTION
* For this challenge, I decided to keep the project simple but still organized, inspired by the principles of Clean Architecture.
* The application is a .NET 8 console app, and I divided it into Models, Interfaces, Services, Data, and Program.
* If we compare this to Clean Architecture:
	* Models act as my core domain entities, independent of any framework.
	* Interfaces define contracts, similar to use cases or boundaries, making the code testable and easy to replace implementations.
	* Services are the application logic, where I implement business rules like exploring folders, extracting metadata, and managing favorites.
	* Data represents a very simple persistence layer, in this case a JSON file.
	* Program.cs is the presentation layer — here it’s just a console menu, but the services could easily be reused in a web or desktop UI.
* So while it’s not a full Clean Architecture implementation, the structure follows the same idea of keeping dependencies flowing inwards and separating responsibilities."

# DEPENDENCY INJECTION
* In a real production project, I would normally use Dependency Injection to register and resolve the services through their interfaces.
* For this challenge, since it’s a small console application and I wanted to focus my time on the core functionality, I decided to instantiate the services directly in Program.cs.
* In this case, I created the IFileExplorerService interface and implemented it in FileExplorerService.

# 1 - Enter Folder Path
* At this point, the program asks the user to enter a folder path.
* The service first validates if the folder exists and then retrieves the files, wrapping them in my BaseFile model.
* These files are stored in currentFiles, which acts as the in-memory collection that will be used in the following steps.
	* In a real-world scenario, instead of a simple list, I could use a memory cache or even a more sophisticated caching strategy to optimize repeated access to large folders.

# 2 - List Files
* In this step, I simply list the files previously loaded into currentFiles.
* The program prints them in a numbered list, so the user can easily reference a file by its index in later operations.
* This keeps the user experience simple and intuitive.

# 3 - View file details
* In this step, the user selects a file, and I use the MediaInfoService to extract metadata.
* The service first checks the general information like format and duration, and then it distinguishes if the file is a video or an audio.
	* If it’s video, it extracts codec, resolution and frame rate.
	* If it’s audio, it extracts codec, sample rate and bit rate.
* This approach works well for the challenge, but if we think about scalability, all this logic is currently inside a single method.

* A cleaner approach would be to use the Factory Pattern.
* I could define an IMediaFileFactory interface, and then have two separate implementations like VideoFileFactory and AudioFileFactory.
* Each factory would be responsible for building the right type of MediaFile with the correct metadata.
* This would improve the design by following the Single Responsibility Principle, and it would make the system easier to extend in the future — for example, if I wanted to support images or other types of media.

* NOTE: 
* Initially I had a single method handling both audio and video metadata extraction.
* I refactored this to use the Factory Pattern, with two separate factories: VideoFileFactory and AudioFileFactory.
* The MediaInfoService decides which factory to use depending on the file type.
* This makes the design cleaner, follows the Single Responsibility Principle, and makes it easy to extend — for example, I could add an ImageFileFactory without touching the existing code."

# 4 - List favorites
* In this step, I work with the FavoritesService, which is responsible for adding, removing, and listing favorite files.
* The service stores favorites in a JSON file (favorites.json), which makes persistence very lightweight and easy to maintain for this challenge.
* When the user chooses this option, I load the list of favorites and display them.

# 5 - Mark file as favorite
* In this step, the user selects a file from the current folder listing to mark it as a favorite.
* The program uses the FavoritesService.AddFavorite method, which first loads the current favorites from the JSON file.
* If the file is already present in the list, it is ignored to avoid duplicates.

# 6 - Remove file from favorites
* In this step, the user can remove a file from their list of favorites.
* The program asks for a file path, then calls the FavoritesService.RemoveFavorite method.
* Inside the service, it loads the list of favorites, removes all entries that match the given path, and saves the updated list back to the JSON file.