using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuideController : MonoBehaviour {
	public Text GuideText;
	
	public void GuideTextOutput (int OutputKey) {
		switch (OutputKey) {
			//------------------------------------------------------
			//----------------------INTRO PAGES---------------------
			//------------------------------------------------------
		case 1:
			StartCoroutine(TimedText("Hello?\n", 0.5F));	
			StartCoroutine(TimedText("Hey, can you hear me?\n", 2.5F));
			StartCoroutine(TimedText("...\n", 3.5F));
			StartCoroutine(TimedText("...\n", 5.0F));
			StartCoroutine(TimedText("WAKE UP!", 6.0F));
			break;
		case 2:
			GuideText.text = "There we go, that wasn't so hard was it?\n";
			StartCoroutine(TimedText("I know, I know... you can't see.\n", 2.0F));
			StartCoroutine(TimedText("Relax, the light's out.\n", 4.0F));
			StartCoroutine(TimedText("Plus, well, you know...\n", 6.0F)); 
			StartCoroutine(TimedText("You're blind...\n", 7.50F));
			StartCoroutine(TimedText("Blind-ish, anyway.", 8.5F));
			break;
		case 3:
			GuideText.text = "It's temporary, OK?" + '\n';
			StartCoroutine(TimedText("They did... something, to you...\n", 1.5F));
			StartCoroutine(TimedText("It doesn't matter what right now,\n", 3.0F));
			StartCoroutine(TimedText("Let's just focus on getting you out of here.\n", 4.0F)); 
			StartCoroutine(TimedText("OK?", 5.5F));
			break;
		case 4:
			GuideText.text = "Good. Thanks.\n";
			StartCoroutine(TimedText("OK, here's the situation:\n", 1.5F));
			StartCoroutine(TimedText("You're in one of their cells, proper Dank Dungeon sort of place.\n", 3.0F));
			StartCoroutine(TimedText("The bed you're lying on has a sheet on it but it's kind of... mouldy.\n", 6.0F));
			StartCoroutine(TimedText("Uh... nothing else here but a broken mirrror and...\n", 9.0F));
			StartCoroutine(TimedText("And the door, I guess. But it's locked so why count it, right?\n", 10.5F));
			StartCoroutine(TimedText("Right?", 13.5F));
			break;
			//------------------------------------------------------
			//--------------------IN-CELL STATES--------------------
			//------------------------------------------------------
		case 5:
			GuideText.text = "Alright, yeah, you're standing... well done!\n";
			StartCoroutine(TimedText("So yeah, the bed's got a nasty looking sheet on it.\n", 1.5F));
			StartCoroutine(TimedText("I don't know why you'd want it, but I guess you could take it.\n\n\n", 2.5F));
			break;
		}
	}
	
	
	//-------FUNCTIONS-------
	IEnumerator TimedText(string Words, float WaitTime) {
		yield return new WaitForSeconds(WaitTime);
		GuideText.text += Words;
	}
	
}
