using EventBOOKR.Models;
using Microsoft.EntityFrameworkCore;

namespace EventBOOKR.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            // If there are any venues, the database has been seeded
            if (context.Venues.Any())
            {
                return;
            }

            // 1. Create Venues
            var vaWaterfront = new Venue { Name = "V&A Waterfront", Location = "19 Dock Rd, Cape Town", Capacity = 5000 };
            var gigiRooftop = new Venue { Name = "Gigi Rooftop", Location = "118 St Georges Mall, Cape Town", Capacity = 150 };
            var theArgyle = new Venue { Name = "The Argyle", Location = "Woodstock, Cape Town", Capacity = 200 };

            context.Venues.AddRange(vaWaterfront, gigiRooftop, theArgyle);
            context.SaveChanges(); // Save to generate IDs

            // 2. Create Events
            var fashionWeek = new Event 
            { 
                Name = "V&A Fashion Week", 
                Description = "A week-long showcase of Africa's top designers.",
                IsFlexibleSchedule = true // Users must pick a 30-min slot during the active days
            };

            var highTea = new Event 
            { 
                Name = "High Tea", 
                Description = "An elegant afternoon of artisan teas and pastries.",
                IsFlexibleSchedule = false // Fixed time, no slot selection needed
            };

            var quizNight = new Event 
            { 
                Name = "Weekly Quiz Night", 
                Description = "Test your trivia knowledge and win bar tabs!",
                IsFlexibleSchedule = false 
            };

            context.Events.AddRange(fashionWeek, highTea, quizNight);
            context.SaveChanges();

            // 3. Create Event Schedules
            context.EventSchedules.AddRange(
                
                new EventSchedule 
                { 
                    EventId = fashionWeek.EventId, 
                    VenueId = vaWaterfront.VenueId, 
                    StartTime = new DateTime(2026, 3, 2, 8, 0, 0), // March 2nd, 08:00
                    EndTime = new DateTime(2026, 3, 9, 20, 0, 0)   // March 9th, 20:00
                },
                
                new EventSchedule 
                { 
                    EventId = highTea.EventId, 
                    VenueId = theArgyle.VenueId, 
                    StartTime = new DateTime(2026, 3, 7, 11, 0, 0), // Saturday, March 7th @ 11:00
                    EndTime = new DateTime(2026, 3, 7, 14, 0, 0)
                },
                
                new EventSchedule 
                { 
                    EventId = highTea.EventId, 
                    VenueId = gigiRooftop.VenueId, 
                    StartTime = new DateTime(2026, 3, 14, 11, 0, 0), // Saturday, March 14th @ 11:00
                    EndTime = new DateTime(2026, 3, 14, 14, 0, 0)
                },
                
                new EventSchedule 
                { 
                    EventId = quizNight.EventId, 
                    VenueId = theArgyle.VenueId, 
                    StartTime = new DateTime(2026, 3, 10, 19, 0, 0), // Tuesday, March 10th @ 19:00
                    EndTime = new DateTime(2026, 3, 10, 22, 0, 0)
                },
                
                new EventSchedule 
                { 
                    EventId = quizNight.EventId, 
                    VenueId = gigiRooftop.VenueId, 
                    StartTime = new DateTime(2026, 3, 11, 19, 0, 0), // Wednesday, March 11th @ 19:00
                    EndTime = new DateTime(2026, 3, 11, 22, 0, 0)
                }
            );

            context.SaveChanges();
        }
    }
}