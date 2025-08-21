using TMD.Challenge.Application.Factory;
using TMD.Challenge.Application.Models;
using TMD.Challenge.Application.Services;

var fileExplorer = new FileExplorerService();

var videoFactory = new VideoFileFactory();
var audioFactory = new AudioFileFactory();
var mediaInfoService = new MediaInfoService(videoFactory, audioFactory);

var favoritesService = new FavoritesService();

List<BaseFile> currentFiles = new();
string currentFolder = string.Empty;

while (true)
{
    Console.WriteLine("\n===== Media Explorer =====");
    Console.WriteLine("1. Enter folder path");
    Console.WriteLine("2. List files");
    Console.WriteLine("3. View file details");
    Console.WriteLine("4. List favorites");
    Console.WriteLine("5. Mark file as favorite");
    Console.WriteLine("6. Remove file from favorites");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");

    var option = Console.ReadLine();

    try
    {
        switch (option)
        {
            case "1":
                Console.Write("Enter the folder path: ");
                
                currentFolder = Console.ReadLine() ?? string.Empty;
                currentFiles = fileExplorer.GetFilesFromFolder(currentFolder);
                
                Console.WriteLine($"{currentFiles.Count} files found.");
                break;

            case "2":
                if (currentFiles.Count == 0)
                {
                    Console.WriteLine("No folder loaded. Use option 1 first.");
                    break;
                }

                for (int i = 0; i < currentFiles.Count; i++)
                    Console.WriteLine($"{i + 1}. {currentFiles[i].FileName}");

                break;

            case "3":
                if (currentFiles.Count == 0)
                {
                    Console.WriteLine("No folder loaded.");
                    break;
                }
                Console.Write("Select the file number: ");
                if (int.TryParse(Console.ReadLine(), out int fileIndex) &&
                    fileIndex > 0 && fileIndex <= currentFiles.Count)
                {
                    var file = currentFiles[fileIndex - 1];
                    var mediaFile = mediaInfoService.CreateMediaFile(file);

                    Console.WriteLine($"\n--- File Details ---");
                    Console.WriteLine($"Name: {mediaFile.FileName}");
                    Console.WriteLine($"Duration: {mediaFile.Duration}");
                    Console.WriteLine($"Format: {mediaFile.Format}");
                    Console.WriteLine($"Codec: {mediaFile.Codec}");
                    Console.WriteLine($"Resolution: {mediaFile.Resolution}");
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
                break;

            case "4":
                var favorites = favoritesService.GetFavorites();
                
                if (!favorites.Any())
                {
                    Console.WriteLine("No favorites saved.");
                    break;
                }

                Console.WriteLine("\n--- Favorites ---");
                foreach (var fav in favorites)
                {
                    Console.WriteLine($"{fav.FileName}");
                }
                break;

            case "5":
                if (currentFiles.Count == 0)
                {
                    Console.WriteLine("No folder loaded.");
                    break;
                }
                Console.Write("Select the file number to mark as favorite: ");
                if (int.TryParse(Console.ReadLine(), out int favIndex) &&
                    favIndex > 0 && favIndex <= currentFiles.Count)
                {
                    favoritesService.CreateFavorite(currentFiles[favIndex - 1].FilePath);
                    Console.WriteLine("File added to favorites.");
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
                break;

            case "6":
                Console.Write("Enter the name of the file to remove from favorites: ");
                var fileToRemove = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(fileToRemove))
                {
                    favoritesService.DeleteFavorite($"{currentFolder}\\{fileToRemove}");
                    Console.WriteLine("Removed from favorites (if it existed).");
                }
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
