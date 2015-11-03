using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
	public Text UIText;
	
	public void UITextOutput (int OutputKey) {
		
		switch (OutputKey) {
			//------------------------------------------------------
			//----------------------INTRO PAGES---------------------
			//------------------------------------------------------
		case 1:
			StartCoroutine(TimedText("--Press <b>Space</b> to wake up--", 6.0F));
			break;
		case 2:
			UIText.text = "";
			StartCoroutine(TimedText("--Press <b>Space</b> to panic--", 9.5F));
			break;
		case 3:
			UIText.text = "";
			StartCoroutine(TimedText( "--Press <b>Space</b> to calm down--", 6.5F));
			break;
			//------------------------------------------------------
			//--------------------IN-CELL STATES--------------------
			//------------------------------------------------------
		case 4:
			UIText.text = "";
			StartCoroutine(TimedText("Press <b>S</b> to inspect the Sheet\nPress <b>M</b> to inspect the Mirror\n" +
										"Press <b>D</b> to inspect the Door", 14.0F));
			break;
		case 5:
			UIText.text = "";
			StartCoroutine(TimedText("Press <b>T</b> to take it\nPress <b>R</b> to return", 4.0F));
			break;
		case 6:
			UIText.text = "";
			StartCoroutine(TimedText("--Press <b>Space</b> to leave the bed--", 4.0F));
			break;
		case 7:
			UIText.text = "";
			StartCoroutine(TimedText("Press <b>S</b> to inspect the Sheet\nPress <b>M</b> to inspect the Mirror\n" +
			                         "Press <b>D</b> to inspect the Door", 1.0F));
			break;
		case 8:
			UIText.text = "";
			StartCoroutine(TimedText("--Press <b>Space</b> to leave the bed--", 1.5F));
			break;
		case 9:
			UIText.text = "";
			StartCoroutine(TimedText("Press <b>T</b> to take it\nPress <b>R</b> to return", 1.0F));
			break;
		}
	}
	
	
	
	//-------FUNCTIONS-------
	IEnumerator TimedText(string Words, float WaitTime) {
		yield return new WaitForSeconds(WaitTime);
		UIText.text += Words;
	}
	
}
