using UnityEngine;

public struct ReplayFrame
{
	public float frameTime;
	public Vector3 position;
	public Vector2 velocity;
	public Vector3 scale;
	public string action;

	public ReplayFrame (float delta, Vector3 pos, Vector2 vel, Vector3 locscale, string act)
	{
		frameTime = delta;
		position = pos;
		velocity = vel;
		scale = locscale;
		action = act;
	}
}
