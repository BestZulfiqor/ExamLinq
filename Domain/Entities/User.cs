namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }

    public TaskAssigment TaskAssigment { get; set; }
    public List<Domain.Entities.Task> Tasks { get; set; }
}
