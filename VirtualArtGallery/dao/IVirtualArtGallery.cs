using System;
using System.Collections.Generic;
using VirtualArtGallery.entity;

namespace VirtualArtGallery.dao
{
    public interface IVirtualArtGallery
    {
        
        bool AddArtwork(Artwork artwork);
        bool UpdateArtwork(Artwork artwork);
        bool RemoveArtwork(int artworkID);
        Artwork GetArtworkById(int artworkID);
        List<Artwork> SearchArtworks(string keyword);

        bool AddArtworkToFavorite(int userID, int artworkID);
        bool RemoveArtworkFromFavorite(int userID, int artworkID);
        List<Artwork> GetUserFavoriteArtworks(int userID);
    }
}
