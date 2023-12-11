namespace EfCoreVsDapper.Data.Models;

[Table("Users")]
public record class User
(
    [property: DatabaseGenerated(DatabaseGeneratedOption.Identity)] int Id,
    string Name,
    string Email
);
