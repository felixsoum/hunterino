using UnityEngine;
using System.Collections;

public class Enemies : MonoBehaviour
{
	public void Clear()
	{
		BroadcastMessage("Die", SendMessageOptions.DontRequireReceiver);
	}
}
