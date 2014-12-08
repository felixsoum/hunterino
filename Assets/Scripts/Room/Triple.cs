public struct Triple
{
	public int X { get; set; }
	public int Y { get; set; }
	public int Z { get; set; }

	public Triple(int n) : this(n, n, n) {}

	public Triple(int x, int y, int z)
	{
		X = x;
		Y = y;
		Z = z;
	}

	public static Triple operator +(Triple t1, Triple t2)
	{
		return new Triple(t1.X + t2.X, t1.Y + t2.Y, t1.Z + t2.Z);
	}

	public static Triple operator -(Triple t1, Triple t2)
	{
		return new Triple(t1.X - t2.X, t1.Y - t2.Y, t1.Z - t2.Z);
	}

	public override string ToString ()
	{
		return "X: " + X.ToString() + ", Y: " + Y.ToString() + ", Z: " + Z.ToString();
	}
}
