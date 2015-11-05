using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
	public Text UIText;
	
	public void UITextOutput (int OutputKey, float WaitTime) {
		
		switch (OutputKey) {
		
		//------------------------------------------------------
		//----------------------INTRO PAGES---------------------
		//------------------------------------------------------
		case 1:
			StartCoroutine(TimedText("--Press <b>Space</b> to wake up--", WaitTime));
			break;
		case 2:
			UIText.text = "";
			StartCoroutine(TimedText("--Press <b>Space</b> to panic--", WaitTime));
			break;
		case 3:
			UIText.text = "";
			StartCoroutine(TimedText( "--Press <b>Space</b> to calm down--", WaitTime));
			break;
		
		//------------------------------------------------------
		//--------------------IN-CELL ACTIONS-------------------
		//------------------------------------------------------
		//++When in the cell
		case 4:
			UIText.text = "";
			StartCoroutine(TimedText("Press <b>B</b> to inspect the Bed\nPress <b>M</b> to inspect the Mirror\n" +
			                         "Press <b>D</b> to inspect the Door", WaitTime));
			break;
		//++When at the Bed WITHOU the sheet in inventory
		case 5:
			UIText.text = "";
			StartCoroutine(TimedText("Press <b>T</b> to take it\nPress <b>R</b> to return", WaitTime));
			break;
		//++When at the bed WITH the sheet in inventory
		case 6:
			UIText.text = "";
			StartCoroutine(TimedText("--Press <b>Space</b> to leave the Bed--", WaitTime));
			break;
		}
	}
	
	
	
	//-------FUNCTIONS-------
	IEnumerator TimedText(string Words, float WaitTime) {
		yield return new WaitForSeconds(WaitTime);
		UIText.text += Words;
	}
	
}
