using NUnit.Framework;
using VirtualArtGallery.dao;
using VirtualArtGallery.entity;
using System;
using System.Collections.Generic;

namespace VirtualArtGallery.Tests
{
    [TestFixture]
    public class ArtworkTests
    {
        private VirtualArtGalleryImpl gallery;

        [SetUp]
        public void Setup()
        {
            gallery = new VirtualArtGalleryImpl();
        }

        [Test]
        public void Test_AddArtwork_ShouldSucceed_WhenArtistExists()
        {
            int artworkId = 103;
            gallery.RemoveArtwork(artworkId); 

            var newArtwork = new Artwork
            {
                ArtworkID = artworkId,
                Title = "Sunset Landscape",
                Description = "Beautiful sunset over hills",
                CreationDate = new DateTime(2024, 05, 01),
                Medium = "Oil",
                ImageURL = "https://images.in/sunset.jpg",
                ArtistID = 1 
            };

            var result = gallery.AddArtwork(newArtwork);
            Assert.IsTrue(result);

            gallery.RemoveArtwork(artworkId); 
        }
        [Test]
        public void Test_UpdateArtwork_ShouldChangeDetailsSuccessfully()
        {
            
            var updatedArtwork = new Artwork
            {
                ArtworkID = 102,
                Title = "Galloping Horses",
                Description = "Dynamic horse motion artwork",
                CreationDate = new DateTime(1976, 01, 01),
                Medium = "Ink and Acrylic",
                ImageURL = "https://images.in/horses_updated.jpg",
                ArtistID = 2 
            };

            var result = gallery.UpdateArtwork(updatedArtwork);
            Assert.IsTrue(result);

            var actual = gallery.GetArtworkById(102);
            Assert.AreEqual("Galloping Horses", actual.Title);
        }
        [Test]
        public void Test_RemoveArtwork_ShouldDeleteExistingArtwork()
        {
            int artworkId = 104;
            gallery.RemoveArtwork(artworkId);

            var art = new Artwork
            {
                ArtworkID = artworkId,
                Title = "To Be Removed",
                Description = "Temporary artwork",
                CreationDate = DateTime.Today,
                Medium = "Charcoal",
                ImageURL = "https://images.in/temp.jpg",
                ArtistID = 3
            };

            gallery.AddArtwork(art);

            var result = gallery.RemoveArtwork(artworkId);
            Assert.IsTrue(result);

            var deleted = gallery.GetArtworkById(artworkId);
            Assert.IsNull(deleted);
        }
        [Test]
        public void Test_SearchArtworks_ShouldReturnExpectedResults()
        {
            var results = gallery.SearchArtworks("Shakuntala");
            Assert.IsNotEmpty(results);
            Assert.IsTrue(results.Exists(a => a.ArtworkID == 101));
        }

    }
}