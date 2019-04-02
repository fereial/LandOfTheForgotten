using UnityEngine;

public static class ExtencionsMethos 
{

	public static Vector3 V2ToXZ(this Vector2 v2 )
	{
		return new Vector3(v2.x, 0 , v2.y );
	}

	private static Vector2 FindPointInArea(float InnerRadius, float OuterRadius )
	{
		Vector2 V2Ref = UnityEngine.Random.insideUnitCircle;
		V2Ref = V2Ref.normalized * InnerRadius + V2Ref*(OuterRadius - InnerRadius );
		return V2Ref; 
	}
}
