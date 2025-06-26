public class CreateReadingRoomDto
{
    public string Name { get; set; } = "";
    public string Location { get; set; } = "";
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }
}
