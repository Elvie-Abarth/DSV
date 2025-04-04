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

        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [BindProperty]
        public int FilterSeats { get; set; }

        [BindProperty]
        public EquipmentEnum FilterEquipment { get; set; }

        public List<Room> rooms = new List<Room>();

        public void OnGet()
        {
            if (TempData.ContainsKey("StartTime"))
            {
                StartTime = (DateTime)TempData["StartTime"];
            }
            if (TempData.ContainsKey("EndTime"))
            {
                EndTime = (DateTime)TempData["EndTime"];
            }
            if (TempData.ContainsKey("FilterEquipment"))
            {
                FilterEquipment = (EquipmentEnum)TempData["FilterEquipment"];
            }
            if (TempData.ContainsKey("FilterSeats"))
            {
                FilterSeats = (int)TempData["FilterSeats"];
            }

            if (!_context.Rooms.Any())
            {
                _context.Add(new Room("Room A", 20, new List<EquipmentEnum> { EquipmentEnum.Whiteboard, EquipmentEnum.Projektor }));
                _context.Add(new Room("Room B", 10, new List<EquipmentEnum> { EquipmentEnum.Teams_intergration, EquipmentEnum.Projektor }));

                _context.SaveChanges();
            }
            rooms = _context.Rooms.ToList();
        }

        public IActionResult OnPost()
        {
            TempData["StartTime"] = StartTime;
            TempData["EndTime"] = EndTime;
            TempData["FilterEquipment"] = FilterEquipment;
            TempData["FilterSeats"] = FilterSeats;
            return RedirectToPage();
        }
    }
}
