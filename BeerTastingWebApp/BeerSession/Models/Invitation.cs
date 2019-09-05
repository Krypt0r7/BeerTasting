namespace BeerSession.Models
{
    public class Invitation
    {
        public int TastingID { get; set; }
        public Tasting Tasting { get; set; }
        public int UserID { get; set; } 
        public User User { get; set; }
    }
}