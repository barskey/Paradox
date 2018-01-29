using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
	public float moveSpeed = 05f;

	private JoyHandler joystickMovement;
	private Vector3 direction;
	private float xMin,xMax,yMin,yMax;

	void OnEnable ()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode)
	{
		Debug.Log (string.Format ("Scene {0} loaded.", scene.name));
		if (scene.name == "HUD") {
			GameObject[] rootObj = scene.GetRootGameObjects ();
			foreach (GameObject go in rootObj)
			{
				if (go.transform.Find ("JoystickContainer") != null)
					joystickMovement = go.transform.Find ("JoystickContainer").GetComponent <JoyHandler> ();
			}
		}
	}

	void Update ()
    {
		if (joystickMovement != null)
			direction = joystickMovement.inputDirection;

        if (direction.magnitude != 0) {
			transform.position += direction * moveSpeed;
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, xMin, xMax), Mathf.Clamp (transform.position.y, yMin, yMax), 0f); //to restric movement of player
		}    
	}

	void Start ()
	{
		//Initialization of boundaries
		xMax = Screen.width;
		xMin = -Screen.width; 
		yMax = Screen.height;
		yMin = -Screen.height;
	}
}