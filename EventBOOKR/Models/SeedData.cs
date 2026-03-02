using EventBOOKR.Data;
using Microsoft.EntityFrameworkCore;

namespace EventBOOKR.Models;

public static class SeedData
{
    public static void Initialise(ApplicationDbContext context)
    {
        // Look for existing venues; if they exist the DB is already seeded
        if (context.Venues.Any())
        {
            return;
        }
        
        // 1. Seed venues
        var venues = new Venue[]
        {
            new Venue { Name = "CTICC Main Hall", Location = "Convention Square, 1 Lower Long Street", Capacity = 10000 },
            new Venue { Name = "Kirstenbosch Summer Concert Stage", Location = "Rhodes Dr, Newlands", Capacity = 5000 },
            new Venue { Name = "The Old Biscuit Mill", Location = "373 Albert Rd, Woodstock", Capacity = 800 },
            new Venue { Name = "Sea Point Civic Centre", Location = "Cnr. Main Rd and Bowlers Ave, Sea Point", Capacity = 300 },
            new Venue { Name = "Radisson Hotel Waterfont", Location = "100 Beach Rd, Mouille Point", Capacity = 300 },
            new Venue { Name = "Mount Nelson Hotel", Location = "76 Orange St, Gardens", Capacity = 400 },
            new Venue { Name = "Zeitz MOCAA Ocular Lounge", Location = "S Arm Rd, Victoria & Alfred Waterfront", Capacity = 200 },
            new Venue { Name = "Rooftop on Bree", Location = "170 Bree St, Cape Town City Centre", Capacity = 100 },
            new Venue { Name = "The Lookout", Location = "2 Sachs St, Schotsche Kloof", Capacity = 800 },
            new Venue { Name = "Grand Africa Cafe & Beach", Location = "1 Haul Rd, Victoria & Alfred Waterfront", Capacity = 1050 },
            new Venue { Name = "NASDAK", Location = "40 Heerengracht St, Cape Town City Centre", Capacity = 250 },
            new Venue { Name = "Gigi Rooftop", Location = "118 St Georges Mall, Cape Town City Centre", Capacity = 100 },
            new Venue { Name = "The Argyle", Location = "1 Argyle St, Woodstock", Capacity = 200 }
        };
        context.Venues.AddRange(venues);
        context.SaveChanges();
        
        // 2. Seed Events
        var events = new Event[]
        {
            new Event { Name = "Global Tech Summit 2026", Description = "Annual technology and software development conference." },
            new Event { Name = "Sunset Botanical Concert", Description = "Live acoustic music in the gardens." },
            new Event { Name = "Weekend Farmers Market", Description = "Local food, crafts, and live entertainment." },
            new Event { Name = "Sea Point Artisan Market", Description = "A vibrant community gathering featuring local crafts, food, and family activities." },
            new Event { Name = "Oceanfront Corporate Gala", Description = "Exclusive black-tie networking evening for maritime and tech executives." },
            new Event { Name = "Pink Lady Spring High Tea", Description = "A luxurious afternoon of classical music, fine teas, and pastries." },
            new Event { Name = "Contemporary African Art Auction", Description = "An avant-garde evening celebrating modern art, featuring live auctions and champagne." },
            new Event { Name = "Mother City Startup Pitch Night", Description = "Cape Town's brightest tech founders pitch their ideas against a city skyline backdrop." },
            new Event { Name = "V&A Waterfront Fashion Week", Description = "Showcasing the upcoming summer collections from top South African designers." },
            new Event { Name = "Grand Summer Sundowners", Description = "An exclusive beach club party featuring international deep house DJs." },
            new Event { Name = "City Bowl Sunset Sessions", Description = "Trendy inner-city networking event with a panoramic view of Table Mountain." },
            new Event { Name = "Boutique Mixology Masterclass", Description = "An intimate evening learning the art of craft cocktails and curated tapas." },
            new Event { Name = "Industrial Chic Wedding Expo", Description = "A showcase of premium wedding vendors in a modern-industrial aesthetic." }
        };
        context.Events.AddRange(events);
        context.SaveChanges();
    }
}