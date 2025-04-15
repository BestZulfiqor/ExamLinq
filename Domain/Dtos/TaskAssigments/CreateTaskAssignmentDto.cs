namespace Domain.Dtos.TaskAssigments;

public class CreateTaskAssignmentDto
{
    public int TaskId { get; set; }
    public int UserId { get; set; }
    public DateTime AssignedDate { get; set; }
}
