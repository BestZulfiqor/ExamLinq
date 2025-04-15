namespace Domain.Entities;

public class TaskAssigment
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public int UserId { get; set; }
    public DateTime AssignedDate { get; set; }

    public Task Task { get; set; }
    public User User { get; set; }
}
