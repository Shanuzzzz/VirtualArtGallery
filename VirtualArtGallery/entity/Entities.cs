using System;

namespace VirtualArtGallery.entity
{
    public class Artist
    {
        private int artistID;
        private string name;
        private string biography;
        private DateTime birthDate;
        private string nationality;
        private string website;
        private string contactInformation;

        public Artist() { }

        public Artist(int artistID, string name, string biography, DateTime birthDate, string nationality, string website, string contactInformation)
        {
            this.artistID = artistID;
            this.name = name;
            this.biography = biography;
            this.birthDate = birthDate;
            this.nationality = nationality;
            this.website = website;
            this.contactInformation = contactInformation;
        }

        public int ArtistID { get => artistID; set => artistID = value; }
        public string Name { get => name; set => name = value; }
        public string Biography { get => biography; set => biography = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string Nationality { get => nationality; set => nationality = value; }
        public string Website { get => website; set => website = value; }
        public string ContactInformation { get => contactInformation; set => contactInformation = value; }
    }

    public class Artwork
    {
        private int artworkID;
        private string title;
        private string description;
        private DateTime creationDate;
        private string medium;
        private string imageURL;
        private int artistID;

        public Artwork() { }

        public Artwork(int artworkID, string title, string description, DateTime creationDate, string medium, string imageURL, int artistID)
        {
            this.artworkID = artworkID;
            this.title = title;
            this.description = description;
            this.creationDate = creationDate;
            this.medium = medium;
            this.imageURL = imageURL;
            this.artistID = artistID;
        }

        public int ArtworkID { get => artworkID; set => artworkID = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public string Medium { get => medium; set => medium = value; }
        public string ImageURL { get => imageURL; set => imageURL = value; }
        public int ArtistID { get => artistID; set => artistID = value; }
    }

    public class User
    {
        private int userID;
        private string username;
        private string password;
        private string email;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private string profilePicture;

        public User() { }

        public User(int userID, string username, string password, string email, string firstName, string lastName, DateTime dateOfBirth, string profilePicture)
        {
            this.userID = userID;
            this.username = username;
            this.password = password;
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.profilePicture = profilePicture;
        }

        public int UserID { get => userID; set => userID = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string ProfilePicture { get => profilePicture; set => profilePicture = value; }
    }

    public class Gallery
    {
        private int galleryID;
        private string name;
        private string description;
        private string location;
        private int curator;
        private string openingHours;

        public Gallery() { }

        public Gallery(int galleryID, string name, string description, string location, int curator, string openingHours)
        {
            this.galleryID = galleryID;
            this.name = name;
            this.description = description;
            this.location = location;
            this.curator = curator;
            this.openingHours = openingHours;
        }

        public int GalleryID { get => galleryID; set => galleryID = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Location { get => location; set => location = value; }
        public int Curator { get => curator; set => curator = value; }
        public string OpeningHours { get => openingHours; set => openingHours = value; }
    }

    public class UserFavoriteArtwork
    {
        private int userID;
        private int artworkID;

        public UserFavoriteArtwork() { }

        public UserFavoriteArtwork(int userID, int artworkID)
        {
            this.userID = userID;
            this.artworkID = artworkID;
        }

        public int UserID { get => userID; set => userID = value; }
        public int ArtworkID { get => artworkID; set => artworkID = value; }
    }
}
