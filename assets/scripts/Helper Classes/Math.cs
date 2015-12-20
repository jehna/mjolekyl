using UnityEngine;
using System.Collections;

public static class Math {

	public static Vector3 Lerp(Vector3 from, Vector3 to, float percent) {
         return from + percent * ( to - from );
	}
}
