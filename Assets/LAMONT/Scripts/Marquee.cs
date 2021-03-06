using UnityEngine;

public class Marquee : MonoBehaviour
{
	public string message = "Where we're going, we don't need roads.";
	public float scrollSpeed = 50;

	Rect messageRect;

	void OnGUI ()
	{
		// Set up message's rect if we haven't already.
		if (messageRect.height == 0) {
			var dimensions = GUI.skin.label.CalcSize(new GUIContent(message));

			// Start message past the left side of the screen.
			messageRect.x = Screen.width/2;
			messageRect.width = dimensions.x;
			messageRect.height = dimensions.y;
		}

		messageRect.y += Time.deltaTime * scrollSpeed;

		// If message has moved past the right side, move it back to the left.
		if (messageRect.y > Screen.height) {
			messageRect.y = -messageRect.height;
		}

		GUI.Label(messageRect, message);
	}
}