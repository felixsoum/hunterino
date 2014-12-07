using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
	public enum Orientation { North, East, South, West }
	public Orientation orientation;
	public Triple cell;
}
