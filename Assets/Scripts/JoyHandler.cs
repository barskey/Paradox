using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyHandler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {
	
	private Image joystickContainer;
	private Image joystick;

	public Vector2 inputDirection;

	void Start () {

		joystickContainer = GetComponent<Image> (); // get the image component of this gameboject
		joystick = transform.GetChild (0).GetComponent<Image> (); // this command is used because there is only one child in hierarchy
		inputDirection = Vector3.zero;
	}

	public void OnDrag (PointerEventData ped) {
		Vector2 position = Vector2.zero;

		//To get InputDirection
		RectTransformUtility.ScreenPointToLocalPointInRectangle
		(joystickContainer.rectTransform, 
			ped.position,
			ped.pressEventCamera,
			out position);

		position.x = (position.x / joystickContainer.rectTransform.sizeDelta.x);
		position.y = (position.y / joystickContainer.rectTransform.sizeDelta.y);

		float x = (joystickContainer.rectTransform.pivot.x == 1f) ? position.x * 2 + 1 : position.x * 2 - 1;
		float y = (joystickContainer.rectTransform.pivot.y == 1f) ? position.y * 2 + 1 : position.y * 2 - 1;

		inputDirection = new Vector2 (x, y);
		inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

        // to define the area in which joystick can move around
		joystick.rectTransform.anchoredPosition = new Vector3 (
            inputDirection.x * (joystickContainer.rectTransform.sizeDelta.x / 3),
            inputDirection.y * (joystickContainer.rectTransform.sizeDelta.y / 3)
        );

	}

	public void OnPointerDown (PointerEventData ped) {

		OnDrag (ped);
	}

	public void OnPointerUp( PointerEventData ped) {

		inputDirection = Vector3.zero;
		joystick.rectTransform.anchoredPosition = Vector3.zero;
	}
}