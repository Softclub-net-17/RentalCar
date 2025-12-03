namespace RentalCar.Domain.Entities
{
    public class Make
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        //navigations
        public List<Model> Models { get; set; } = [];
        public Image Image { get; set; } = null!;
    }
}
