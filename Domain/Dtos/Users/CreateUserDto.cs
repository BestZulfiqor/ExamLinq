namespace Domain.Dtos.Users;

public class CreateUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }

}
