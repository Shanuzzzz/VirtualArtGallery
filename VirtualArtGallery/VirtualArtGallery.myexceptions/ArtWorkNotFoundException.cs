using System;

namespace VirtualArtGallery.myexceptions
{
    public class ArtWorkNotFoundException : Exception
    {
        public ArtWorkNotFoundException(string message) : base(message)
        {
        }
    }
}
