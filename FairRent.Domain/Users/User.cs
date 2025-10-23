namespace FairRent.Domain.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string? DisplayName { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
