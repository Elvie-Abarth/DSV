using DSV_Book_a_room.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DSV_Book_a_room.Pages
{
    public class BookingsModel : PageModel
    {
        private readonly DsvDbContext _context;

        public BookingsModel(DsvDbContext context)
        {
            _context = context;
        }

        public List<BookingViewModel> bookings { get; set; }
       

        public void OnGet()
        {
            PopulateBookings();
        }
       
        private void PopulateBookings()
        {
            bookings = _context.Bookings
                .Join(_context.Rooms,
                      booking => booking.RoomId,
                      room => room.Id,
                      (booking, room) => new BookingViewModel
                      {
                          BookingId = booking.Id,
                          RoomId = booking.RoomId,
                          RoomName = room.Name,
                          StartTime = booking.StartTime,
                          EndTime = booking.EndTime,
                          Description = booking.Description
                      })
                .ToList();
        }

        public IActionResult OnPostDeleteBooking(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }
            PopulateBookings();
            return Page();
        }
    }
}
