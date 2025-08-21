using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TMD.Challenge.Application.Models;

namespace TMD.Challenge.Application.Services
{
    public class FavoritesService
    {
        private readonly string _storageFile;

        public FavoritesService(string storageFile = "favorites.json")
        {
            _storageFile = storageFile;
        }

        public List<FavoriteFile> GetFavorites()
        {
            if (!File.Exists(_storageFile))
                return new List<FavoriteFile>();

            try
            {
                var json = File.ReadAllText(_storageFile);
                var favorites = JsonSerializer.Deserialize<List<FavoriteFile>>(json, new JsonSerializerOptions() { WriteIndented = true });
                return favorites ?? new List<FavoriteFile>();
            }
            catch
            {
                return new List<FavoriteFile>();
            }
        }

        public void CreateFavorite(string filePath)
        {
            var favorites = GetFavorites();

            if (favorites.Any(f => f.FilePath.Equals(filePath, StringComparison.OrdinalIgnoreCase)))
                return;

            favorites.Add(new FavoriteFile
            {
                FilePath = filePath,
                AddedAt = DateTime.Now
            });

            SaveFavorites(favorites);
        }

        public void DeleteFavorite(string filePath)
        {
            var favorites = GetFavorites();

            favorites.RemoveAll(f => f.FilePath.Equals(filePath, StringComparison.OrdinalIgnoreCase));

            SaveFavorites(favorites);
        }

        private void SaveFavorites(List<FavoriteFile> favorites)
        {
            var json = JsonSerializer.Serialize(favorites, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(_storageFile, json);
        }
    }
}
