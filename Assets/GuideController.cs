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
			//++Initial visit to the door WITHOUT the broken glass, sheet or shiv
		case 18:
			GuideText.text = "It's kind of what you'd expect from a cell door: heavy and ugly.\n";
			StartCoroutine(TimedText("It's made of solid wood, no window, not even bars...\n", 1.5F / Speed));
			StartCoroutine(TimedText("You're not getting out of here any time soon.\n", 3.5F / Speed));
			StartCoroutine(TimedText("Wait...\n", 4.0F / Speed));
			StartCoroutine(TimedText("...\n", 4.25F / Speed));
			StartCoroutine(TimedText("I think there's a guard outside.\n", 5.0F / Speed));
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
			//++Returning to the bed WITH the shiv
		case 16:
			GuideText.text = "You still have the sheet... in a way.";
			break;	
			//++Returning to the mirror WITH the broken glass
		case 13:
			GuideText.text = "Nothing else to see here...\n";
			StartCoroutine(TimedText("Uh...\n", 1.0F / Speed));
			StartCoroutine(TimedText("Sorry.", 2.0F / Speed));
			break;
		case 19:
			GuideText.text = "Sorry, it didn't magically disappear.";
			break; 
			
			//------------------------------------------------------
			//----------------------Actions-------------------------
			//------------------------------------------------------	
			//++When taking the sheet
		case 6:
			GuideText.text = "... ew.\n";
			StartCoroutine(TimedText("What exactly are you planning on doing with that?\n", 1.0F / Speed));
			StartCoroutine(TimedText("Though I guess you were sleeping on it just now.\n", 2.5F / Speed));
			StartCoroutine(TimedText("Still though... ew.", 4.0F / Speed));
			break;
			//++When taking the broken glass
		case 14:
			GuideText.text = "You can grip it like a knife, but it'll cut your hand.";
			break;
			//++When attacking the bed with the broken glass
		case 10:
			GuideText.text = "Ok. You've stabbed the bed and now your hand is bleeding.\n";
			StartCoroutine(TimedText("Was it worth it?", 1.5F / Speed));
			break;
			//++When attacking the bed with the shiv
		case 15:
			GuideText.text = "Hey, that shiv works nicely!\n";
			StartCoroutine(TimedText("You... you can stop stabbing the bed now.", 1.5F / Speed));
			break;
			//++When making the shiv
		case 17:
			GuideText.text = "That's it, wrap the sheet around the glass.\n";
			StartCoroutine(TimedText("Done. Boy that's a mean looking shiv!", 2.0F / Speed));
			break;
			//++When pushing the door
		case 20:
			GuideText.text = "Well, you don't need to be able to see to know it didn't move!";
			break;
			//++When attacking the door bare handed
		case 21:
			GuideText.text = "So now your hands hurt and you've got the guard's attention. Happy?";
			break;
			//++When attacking the door with the broken glass
		case 22:
			GuideText.text = "Ouch! Did that hurt?";
			break;
			//++When attacking the door with the shiv
		case 23:
			GuideText.text = "Keep at it for a few years and might get through.";
			break;
			//++When yelling through the door
		case 24:
			GuideText.text = "Yeah, that did nothing but get the guard's attention. Happy?";
			break;
			//++When trying the door handle and the guard is facing away
		case 25:
			GuideText.text = "Well that's embarrasing; the door wasn't locked!\n";
			StartCoroutine(TimedText("OK, so the corridor is about as charming as the cell.\n", 1.5F / Speed));
			StartCoroutine(TimedText("The guard is armed and he's got his back to you.\n", 2.0F / Speed));
			StartCoroutine(TimedText("Shameful that he hasn't noticed you really.", 3.0F / Speed));
			break;
			//++When trying the door handle and the guard is facing towards
		case 26:
			GuideText.text = "Well that's embarrasing; the door wasn't locked!\n";
			StartCoroutine(TimedText("OK, so the corridor is about as charming as the cell.\n", 1.5F / Speed));
			StartCoroutine(TimedText("The guard is armed and he's staring right at you.\n", 2.0F / Speed));
			StartCoroutine(TimedText("He looks really scared!", 3.0F / Speed));
			break;
			//++When attacking the guard WITH a weapon
		case 27:
			GuideText.text = "NO, NO, STOP! Oh my God, so much blood.";
			break;	
			//++When attacking the guard WITHOUT a weapon
		case 28:
			GuideText.text = "Stop hitting him damn it, he's out... No, don't BITE him!";
			break;	
			//++When talking to the guard while he's facing away
		case 29:
			GuideText.text = "He damn near jumps out of his skin, but he's quick to turn his weapon on you.";
			break;	
			//++When talking to the guard when he's facing towards
		case 30:
			GuideText.text = "No, that hasn't made him any less scared... Just more dangerous.";
			break;	
			//++When sneaking passed the guard
		case 31:
			GuideText.text = "My God... You made it!";
			break;		
			//++When running passed the guard
		case 32:
			GuideText.text = "That's not exactly runnning, more like shuffling... And he's caught you.";
			break;		
		}
	}
	
	
	//-------FUNCTIONS-------
	IEnumerator TimedText(string Words, float WaitTime) {
		yield return new WaitForSeconds(WaitTime);
		GuideText.text += Words;
	}
	
}
