using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	
	void OnGUI()
	{
		GUIStyle style = new GUIStyle ();
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = 24;
		style.normal.textColor = Color.white;
		GUI.Label (new Rect(10,(Screen.height/2) - 16,Screen.width-20,32),"Game Over", style);

		if(GUI.Button(new Rect(Screen.width/2 - 32, Screen.height/2 + 16,64,32),"Re-Start"))
		{
			Application.LoadLevel("menu");
		}
	}
}
