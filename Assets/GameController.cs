using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public UIController UI;
	public GuideController Guide;
	public enum States {Intro, Cell, Mirror, Bed, Door, Corridor, Ending, End};
	public States CurrentState;
	int Page;
	float Speed;
	string[,] Inventory;
	string[,] Visited;
	string GuardFacing;
	//Behvioural scores effect game ending.
	//The scores are Violent and Smart [V,S]
	int[] Score = new int[2];
	
	
	// Use this for initialization
	void Start () {
		Speed = 2.0F;
		CurrentState = States.Intro;	
		Page = -1;
		Inventory = new string[,] {
			{"Mouldy Sheet", "no"}
			,{"Broken Glass", "no"}
			,{"Shiv", "no"}
		};
		Visited = new string[,] {
			{"Cell", "no"}
			,{"Bed", "no"}
			,{"Mirror", "no"}
			,{"Door", "no"}
		};
		GuardFacing = "away";
	}
	
	// Update is called once per frame
	void Update () {
		
		//------------------------------------------------------
		//----------------------INTRO PAGES---------------------
		//------------------------------------------------------
		if(CurrentState == States.Intro & 
		   Page == 1) {
			StartCoroutine(DisablePaging(2, 6.0F / Speed));
			UI.UITextOutput(1, 6.0F / Speed);
			Guide.GuideTextOutput(1, Speed);
		}	
		else if(Input.GetKeyUp(KeyCode.Space) & 
		        CurrentState == States.Intro & 
		        Page == 2) {
			StartCoroutine(DisablePaging(3, 9.0F / Speed));
			UI.UITextOutput(2, 9.5F / Speed);
			Guide.GuideTextOutput(2, Speed);
		}	
		else if(Input.GetKeyUp(KeyCode.Space) & 
		        CurrentState == States.Intro & 
		        Page == 3) {
			StartCoroutine(DisablePaging(-1, 6.0F / Speed));
			UI.UITextOutput(3, 6.5F / Speed);
			Guide.GuideTextOutput(3, Speed);
		}
		//------------------------------------------------------
		//--------------------Initial Visits--------------------
		//------------------------------------------------------
		//++Initial introduction to the cell
		else if(Input.GetKeyUp(KeyCode.Space) & 
		        CurrentState != States.Cell & 
		        Visited[0,1] == "no") {
			StartCoroutine(DisablePaging(-1, 14.0F / Speed));
			UI.UITextOutput(4, 14.0F / Speed);
			Guide.GuideTextOutput(4, Speed);
			CurrentState = States.Cell;
			Visited[0,1] = "yes";
		} 
		//++Initial introduction to the bed WITHOUT the broken glass
		else if(Input.GetKeyUp(KeyCode.B) & 
		        CurrentState != States.Bed &
		        Visited[1,1] == "no" & 
		        Inventory[1,1] == "no") {
			StartCoroutine(DisablePaging(-1, 4.0F / Speed));
			UI.UITextOutput(5, 4.0F / Speed);
			Guide.GuideTextOutput(5, Speed);
			CurrentState = States.Bed;
			Visited[1,1] = "yes";
		}
		//++Initial introduction to the bed WITH the broken glass
		else if(Input.GetKeyUp(KeyCode.B) & 
		        CurrentState != States.Bed &
		        Visited[1,1] == "no" & 
		        Inventory[1,1] == "yes") {
			StartCoroutine(DisablePaging(-1, 4.0F / Speed));
			UI.UITextOutput(8, 4.0F / Speed);
			Guide.GuideTextOutput(5, Speed);
			CurrentState = States.Bed;
			Visited[1,1] = "yes";
		}
		//++Initial introduction to the mirror
		else if(Input.GetKeyUp(KeyCode.M) & 
		        CurrentState != States.Mirror &
		        Visited[2,1] == "no") {
			StartCoroutine(DisablePaging(-1, 4.0F / Speed));
			UI.UITextOutput(9, 4.0F / Speed);
			Guide.GuideTextOutput(11, Speed);
			CurrentState = States.Mirror;
			Visited[2,1] = "yes";
		}
		//++Initial introduction to the door
		else if(Input.GetKeyUp(KeyCode.D) &
				CurrentState != States.Door &
				Visited[3,1] == "no" ) {
			StartCoroutine(DisablePaging(-1, 5.5F / Speed));
			UI.UITextOutput(11, 5.5F / Speed);
			Guide.GuideTextOutput(18, Speed);
			CurrentState = States.Door;
			Visited[3,1] = "yes";		
		}
				
		//------------------------------------------------------
		//-------------------Further Visits---------------------
		//------------------------------------------------------		 
		//++When returning to the cell WITHOUT the sheet and/or WITHOUT the broken glass
		else if((Input.GetKeyUp(KeyCode.Space) | Input.GetKeyUp(KeyCode.R)) & 
		        CurrentState != States.Cell &
		        Visited[0,1] == "yes" &
		        (
		        Inventory[0,1] == "no" |
		        Inventory[1,1] == "no"
		        )) {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(4, 1.0F / Speed);
			Guide.GuideTextOutput(7, Speed);
			CurrentState = States.Cell;
		} 
		//++When returning to the cell WITH the sheet and WITH the broken glass
		else if((Input.GetKeyUp(KeyCode.Space) | Input.GetKeyUp(KeyCode.R)) & 
		        CurrentState != States.Cell &
		        Visited[0,1] == "yes" &
		        Inventory[0,1] == "yes" &
		        Inventory[1,1] == "yes") {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(10, 1.0F / Speed);
			Guide.GuideTextOutput(7, Speed);
			CurrentState = States.Cell;
		} 
		//++When returning to the bed WITH the shiv in inventory
		else if(Input.GetKeyUp(KeyCode.B) & 
		        CurrentState != States.Bed &
		        Visited[1,1] == "yes" & 
		        Inventory[2,1] == "yes") {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(7, 1.0F / Speed);
			Guide.GuideTextOutput(16, Speed);
			CurrentState = States.Bed;
		}
		//++When returning to the bed WITH the sheet & WITH the broken glass
		else if(Input.GetKeyUp(KeyCode.B) & 
		        CurrentState != States.Bed &
		        Visited[1,1] == "yes" & 
		        Inventory[0,1] == "yes" & 
		        Inventory[1,1] == "yes") {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(7, 1.0F / Speed);
			Guide.GuideTextOutput(8, Speed);
			CurrentState = States.Bed;
		}
		//++When returning to the bed WITH the sheet & WITHOUT the broken glass in inventory
		else if(Input.GetKeyUp(KeyCode.B) & 
		        CurrentState != States.Bed &
		        Visited[1,1] == "yes" & 
		        Inventory[0,1] == "yes" & 
		        Inventory[1,1] == "no") {
			StartCoroutine(DisablePaging(-1, 1.5F / Speed));
			UI.UITextOutput(6, 1.5F / Speed);
			Guide.GuideTextOutput(8, Speed);
			CurrentState = States.Bed;
		} 
		//++When returning to the bed WITHOUT the sheet & WITHOUT the broken glass in inventory
		else if(Input.GetKeyUp(KeyCode.B) & 
		        CurrentState != States.Bed &
		        Visited[1,1] == "yes" & 
		        Inventory[0,1] == "no" &
		        Inventory[1,1] == "no") {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(5, 1.0F / Speed);
			Guide.GuideTextOutput(9, Speed);
			CurrentState = States.Bed;
		}
		//++When returning to the bed WITHOUT the sheet & WITH the broken glass in inventory
		else if(Input.GetKeyUp(KeyCode.B) & 
		        CurrentState != States.Bed &
		        Visited[1,1] == "yes" & 
		        Inventory[0,1] == "no" &
		        Inventory[1,1] == "yes") {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(8, 1.0F / Speed);
			Guide.GuideTextOutput(9, Speed);
			CurrentState = States.Bed;
		}
		//++Returning to the mirror WITHOUT the broken glass
		else if(Input.GetKeyUp(KeyCode.M) & 
		        CurrentState != States.Mirror &
		        Visited[2,1] == "yes" &
		        Inventory[1,1] == "no" &
		        Inventory[2,1] == "no") {
			StartCoroutine(DisablePaging(-1, 3.0F / Speed));
			UI.UITextOutput(9, 3.0F / Speed);
			Guide.GuideTextOutput(12, Speed);
			CurrentState = States.Mirror;
		}
		//++Returning to the mirror WITH the broken glass
		else if(Input.GetKeyUp(KeyCode.M) & 
		        CurrentState != States.Mirror &
		        Visited[2,1] == "yes" &
		        (
		        Inventory[1,1] == "yes" |
		        Inventory[2,1] == "yes"
		        )) {
			StartCoroutine(DisablePaging(-1, 2.0F / Speed));
			UI.UITextOutput(6, 2.0F / Speed);
			Guide.GuideTextOutput(13, Speed);
			CurrentState = States.Mirror;
		}
		//++Returning to the door
		else if(Input.GetKeyUp(KeyCode.D) &
		        CurrentState != States.Door &
		        Visited[3,1] == "yes" ) {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(11, 1.0F / Speed);
			Guide.GuideTextOutput(19, Speed);
			CurrentState = States.Door;	
		}
		
		//------------------------------------------------------
		//----------------------Actions-------------------------
		//------------------------------------------------------	
		//++When taking the sheet
		else if(Input.GetKeyUp(KeyCode.T) & 
		        CurrentState == States.Bed & 
		        Inventory[0,1] == "no") {
			StartCoroutine(DisablePaging(-1, 4.0F / Speed));
			UI.UITextOutput(6, 4.0F / Speed);
			Guide.GuideTextOutput(6, Speed);
			Inventory[0,1] = "yes";	
			Score[0] += 0;
			Score[1] += 1;
		}
		//++When taking the broken glass
		else if(Input.GetKeyUp(KeyCode.T) &
		        CurrentState == States.Mirror &
		        Inventory[1,1] == "no") {
			StartCoroutine(DisablePaging(-1, 2.0F / Speed));
			UI.UITextOutput(6, 2.0F / Speed);
			Guide.GuideTextOutput(14, Speed); 
			Inventory[1,1] = "yes";	
			Score[0] += 1;
			Score[1] += 1;		
		}
		//++When attacking the bed with the broken glass
		else if(Input.GetKeyUp(KeyCode.A) & 
		        CurrentState == States.Bed & 
		        Inventory[1,1] == "yes") {
			StartCoroutine(DisablePaging(-1, 2.0F / Speed));
			UI.UITextOutput(6, 2.0F / Speed);
			Guide.GuideTextOutput(10, Speed);	
			Score[0] += 1;
			Score[1] += 0;
		}
		//++When attacking the bed with the shiv
		else if(Input.GetKeyUp(KeyCode.A) & 
		        CurrentState == States.Bed & 
		        Inventory[2,1] == "yes") {
			StartCoroutine(DisablePaging(-1, 2.0F / Speed));
			UI.UITextOutput(6, 2.0F / Speed);
			Guide.GuideTextOutput(15, Speed);	
			Score[0] += 1;
			Score[1] += 1;
		}
		//++When making the shiv
		else if(Input.GetKeyUp(KeyCode.C) &
				CurrentState == States.Cell &
				Inventory[0,1] == "yes" &
				Inventory[1,1] == "yes") {
			StartCoroutine(DisablePaging(-1, 2.5F / Speed));
			UI.UITextOutput(4, 2.5F / Speed);
			Guide.GuideTextOutput(17, Speed);
			Inventory[0,1] = "no";
			Inventory[1,1] = "no";
			Inventory[2,1] = "yes";	
			Score[0] += 1;
			Score[1] += 2;
		}
		//++When pushing the door
		else if(Input.GetKeyUp(KeyCode.P) &
		        CurrentState == States.Door) {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(11, 1.0F / Speed);
			Guide.GuideTextOutput(20, Speed);	
			Score[0] += 0;
			Score[1] += 0;
		}
		//++When attacking the door bare handed
		else if(Input.GetKeyUp(KeyCode.A) &
		        CurrentState == States.Door &
		        Inventory[1,1] == "no" &
		        Inventory[2,1] == "no") {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(11, 1.0F / Speed);
			Guide.GuideTextOutput(21, Speed);
			GuardFacing = "towards";	
			Score[0] += 2;
			Score[1] += 0;
		}
		//++When attacking the door with the broken glass
		else if(Input.GetKeyUp(KeyCode.A) &
		        CurrentState == States.Door &
		        Inventory[1,1] == "yes") {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(11, 1.0F / Speed);
			Guide.GuideTextOutput(22, Speed);	
			Score[0] += 1;
			Score[1] += 1;
		}
		//++When attacking the door with the shiv
		else if(Input.GetKeyUp(KeyCode.A) &
		        CurrentState == States.Door &
		        Inventory[2,1] == "yes") {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(11, 1.0F / Speed);
			Guide.GuideTextOutput(23, Speed);	
			Score[0] += 1;
			Score[1] += 1;
		}
		//++When yelling through the door
		else if(Input.GetKeyUp(KeyCode.Y) &
		        CurrentState == States.Door) {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(11, 1.0F / Speed);
			Guide.GuideTextOutput(24, Speed);
			GuardFacing = "towards";	
			Score[0] += 1;
			Score[1] += 1;
		}
		//++When trying the door handle and the guard is facing away
		else if(Input.GetKeyUp(KeyCode.L) &
		        CurrentState == States.Door &
		        GuardFacing == "away") {
			StartCoroutine(DisablePaging(-1, 3.5F / Speed));
			UI.UITextOutput(12, 5.0F / Speed);
			Guide.GuideTextOutput(25, Speed);
			CurrentState = States.Corridor;	
			Score[0] += 0;
			Score[1] += 2;
		}
		//++When trying the door handle and the guard is facing towards
		else if(Input.GetKeyUp(KeyCode.L) &
		        CurrentState == States.Door &
		        GuardFacing == "towards") {
			StartCoroutine(DisablePaging(-1, 3.5F / Speed));
			UI.UITextOutput(13, 5.0F / Speed);
			Guide.GuideTextOutput(26, Speed);
			CurrentState = States.Corridor;	
			Score[0] += 0;
			Score[1] += 2;
		}
		//++When attacking the guard WITH the glass
		else if(Input.GetKeyUp(KeyCode.A) &
		        CurrentState == States.Corridor &
		        Inventory[1,1] == "yes") {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(14, 0.0F);
			Guide.GuideTextOutput(27, Speed);
			CurrentState = States.Ending;	
			Score[0] += 1;
			Score[1] += 0;
		}
		//++When attacking the guard WITH the shiv
		else if(Input.GetKeyUp(KeyCode.A) &
				CurrentState == States.Corridor &
				Inventory[2,1] == "yes") {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(14, 0.0F);
			Guide.GuideTextOutput(27, Speed);
			CurrentState = States.Ending;	
			Score[0] += 1;
			Score[1] += 1;
		}
		//++When attacking the guard WITHOUT a weapon
		else if(Input.GetKeyUp(KeyCode.A) &
		        CurrentState == States.Corridor &
		        Inventory[1,1] == "no" &
				Inventory[2,1] == "no") {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(14, 0.0F);
			Guide.GuideTextOutput(28, Speed);
			CurrentState = States.Ending;	
			Score[0] += 4;
			Score[1] += 0;	
		}
		//++When talking to the guard while he's facing away
		else if(Input.GetKeyUp(KeyCode.T) &
		        CurrentState == States.Corridor &
		        GuardFacing == "away") {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(14, 0.0F);
			Guide.GuideTextOutput(29, Speed);
			CurrentState = States.Ending;	
			Score[0] += 0;
			Score[1] += 1;
		}
		//++When talking to the guard when he's facing towards
		else if(Input.GetKeyUp(KeyCode.A) &
		        CurrentState == States.Corridor &
		        GuardFacing == "towards") {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(14, 0.0F);
			Guide.GuideTextOutput(30, Speed);
			CurrentState = States.Ending;	
			Score[0] += 0;
			Score[1] += 1;
		}
		//++When sneaking passed the guard
		else if(Input.GetKeyUp(KeyCode.S) &
		        CurrentState == States.Corridor) {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(14, 0.0F);
			Guide.GuideTextOutput(31, Speed);
			CurrentState = States.Ending;	
			Score[0] += 0;
			Score[1] += 1;
		}	
		//++When running passed the guard
		else if(Input.GetKeyUp(KeyCode.R) &
		        CurrentState == States.Corridor) {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(14, 0.0F);
			Guide.GuideTextOutput(32, Speed);
			CurrentState = States.Ending;	
			Score[0] += 0;
			Score[1] += 1;    
		}
		
	}
	
	//-------FUNCTIONS-------
	IEnumerator DisablePaging(int NextPage, float WaitTime) {
		Page = 0;
		yield return new WaitForSeconds(WaitTime);
		Page = NextPage;
	}
}




