using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VirtualArtGallery.entity;
using VirtualArtGallery.util;

namespace VirtualArtGallery.dao
{
    public class VirtualArtGalleryImpl : IVirtualArtGallery, IGalleryManagement
    {
        private readonly SqlConnection connection;

        public VirtualArtGalleryImpl()
        {
            connection = DBConnection.GetConnection();
        }

        // Add Artwork
        public bool AddArtwork(Artwork artwork)
        {
            connection.Open();
            string query = "INSERT INTO Artwork (ArtworkID, Title, Description, CreationDate, Medium, ImageURL, ArtistID) " +
                           "VALUES (@ArtworkID, @Title, @Description, @CreationDate, @Medium, @ImageURL, @ArtistID)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
            cmd.Parameters.AddWithValue("@Title", artwork.Title);
            cmd.Parameters.AddWithValue("@Description", artwork.Description);
            cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
            cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
            cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
            cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);
            bool result = cmd.ExecuteNonQuery() > 0;
            connection.Close();
            return result;
        }

        // Update Artwork
        public bool UpdateArtwork(Artwork artwork)
        {
            connection.Open();
            string query = "UPDATE Artwork SET Title=@Title, Description=@Description, CreationDate=@CreationDate, Medium=@Medium, ImageURL=@ImageURL, ArtistID=@ArtistID WHERE ArtworkID=@ArtworkID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
            cmd.Parameters.AddWithValue("@Title", artwork.Title);
            cmd.Parameters.AddWithValue("@Description", artwork.Description);
            cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
            cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
            cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
            cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);
            bool result = cmd.ExecuteNonQuery() > 0;
            connection.Close();
            return result;
        }

        // Remove Artwork
        public bool RemoveArtwork(int artworkID)
        {
            connection.Open();
            string query = "DELETE FROM Artwork WHERE ArtworkID=@ArtworkID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ArtworkID", artworkID);
            bool result = cmd.ExecuteNonQuery() > 0;
            connection.Close();
            return result;
        }

        // Get Artwork by ID
        public Artwork GetArtworkById(int artworkID)
        {
            connection.Open();
            string query = "SELECT * FROM Artwork WHERE ArtworkID=@ArtworkID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ArtworkID", artworkID);
            SqlDataReader reader = cmd.ExecuteReader();
            Artwork artwork = null;
            if (reader.Read())
            {
                artwork = new Artwork
                {
                    ArtworkID = (int)reader["ArtworkID"],
                    Title = reader["Title"].ToString(),
                    Description = reader["Description"].ToString(),
                    CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                    Medium = reader["Medium"].ToString(),
                    ImageURL = reader["ImageURL"].ToString(),
                    ArtistID = (int)reader["ArtistID"]
                };
            }
            connection.Close();
            return artwork;
        }

        // Search Artworks
        public List<Artwork> SearchArtworks(string keyword)
        {
            connection.Open();
            string query = "SELECT * FROM Artwork WHERE Title LIKE @Keyword OR Description LIKE @Keyword";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
            SqlDataReader reader = cmd.ExecuteReader();
            List<Artwork> artworks = new List<Artwork>();
            while (reader.Read())
            {
                artworks.Add(new Artwork
                {
                    ArtworkID = (int)reader["ArtworkID"],
                    Title = reader["Title"].ToString(),
                    Description = reader["Description"].ToString(),
                    CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                    Medium = reader["Medium"].ToString(),
                    ImageURL = reader["ImageURL"].ToString(),
                    ArtistID = (int)reader["ArtistID"]
                });
            }
            connection.Close();
            return artworks;
        }

        // Add to Favorites
       
        public bool AddArtworkToFavorite(int userId, int artworkId)
        {
            connection.Open();

            
            string checkQuery = "SELECT COUNT(*) FROM User_Favorite_Artwork WHERE UserID = @UserID AND ArtworkID = @ArtworkID";
            SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
            checkCmd.Parameters.AddWithValue("@UserID", userId);
            checkCmd.Parameters.AddWithValue("@ArtworkID", artworkId);
            int exists = (int)checkCmd.ExecuteScalar();

            if (exists > 0)
            {
                Console.WriteLine("Artwork already exists in favorites.");
                connection.Close();
                return false;
            }

            
            string insertQuery = "INSERT INTO User_Favorite_Artwork (UserID, ArtworkID) VALUES (@UserID, @ArtworkID)";
            SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
            insertCmd.Parameters.AddWithValue("@UserID", userId);
            insertCmd.Parameters.AddWithValue("@ArtworkID", artworkId);
            bool result = insertCmd.ExecuteNonQuery() > 0;

            connection.Close();
            return result;
        }
        

        // Remove from Favorites
        public bool RemoveArtworkFromFavorite(int userId, int artworkId)
        {
            connection.Open();
            string query = "DELETE FROM User_Favorite_Artwork WHERE UserID=@UserID AND ArtworkID=@ArtworkID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.AddWithValue("@ArtworkID", artworkId);
            bool result = cmd.ExecuteNonQuery() > 0;
            connection.Close();
            return result;
        }

        // Get User Favorite Artworks
       
        public List<Artwork> GetUserFavoriteArtworks(int userId)
        {
            connection.Open();
            string query = @"SELECT A.* 
                     FROM Artwork A 
                     JOIN User_Favorite_Artwork UFA ON A.ArtworkID = UFA.ArtworkID 
                     WHERE UFA.UserID = @UserID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserID", userId);
            SqlDataReader reader = cmd.ExecuteReader();

            List<Artwork> favorites = new List<Artwork>();
            while (reader.Read())
            {
                favorites.Add(new Artwork
                {
                    ArtworkID = (int)reader["ArtworkID"],
                    Title = reader["Title"].ToString(),
                    Description = reader["Description"].ToString(),
                    CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                    Medium = reader["Medium"].ToString(),
                    ImageURL = reader["ImageURL"].ToString(),
                    ArtistID = (int)reader["ArtistID"]
                });
            }

           
            reader.Close();
            connection.Close();
            return favorites;
        }

        // Add Gallery
        public bool AddGallery(Gallery gallery)
        {
            connection.Open();
            string query = "INSERT INTO Gallery (GalleryID, Name, Description, Location, Curator, OpeningHours) " +
                           "VALUES (@GalleryID, @Name, @Description, @Location, @Curator, @OpeningHours)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@GalleryID", gallery.GalleryID);
            cmd.Parameters.AddWithValue("@Name", gallery.Name);
            cmd.Parameters.AddWithValue("@Description", gallery.Description);
            cmd.Parameters.AddWithValue("@Location", gallery.Location);
            cmd.Parameters.AddWithValue("@Curator", gallery.Curator);
            cmd.Parameters.AddWithValue("@OpeningHours", gallery.OpeningHours);
            bool result = cmd.ExecuteNonQuery() > 0;
            connection.Close();
            return result;
        }

        // Update Gallery
        public bool UpdateGallery(Gallery gallery)
        {
            connection.Open();
            string query = "UPDATE Gallery SET Name=@Name, Description=@Description, Location=@Location, Curator=@Curator, OpeningHours=@OpeningHours WHERE GalleryID=@GalleryID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@GalleryID", gallery.GalleryID);
            cmd.Parameters.AddWithValue("@Name", gallery.Name);
            cmd.Parameters.AddWithValue("@Description", gallery.Description);
            cmd.Parameters.AddWithValue("@Location", gallery.Location);
            cmd.Parameters.AddWithValue("@Curator", gallery.Curator);
            cmd.Parameters.AddWithValue("@OpeningHours", gallery.OpeningHours);
            bool result = cmd.ExecuteNonQuery() > 0;
            connection.Close();
            return result;
        }

        // Remove Gallery
        public bool RemoveGallery(int galleryId)
        {
            connection.Open();
            string query = "DELETE FROM Gallery WHERE GalleryID=@GalleryID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@GalleryID", galleryId);
            bool result = cmd.ExecuteNonQuery() > 0;
            connection.Close();
            return result;
        }

        // Get Gallery by ID
        public Gallery GetGalleryById(int galleryId)
        {
            connection.Open();
            string query = "SELECT * FROM Gallery WHERE GalleryID=@GalleryID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@GalleryID", galleryId);
            SqlDataReader reader = cmd.ExecuteReader();
            Gallery gallery = null;
            if (reader.Read())
            {
                gallery = new Gallery
                {
                    GalleryID = (int)reader["GalleryID"],
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    Location = reader["Location"].ToString(),
                    Curator = (int)reader["Curator"],
                    OpeningHours = reader["OpeningHours"].ToString()
                };
            }
            connection.Close();
            return gallery;
        }

        // Search Galleries
        public List<Gallery> SearchGalleries(string keyword)
        {
            connection.Open();
            string query = "SELECT * FROM Gallery WHERE Name LIKE @Keyword OR Description LIKE @Keyword OR Location LIKE @Keyword";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
            SqlDataReader reader = cmd.ExecuteReader();
            List<Gallery> galleries = new List<Gallery>();
            while (reader.Read())
            {
                galleries.Add(new Gallery
                {
                    GalleryID = (int)reader["GalleryID"],
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    Location = reader["Location"].ToString(),
                    Curator = (int)reader["Curator"],
                    OpeningHours = reader["OpeningHours"].ToString()
                });
            }
            connection.Close();
            return galleries;
        }

    }
}





