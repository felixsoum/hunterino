public class DoorTemplate
{
	public Door Origin { get; set; }
	public int Index { get; set; }
	public Triple Cell { get; set; }

	public DoorTemplate(Door door, int index)
	{
		Origin = door;
		Index = index;
		Cell = door.cell;
	}
}
