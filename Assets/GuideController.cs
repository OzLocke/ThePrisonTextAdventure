using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuideController : MonoBehaviour {
	public Text GuideText;
	
	public void GuideTextOutput (int OutputKey, float Speed) {
		switch (OutputKey) {
			//------------------------------------------------------
			//----------------------INTRO PAGES---------------------
			//------------------------------------------------------
		case 1:
			StartCoroutine(TimedText("Hello?\n", 0.5F / Speed));	
			StartCoroutine(TimedText("Hey, can you hear me?\n", 2.5F / Speed));
			StartCoroutine(TimedText("...\n", 3.5F / Speed));
			StartCoroutine(TimedText("...\n", 5.0F / Speed));
			StartCoroutine(TimedText("WAKE UP!", 6.0F / Speed));
			break;
		case 2:
			GuideText.text = "There we go, that wasn't so hard was it?\n";
			StartCoroutine(TimedText("I know, I know... you can't see.\n", 2.0F / Speed));
			StartCoroutine(TimedText("Relax, the light's out.\n", 4.0F / Speed));
			StartCoroutine(TimedText("Plus, well, you know...\n", 6.0F / Speed)); 
			StartCoroutine(TimedText("You're blind...\n", 7.50F / Speed));
			StartCoroutine(TimedText("Blind-ish, anyway.", 8.5F / Speed));
			break;
		case 3:
			GuideText.text = "It's temporary, OK?" + '\n';
			StartCoroutine(TimedText("They did... something, to you...\n", 1.5F / Speed));
			StartCoroutine(TimedText("It doesn't matter what right now,\n", 3.0F / Speed));
			StartCoroutine(TimedText("Let's just focus on getting you out of here.\n", 4.0F / Speed)); 
			StartCoroutine(TimedText("OK?", 5.5F / Speed));
			break;
			
			//------------------------------------------------------
			//--------------------Initial Visits--------------------
			//------------------------------------------------------
			//++Initial introduction to the cell
		case 4:
			GuideText.text = "Good. Thanks.\n";
			StartCoroutine(TimedText("OK, here's the situation:\n", 1.5F / Speed));
			StartCoroutine(TimedText("You're in one of their cells, proper Dank Dungeon sort of place.\n", 3.0F / Speed));
			StartCoroutine(TimedText("The bed you're lying on has a sheet on it but it's kind of... mouldy.\n", 6.0F / Speed));
			StartCoroutine(TimedText("Uh... nothing else here but a broken mirrror and...\n", 9.0F / Speed));
			StartCoroutine(TimedText("And the door, I guess. But it's locked so why count it, right?\n", 10.5F / Speed));
			StartCoroutine(TimedText("Right?", 12.5F / Speed));
			break;
			//++Initial introduction to the bed
		case 5:
			GuideText.text = "Hey, at least they gave you a bed to lie on.\n";
			StartCoroutine(TimedText("So yeah, the bed's got a nasty looking sheet on it.\n", 1.5F / Speed));
			StartCoroutine(TimedText("I don't know why you'd want it, but I guess you could take it.", 2.5F / Speed));
			break;
			//++Initial introduction to the mirror
		case 11:
			GuideText.text = "It's a mirror, which seems like an insult given they blinded you.\n";
			StartCoroutine(TimedText("It's cracked though, a bit of it is jutting out...\n", 2.0F / Speed));
			StartCoroutine(TimedText("It looks pretty sharp.", 4.0F / Speed));
			break;
			//++Returning to the mirror WITHOUT the broken glass
		case 12:
			GuideText.text = "The mirror's still here, and it's still got that nasty bit sticking out.\n";
			StartCoroutine(TimedText("You could take it...\n", 2.0F / Speed));
			StartCoroutine(TimedText("Carefully.", 3.0F / Speed));
			break;
			//++Returning to the mirror WITH the broken glass
		case 13:
			GuideText.text = "Nothing else to see here...\n";
			StartCoroutine(TimedText("Uh...\n", 1.0F / Speed));
			StartCoroutine(TimedText("Sorry.", 2.0F / Speed));
			break;
			
			//------------------------------------------------------
			//-------------------Further Visits---------------------
			//------------------------------------------------------	
			//++When returning to the cell
		case 7:
			GuideText.text = "Yup, still in the cell.\n";
			break;
			//++When returning to the bed WITH the sheet in inventory
		case 8:
			GuideText.text = "You still have the sheet.\n";
			StartCoroutine(TimedText("Come on, if there was anything else here I'd have told you.\n", 1.0F / Speed));
			break;
			//++When returning to the bed WITHOUT the sheet in inventory
		case 9:
			GuideText.text = "Still just the manky sheet. You want it?";
			break;
			
			//------------------------------------------------------
			//----------------------Actions-------------------------
			//------------------------------------------------------	
		case 6:
			GuideText.text = "... ew.\n";
			StartCoroutine(TimedText("What exactly are you planning on doing with that?\n", 1.0F / Speed));
			StartCoroutine(TimedText("Though I guess you were sleeping on it just now.\n", 2.5F / Speed));
			StartCoroutine(TimedText("Still though... ew.", 4.0F / Speed));
			break;
		case 10:
			GuideText.text = "Ok. You've stabbed the bed...\n";
			StartCoroutine(TimedText("Feel better now?", 1.5F / Speed));
			break;
		case 14:
			GuideText.text = "You can grip it like a knife, but it'll cut your hand.";
			break;
		}
	}
	
	
	//-------FUNCTIONS-------
	IEnumerator TimedText(string Words, float WaitTime) {
		yield return new WaitForSeconds(WaitTime);
		GuideText.text += Words;
	}
	
}
