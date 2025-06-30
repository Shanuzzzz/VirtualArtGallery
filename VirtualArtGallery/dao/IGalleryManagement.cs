using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.entity;

namespace VirtualArtGallery.dao
{
    public interface IGalleryManagement
    {
        bool AddGallery(Gallery gallery);
        bool UpdateGallery(Gallery gallery);
        bool RemoveGallery(int galleryId);
        Gallery GetGalleryById(int galleryId);
        List<Gallery> SearchGalleries(string keyword);
    }

}