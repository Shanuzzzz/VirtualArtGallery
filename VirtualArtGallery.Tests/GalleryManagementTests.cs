using NUnit.Framework;
using VirtualArtGallery.dao;
using VirtualArtGallery.entity;
using System;
using System.Collections.Generic;

namespace VirtualArtGallery.Tests
{
    [TestFixture]
    public class GalleryTests
    {
        private VirtualArtGalleryImpl gallery;

        [SetUp]
        public void Setup()
        {
            gallery = new VirtualArtGalleryImpl();
        }

        [Test]
        public void Test_AddGallery_ShouldSucceed()
        {
            int galleryId = 202;

            
            gallery.RemoveGallery(galleryId);

            var newGallery = new Gallery
            {
                GalleryID = galleryId,
                Name = "Test Gallery",
                Description = "A temporary gallery for testing",
                Location = "Hyderabad",
                Curator = 1, 
                OpeningHours = "9:00 AM - 5:00 PM"
            };

            var result = gallery.AddGallery(newGallery);
            Assert.IsTrue(result, "Gallery should be added successfully.");

            var inserted = gallery.GetGalleryById(galleryId);
            Assert.IsNotNull(inserted, "Inserted gallery should not be null.");
            Assert.AreEqual("Test Gallery", inserted.Name);

            
            gallery.RemoveGallery(galleryId);
        }

        [Test]
        public void Test_UpdateGallery_ShouldChangeDetailsSuccessfully()
        {
            int galleryId = 203;

            
            gallery.RemoveGallery(galleryId);

            
            gallery.AddGallery(new Gallery
            {
                GalleryID = galleryId,
                Name = "Original Gallery",
                Description = "Before update",
                Location = "Pune",
                Curator = 2,
                OpeningHours = "10:00 AM - 6:00 PM"
            });

            
            var updatedGallery = new Gallery
            {
                GalleryID = galleryId,
                Name = "Updated Gallery",
                Description = "Updated description",
                Location = "Bangalore",
                Curator = 2,
                OpeningHours = "11:00 AM - 7:00 PM"
            };

            var result = gallery.UpdateGallery(updatedGallery);
            Assert.IsTrue(result, "Gallery should be updated.");

            var fetched = gallery.GetGalleryById(galleryId);
            Assert.AreEqual("Updated Gallery", fetched.Name);
            Assert.AreEqual("Bangalore", fetched.Location);

            
            gallery.RemoveGallery(galleryId);
        }

        [Test]
        public void Test_RemoveGallery_ShouldDeleteSuccessfully()
        {
            int galleryId = 204;

            gallery.RemoveGallery(galleryId); 

            var galleryToDelete = new Gallery
            {
                GalleryID = galleryId,
                Name = "To Be Deleted",
                Description = "This gallery is temporary",
                Location = "Chandigarh",
                Curator = 3,
                OpeningHours = "8:00 AM - 4:00 PM"
            };

            gallery.AddGallery(galleryToDelete);

            var result = gallery.RemoveGallery(galleryId);
            Assert.IsTrue(result, "Gallery should be removed.");

            var deleted = gallery.GetGalleryById(galleryId);
            Assert.IsNull(deleted, "Deleted gallery should return null.");
        }

        [Test]
        public void Test_SearchGalleries_ShouldReturnResults()
        {
            var results = gallery.SearchGalleries("Modern");
            Assert.IsTrue(results.Count > 0, "Should return at least one match.");
        }

    }
}
