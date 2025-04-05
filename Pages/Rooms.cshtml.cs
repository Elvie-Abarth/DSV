using DSV_Book_a_room.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace DSV_Book_a_room.Pages.Rooms
{
    public class RoomsModel : PageModel
    {
        private readonly DsvDbContext _context;

        public RoomsModel(DsvDbContext context)
        {
            _context = context;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
        }

        [BindProperty, DisplayFormat(DataFormatString = "{0:dd-MM-yyyyTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [BindProperty, DisplayFormat(DataFormatString = "{0:dd-MM-yyyyTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [BindProperty]
        public int FilterSeats { get; set; }

        [BindProperty]
        public EquipmentEnum FilterEquipment { get; set; }

        [BindProperty]
        public string BookingDescription { get; set; }

        public List<Room> rooms = new List<Room>();
        public List<Booking> bookings = new List<Booking>();

        [BindProperty]
        public bool ShowPopup { get; set; }

        public void OnGet()
        {
            if (!_context.Rooms.Any())
            {
                _context.Rooms.Add(new Room("Room A", 20, new List<EquipmentEnum> { EquipmentEnum.Whiteboard, EquipmentEnum.Projektor }));
                _context.Rooms.Add(new Room("Room B", 10, new List<EquipmentEnum> { EquipmentEnum.Teams_intergration, EquipmentEnum.Projektor }));

                _context.SaveChanges();
            }
            PopulateRooms();
        }

        public IActionResult OnPostFilterRooms()
        {
            PopulateRooms();
            return Page();
        }

        public IActionResult OnPostBookRooms(int Id)
        {
            var room = _context.Rooms.Find(Id);
            if (room != null && BookingDescription != null)
            {
                _context.Bookings.Add(new Booking(StartTime, EndTime, room.Id, BookingDescription));
                _context.SaveChanges();
                ShowPopup = true;
            }

            PopulateRooms();
            return Page();
        }

        private void PopulateRooms()
        {
            var query = _context.Rooms.AsQueryable();

            // Filter and only keep the rooms with more a same amount of seats
            query = query.Where(room => room.Seating >= FilterSeats);
            // Filter and only keep the rooms that contain the equipment that is filtered for.
            query = query.Where(room => room.Equipment.Contains(FilterEquipment));

            // put our query into the rooms list for the webpage to view.
            rooms = query.ToList();

            bookings = _context.Bookings.ToList();
        }
    }
}
