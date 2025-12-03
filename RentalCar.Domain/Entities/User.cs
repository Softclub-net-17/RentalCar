using RentalCar.Domain.Enums;

namespace RentalCar.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public Role Role { get; set; }
        public string PasswordHash { get; set; } = null!;
        
        // navigation
        public List<Reservation> Reservations { get; set; } = new ();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public IEnumerable<Favorite> Favorites { get; set; } = new List<Favorite>();    
    }
}
