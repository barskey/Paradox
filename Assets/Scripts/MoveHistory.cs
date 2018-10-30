using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHistory : MonoBehaviour {

	public float history = 5f;

	public struct MoveFrame {
		public float deltaTime; // Time.deltaTime
		public Transform trans; // position and scale
		public Vector2 vel; // velocity
		public string action; // what happened
		public bool end; // for tracking end of replay

		public MoveFrame (float t, Transform tr, Vector2 v, string a)
		{
			deltaTime = t;
			trans = tr;
			vel = v;
			action = a;
			end = true;
		}

		public MoveFrame (float t, Transform tr, Vector2 v, string a, bool e)
		{
			deltaTime = t;
			trans = tr;
			vel = v;
			action = a;
			end = e;
		}
	}

	private List<MoveFrame> frames;
	private float t;

	// Use this for initialization
	void Start () {
		frames = new List<MoveFrame> ();
	}
	
	public void AddMove (float time, Transform trans, Vector2 velocity, string action)
	{
		// set end marker of last item to false if more than one item in List
		int i = frames.Count - 1;
		if (i > 0) {
			MoveFrame temp = frames [i];
			frames [i] = new MoveFrame (temp.deltaTime, temp.trans, temp.vel, temp.action, false); 
			t = t + frames [frames.Count - 1].deltaTime;
		}

		frames.Add (new MoveFrame (time, trans, velocity, action)); // add new item

		// only keep enough frames to replay total time in history
		if (HistoryFull ())
			frames.RemoveAt (0);

		Debug.Log (string.Format ("Items:{0} Time:{1}", frames.Count, t));
	}

	public Vector3 GetStartPos ()
	{
		return frames [0].trans.position;
	}

	public MoveFrame GetFrame (int i)
	{
		MoveFrame ret = i < frames.Count ? frames [i] : frames [frames.Count - 1];

		return ret;
	}

	private bool HistoryFull ()
	{
		float t = 0;
		foreach (MoveFrame frame in frames)
		{
			t += frame.deltaTime;
		}

		return (t > history ? true : false);
	}
}
