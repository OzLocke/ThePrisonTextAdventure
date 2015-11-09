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
			StartCoroutine(TimedText("Press <b>Space</b> to wake up", WaitTime));
			break;
		case 2:
			UIText.text = "";
			StartCoroutine(TimedText("Press <b>Space</b> to panic", WaitTime));
			break;
		case 3:
			UIText.text = "";
			StartCoroutine(TimedText( "Press <b>Space</b> to calm down", WaitTime));
			break;
		
		//------------------------------------------------------
		//--------------------IN-CELL ACTIONS-------------------
		//------------------------------------------------------
		//++When returning to the cell WITHOUT the sheet and/or WITHOUT the broken glass
		case 4:
			UIText.text = "";
			StartCoroutine(TimedText("Press <b>B</b> to inspect the Bed\nPress <b>M</b> to inspect the Mirror\n" +
			                         "Press <b>D</b> to inspect the Door", WaitTime));
			break;
		//++When returning to the cell WITH the sheet and WITH the broken glass
		case 10:
			UIText.text = "";
			StartCoroutine(TimedText("Press <b>B</b> to inspect the Bed\nPress <b>M</b> to inspect the Mirror\n" +
			                         "Press <b>D</b> to inspect the Door\nPress <b>C</b> to combine things", WaitTime));
			break;
		//++When at the Bed WITHOUT the sheet in inventory
		case 5:
			UIText.text = "";
			StartCoroutine(TimedText("Press <b>T</b> to take it\nPress <b>R</b> to return", WaitTime));
			break;
		//++When at the bed WITH the sheet in inventory
		case 6:
			UIText.text = "";
			StartCoroutine(TimedText("Press <b>Space</b> to return to the cell", WaitTime));
			break;
		//++When at the bed (WITH the broken glass and WITH the sheet) or WITH the shiv in inventory
		case 7:
			UIText.text = "";
			StartCoroutine(TimedText("Press <b>A</b> to attack\nPress <b>R</b> to return", WaitTime));
			break;
		//++When at the bed WITH the broken glass and WITHOUT the sheet
		case 8:
			UIText.text = "";
			StartCoroutine(TimedText("Press <b>T</b> to take it\nPress <b>A</b> to attack\nPress <b>R</b> to return", WaitTime));
			break;
		//++When at the bed WITHOUT the broken glass in inventory
		case 9:
			UIText.text = "";
			StartCoroutine(TimedText("Press <b>T</b> to take it\nPress <b>R</b> to return", WaitTime));
			break;
		}
	}
	
	
	
	//-------FUNCTIONS-------
	IEnumerator TimedText(string Words, float WaitTime) {
		yield return new WaitForSeconds(WaitTime);
		UIText.text += Words;
	}
	
}
