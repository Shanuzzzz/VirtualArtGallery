using System;
using System.Collections.Generic;
using VirtualArtGallery.dao;
using VirtualArtGallery.entity;
using VirtualArtGallery.myexceptions;

namespace VirtualArtGallery.main
{
    class MainModule
    {
        static void Main(string[] args)
        {
            IVirtualArtGallery galleryService = new VirtualArtGalleryImpl();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n Virtual Art Gallery - Menu");

                Console.WriteLine("1. Add Artwork");
                Console.WriteLine("2. Update Artwork");
                Console.WriteLine("3. Remove Artwork");
                Console.WriteLine("4. Get Artwork by ID");
                Console.WriteLine("5. Search Artworks by Keyword");
                Console.WriteLine("6. Add Artwork to Favorites");
                Console.WriteLine("7. Remove Artwork from Favorites");
                Console.WriteLine("8. Get User Favorite Artworks");
                Console.WriteLine("9. Exit");
                Console.Write("Choose option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Artwork art = new Artwork();
                            Console.Write("Enter Artwork ID: ");
                            art.ArtworkID = int.Parse(Console.ReadLine());
                            Console.Write("Enter Title: ");
                            art.Title = Console.ReadLine();
                            Console.Write("Enter Description: ");
                            art.Description = Console.ReadLine();
                            Console.Write("Enter Creation Date (yyyy-mm-dd): ");
                            art.CreationDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter Medium: ");
                            art.Medium = Console.ReadLine();
                            Console.Write("Enter Image URL: ");
                            art.ImageURL = Console.ReadLine();
                            Console.Write("Enter Artist ID: ");
                            art.ArtistID = int.Parse(Console.ReadLine());

                            bool added = galleryService.AddArtwork(art);
                            Console.WriteLine(added ? "Artwork Added!" : "Failed to Add Artwork");
                            break;

                        case 2:
                            Console.Write("Enter Artwork ID to update: ");
                            int updateId = int.Parse(Console.ReadLine());
                            Artwork update = new Artwork();
                            update.ArtworkID = updateId;
                            Console.Write("Enter New Title: ");
                            update.Title = Console.ReadLine();
                            Console.Write("Enter New Description: ");
                            update.Description = Console.ReadLine();
                            Console.Write("Enter New Creation Date (yyyy-mm-dd): ");
                            update.CreationDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter New Medium: ");
                            update.Medium = Console.ReadLine();
                            Console.Write("Enter New Image URL: ");
                            update.ImageURL = Console.ReadLine();
                            Console.Write("Enter New Artist ID: ");
                            update.ArtistID = int.Parse(Console.ReadLine());

                            bool updated = galleryService.UpdateArtwork(update);
                            Console.WriteLine(updated ? "Artwork Updated!" : "Update Failed");
                            break;

                        case 3:
                            Console.Write("Enter Artwork ID to remove: ");
                            int removeId = int.Parse(Console.ReadLine());
                            bool removed = galleryService.RemoveArtwork(removeId);
                            Console.WriteLine(removed ? "Artwork Removed!" : "Remove Failed");
                            break;

                        case 4:
                            Console.Write("Enter Artwork ID to fetch: ");
                            int fetchId = int.Parse(Console.ReadLine());
                            Artwork fetched = galleryService.GetArtworkById(fetchId);
                            if (fetched == null)
                                throw new ArtWorkNotFoundException("Artwork not found with ID: " + fetchId);
                            Console.WriteLine($" {fetched.ArtworkID}: {fetched.Title} - {fetched.Description}");
                            break;

                        case 5:
                            Console.Write("Enter keyword to search: ");
                            string keyword = Console.ReadLine();
                            List<Artwork> results = galleryService.SearchArtworks(keyword);
                            Console.WriteLine("Search Results:");
                            results.ForEach(a =>
                                Console.WriteLine($"- {a.Title}: {a.Description}")
                            );
                            break;

                        case 6:
                            Console.Write("Enter User ID: ");
                            int uid1 = int.Parse(Console.ReadLine());
                            Console.Write("Enter Artwork ID: ");
                            int aid1 = int.Parse(Console.ReadLine());
                            bool favAdded = galleryService.AddArtworkToFavorite(uid1, aid1);
                            Console.WriteLine(favAdded ? " Added to Favorites" : " Failed to Add to Favorites");
                            break;

                        case 7:
                            Console.Write("Enter User ID: ");
                            int uid2 = int.Parse(Console.ReadLine());
                            Console.Write("Enter Artwork ID: ");
                            int aid2 = int.Parse(Console.ReadLine());
                            bool favRemoved = galleryService.RemoveArtworkFromFavorite(uid2, aid2);
                            Console.WriteLine(favRemoved ? " Removed from Favorites" : " Failed to Remove from Favorites");
                            break;

                        case 8:
                            Console.Write("Enter User ID: ");
                            int uid3 = int.Parse(Console.ReadLine());
                            List<Artwork> favorites = galleryService.GetUserFavoriteArtworks(uid3);
                            if (favorites.Count == 0)
                                throw new UserNotFoundException("No favorites or user not found.");
                            Console.WriteLine("Favorite Artworks:");
                            favorites.ForEach(a =>
                                Console.WriteLine($"- {a.Title} by Artist {a.ArtistID}")
                            );
                            break;

                        case 9:
                            running = false;
                            Console.WriteLine(" Exiting Virtual Art Gallery. Goodbye!");
                            break;

                        default:
                            Console.WriteLine(" Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (ArtWorkNotFoundException ex)
                {
                    Console.WriteLine("Error " + ex.Message);
                }
                catch (UserNotFoundException ex)
                {
                    Console.WriteLine("Error " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine(" Please enter valid input format.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" General Error: " + ex.Message);
                }
            }
        }
    }
}
