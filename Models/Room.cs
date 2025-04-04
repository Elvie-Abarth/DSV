namespace DSV_Book_a_room.Models
{
    public class Room
    {
        public int Id { get; set; }
        public Room(string name, int seating, List<EquipmentEnum> equipment)
        {
            Name = name;
            Seating = seating;
            Equipment = equipment;
        }

        public string Name { get; set; }
        public int Seating { get; set; }
        public List<EquipmentEnum> Equipment { get; set; }
    }
    public enum EquipmentEnum
    {
        Projektor,
        Whiteboard,
        Teams_intergration
    }


}
