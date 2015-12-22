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
		Speed = 1.5F;
		CurrentState = States.Intro;	
		Page = 1;
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
		//Score[0] = 0;
		//Score[1] = 3;
	}
	
	//-------FUNCTIONS-------
	IEnumerator DisablePaging(int NextPage, float WaitTime) {
		Page = 0;
		yield return new WaitForSeconds(WaitTime);
		Page = NextPage;
	}
	
	// Update is called once per frame
	void Update () {
		
		//------------------------------------------------------
		//----------------------INTRO PAGES---------------------
		//------------------------------------------------------
		if(CurrentState == States.Intro & Page == 1) {PageOne();}	
		else if(Input.GetKeyUp(KeyCode.Space) & CurrentState == States.Intro & Page == 2) {PageTwo();}	
		else if(Input.GetKeyUp(KeyCode.Space) & CurrentState == States.Intro & Page == 3) {PageThree();}
		//------------------------------------------------------
		//--------------------Initial Visits--------------------
		//------------------------------------------------------
		else if(Input.GetKeyUp(KeyCode.Space) & CurrentState != States.Cell & CurrentState != States.Ending & CurrentState != States.End & Visited[0,1] == "no") {InitialVisitCell();}
		else if(Input.GetKeyUp(KeyCode.B) & CurrentState != States.Bed & Visited[1,1] == "no" & Inventory[1,1] == "no") {InitialVisitBed_WithoutGlass();}
		else if(Input.GetKeyUp(KeyCode.B) & CurrentState != States.Bed & Visited[1,1] == "no" & Inventory[1,1] == "yes") {InitialVisitBed_WithGlass();}
		else if(Input.GetKeyUp(KeyCode.M) & CurrentState != States.Mirror & Visited[2,1] == "no") {InitialVisitMirror();}
		else if(Input.GetKeyUp(KeyCode.D) & CurrentState != States.Door & Visited[3,1] == "no" ) {InitialVisitDoor();}		
		//------------------------------------------------------
		//-------------------Further Visits---------------------
		//------------------------------------------------------	
		else if((Input.GetKeyUp(KeyCode.Space) | Input.GetKeyUp(KeyCode.R)) & CurrentState != States.Cell & CurrentState != States.Ending & CurrentState != States.End & 
					Visited[0,1] == "yes" & (Inventory[0,1] == "no" | Inventory[1,1] == "no")) {ReturnCell_WithoutSheetOrGlass();} 
		else if((Input.GetKeyUp(KeyCode.Space) | Input.GetKeyUp(KeyCode.R)) & CurrentState != States.Cell & Visited[0,1] == "yes" & Inventory[0,1] == "yes" & Inventory[1,1] == "yes") {ReturnCell_WithSheetAndGlass();} 
		else if(Input.GetKeyUp(KeyCode.B) & CurrentState != States.Bed &Visited[1,1] == "yes" & Inventory[2,1] == "yes") {ReturnBed_WithShiv();}
		else if(Input.GetKeyUp(KeyCode.B) & CurrentState != States.Bed &Visited[1,1] == "yes" & Inventory[0,1] == "yes" & Inventory[1,1] == "yes") {ReturnBed_WithSheetAndWithGlass();}
		else if(Input.GetKeyUp(KeyCode.B) & CurrentState != States.Bed &Visited[1,1] == "yes" & Inventory[0,1] == "yes" & Inventory[1,1] == "no") {ReturnBed_WithSheetAndWithoutGlass();} 
		else if(Input.GetKeyUp(KeyCode.B) & CurrentState != States.Bed &Visited[1,1] == "yes" & Inventory[0,1] == "no" & Inventory[1,1] == "no") {ReturnBed_WithoutSheetAndGlass();}
		else if(Input.GetKeyUp(KeyCode.B) & CurrentState != States.Bed & Visited[1,1] == "yes" & Inventory[0,1] == "no" & Inventory[1,1] == "yes") {ReturnBed_WithoutSheetAndWithGlass();}
		else if(Input.GetKeyUp(KeyCode.M) & CurrentState != States.Mirror & Visited[2,1] == "yes" & Inventory[1,1] == "no" & Inventory[2,1] == "no") {ReturnMirror_WithoutGlass();}
		else if(Input.GetKeyUp(KeyCode.M) & CurrentState != States.Mirror & Visited[2,1] == "yes" & (Inventory[1,1] == "yes" | Inventory[2,1] == "yes")) {ReturnMirror_WithGlassOrShiv();}
		else if(Input.GetKeyUp(KeyCode.D) & CurrentState != States.Door & Visited[3,1] == "yes" ) {ReturnDoor();}
		//------------------------------------------------------
		//----------------------Actions-------------------------
		//------------------------------------------------------	
		else if(Input.GetKeyUp(KeyCode.T) & CurrentState == States.Bed & Inventory[0,1] == "no") {Action_TakeSheet();}
		else if(Input.GetKeyUp(KeyCode.T) & CurrentState == States.Mirror & Inventory[1,1] == "no") {Action_TakeGlass();}
		else if(Input.GetKeyUp(KeyCode.A) & CurrentState == States.Bed & Inventory[1,1] == "yes") {Action_AttackBedWithGlass();}
		else if(Input.GetKeyUp(KeyCode.A) & CurrentState == States.Bed & Inventory[2,1] == "yes") {Action_AttackBedWithShiv();}
		else if(Input.GetKeyUp(KeyCode.C) & CurrentState == States.Cell & Inventory[0,1] == "yes" & Inventory[1,1] == "yes") {Action_MakeShiv();}
		else if(Input.GetKeyUp(KeyCode.A) & CurrentState == States.Door & Inventory[1,1] == "no" & Inventory[2,1] == "no") {Action_AttackDoorWithFists();}
		else if(Input.GetKeyUp(KeyCode.A) & CurrentState == States.Door & Inventory[1,1] == "yes") {Action_AttackDoorWithGlass();}
		else if(Input.GetKeyUp(KeyCode.A) & CurrentState == States.Door & Inventory[2,1] == "yes") {Action_AttackDoorWithShiv();}
		else if(Input.GetKeyUp(KeyCode.Y) & CurrentState == States.Door) {Action_YellThroughDoor();}
		else if(Input.GetKeyUp(KeyCode.F) & CurrentState == States.Door & GuardFacing == "away") {Action_ForceDoor_GuardFacingAway();}
		else if(Input.GetKeyUp(KeyCode.F) & CurrentState == States.Door & GuardFacing == "towards") {Action_ForceDoor_GuardFacingTowards();}
		else if(Input.GetKeyUp(KeyCode.L) & CurrentState == States.Door & GuardFacing == "away") {Action_TryLock_GuardFacingAway();}
		else if(Input.GetKeyUp(KeyCode.L) & CurrentState == States.Door & GuardFacing == "towards") {Action_TryLock_GuardFacingTowards();}
		//-----FINAL STAGE ACTIONS, only one can be chosen per game
		else if(Input.GetKeyUp(KeyCode.A) & CurrentState == States.Corridor & (Inventory[1,1] == "yes" | Inventory[2,1] == "yes")) {FinalAction_AttackGuardWithWeapon();}
		else if(Input.GetKeyUp(KeyCode.A) & CurrentState == States.Corridor & Inventory[1,1] == "no" & Inventory[2,1] == "no") {FinalAction_AttackGuardWithoutWeapon();}
		else if(Input.GetKeyUp(KeyCode.T) & CurrentState == States.Corridor & GuardFacing == "away") {FinalAction_Talk_GuardFacingAway();}
		else if(Input.GetKeyUp(KeyCode.A) & CurrentState == States.Corridor & GuardFacing == "towards") {FinalAction_Talk_GuardFacingTowards();}
		else if(Input.GetKeyUp(KeyCode.S) & CurrentState == States.Corridor & GuardFacing == "away") {FinalAction_Sneak();}	
		else if(Input.GetKeyUp(KeyCode.R) & CurrentState == States.Corridor & GuardFacing == "towards") {FinalAction_Run();}
		//------------------------------------------------------
		//---------------------ENDING PAGES---------------------
		//------------------------------------------------------
		else if(Input.GetKeyUp(KeyCode.Space) & CurrentState == States.Ending & Score[0] == 0 & Score[1] != 0) {Ending_EasterEgg();}
		else if(Input.GetKeyUp(KeyCode.Space) & CurrentState == States.Ending & Score[0] > 7 & Score[1] <= 7) {Ending_Violent();}
		else if(Input.GetKeyUp(KeyCode.Space) & CurrentState == States.Ending & Score[0] <=7 & Score[1] >7) {Ending_Smart();}
		else if(Input.GetKeyUp(KeyCode.Space) & CurrentState == States.Ending & (Score[0] > 7 & Score[1] > 7 | Score[0] <= 7 & Score[1] <= 7)) {Ending_Useless();}
		else if(Input.GetKeyUp(KeyCode.Space) & CurrentState == States.End) {Ending_Restart();}
		
	}

	#region screens
	//------------------------------------------------------
	//----------------------INTRO PAGES---------------------
	//------------------------------------------------------
	void PageOne() {
			StartCoroutine(DisablePaging(2, 6.0F / Speed));
			UI.UITextOutput(1, 6.0F / Speed);
			Guide.GuideTextOutput(1, Speed);
		}	
	void PageTwo() {
			StartCoroutine(DisablePaging(3, 9.0F / Speed));
			UI.UITextOutput(2, 9.5F / Speed);
			Guide.GuideTextOutput(2, Speed);
		}	
	void PageThree() {
			StartCoroutine(DisablePaging(-1, 6.0F / Speed));
			UI.UITextOutput(3, 6.5F / Speed);
			Guide.GuideTextOutput(3, Speed);
		}
	//------------------------------------------------------
	//--------------------Initial Visits--------------------
	//------------------------------------------------------
	void InitialVisitCell() {
			StartCoroutine(DisablePaging(-1, 14.0F / Speed));
			UI.UITextOutput(4, 14.0F / Speed);
			Guide.GuideTextOutput(4, Speed);
			CurrentState = States.Cell;
			Visited[0,1] = "yes";
		} 
		//++Initial introduction to the bed WITHOUT the broken glass
	void InitialVisitBed_WithoutGlass() {
			StartCoroutine(DisablePaging(-1, 4.0F / Speed));
			UI.UITextOutput(5, 4.0F / Speed);
			Guide.GuideTextOutput(5, Speed);
			CurrentState = States.Bed;
			Visited[1,1] = "yes";
		}
		//++Initial introduction to the bed WITH the broken glass
	void InitialVisitBed_WithGlass() {
			StartCoroutine(DisablePaging(-1, 4.0F / Speed));
			UI.UITextOutput(8, 4.0F / Speed);
			Guide.GuideTextOutput(5, Speed);
			CurrentState = States.Bed;
			Visited[1,1] = "yes";
		}
		//++Initial introduction to the mirror
	void InitialVisitMirror() {
			StartCoroutine(DisablePaging(-1, 4.0F / Speed));
			UI.UITextOutput(9, 4.0F / Speed);
			Guide.GuideTextOutput(11, Speed);
			CurrentState = States.Mirror;
			Visited[2,1] = "yes";
		}
		//++Initial introduction to the door
	void InitialVisitDoor() {
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
	void ReturnCell_WithoutSheetOrGlass() {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(4, 1.0F / Speed);
			Guide.GuideTextOutput(7, Speed);
			CurrentState = States.Cell;
		} 
		//++When returning to the cell WITH the sheet and WITH the broken glass
	void ReturnCell_WithSheetAndGlass() {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(10, 1.0F / Speed);
			Guide.GuideTextOutput(7, Speed);
			CurrentState = States.Cell;
		} 
		//++When returning to the bed WITH the shiv in inventory
	void ReturnBed_WithShiv() {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(7, 1.0F / Speed);
			Guide.GuideTextOutput(16, Speed);
			CurrentState = States.Bed;
		}
		//++When returning to the bed WITH the sheet & WITH the broken glass
	void ReturnBed_WithSheetAndWithGlass() {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(7, 1.0F / Speed);
			Guide.GuideTextOutput(8, Speed);
			CurrentState = States.Bed;
		}
		//++When returning to the bed WITH the sheet & WITHOUT the broken glass in inventory
	void ReturnBed_WithSheetAndWithoutGlass() {
			StartCoroutine(DisablePaging(-1, 1.5F / Speed));
			UI.UITextOutput(6, 1.5F / Speed);
			Guide.GuideTextOutput(8, Speed);
			CurrentState = States.Bed;
		} 
		//++When returning to the bed WITHOUT the sheet & WITHOUT the broken glass in inventory
	void ReturnBed_WithoutSheetAndGlass() {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(5, 1.0F / Speed);
			Guide.GuideTextOutput(9, Speed);
			CurrentState = States.Bed;
		}
		//++When returning to the bed WITHOUT the sheet & WITH the broken glass in inventory
	void ReturnBed_WithoutSheetAndWithGlass() {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(8, 1.0F / Speed);
			Guide.GuideTextOutput(9, Speed);
			CurrentState = States.Bed;
		}
		//++Returning to the mirror WITHOUT the broken glass
	void ReturnMirror_WithoutGlass() {
			StartCoroutine(DisablePaging(-1, 3.0F / Speed));
			UI.UITextOutput(9, 3.0F / Speed);
			Guide.GuideTextOutput(12, Speed);
			CurrentState = States.Mirror;
		}
		//++Returning to the mirror WITH the broken glass
	void ReturnMirror_WithGlassOrShiv() {
			StartCoroutine(DisablePaging(-1, 2.0F / Speed));
			UI.UITextOutput(6, 2.0F / Speed);
			Guide.GuideTextOutput(13, Speed);
			CurrentState = States.Mirror;
		}
		//++Returning to the door
	void ReturnDoor() {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(11, 1.0F / Speed);
			Guide.GuideTextOutput(19, Speed);
			CurrentState = States.Door;	
		}
		
		//------------------------------------------------------
		//----------------------Actions-------------------------
		//------------------------------------------------------	
		//++When taking the sheet
	void Action_TakeSheet() {
			StartCoroutine(DisablePaging(-1, 4.0F / Speed));
			UI.UITextOutput(6, 4.0F / Speed);
			Guide.GuideTextOutput(6, Speed);
			Inventory[0,1] = "yes";	
			Score[0] += 0;
			Score[1] += 1;
		}
		//++When taking the broken glass
	void Action_TakeGlass() {
			StartCoroutine(DisablePaging(-1, 2.0F / Speed));
			UI.UITextOutput(6, 2.0F / Speed);
			Guide.GuideTextOutput(14, Speed); 
			Inventory[1,1] = "yes";	
			Score[0] += 1;
			Score[1] += 1;		
		}
		//++When attacking the bed with the broken glass
	void Action_AttackBedWithGlass() {
			StartCoroutine(DisablePaging(-1, 2.0F / Speed));
			UI.UITextOutput(6, 2.0F / Speed);
			Guide.GuideTextOutput(10, Speed);	
			Score[0] += 1;
			Score[1] += 0;
		}
		//++When attacking the bed with the shiv
	void Action_AttackBedWithShiv() {
			StartCoroutine(DisablePaging(-1, 2.0F / Speed));
			UI.UITextOutput(6, 2.0F / Speed);
			Guide.GuideTextOutput(15, Speed);	
			Score[0] += 1;
			Score[1] += 0;
		}
		//++When making the shiv
	void Action_MakeShiv() {
			StartCoroutine(DisablePaging(-1, 2.5F / Speed));
			UI.UITextOutput(4, 2.5F / Speed);
			Guide.GuideTextOutput(17, Speed);
			Inventory[0,1] = "no";
			Inventory[1,1] = "no";
			Inventory[2,1] = "yes";	
			Score[0] += 1;
			Score[1] += 2;
		}
		//++When attacking the door bare handed
	void Action_AttackDoorWithFists() {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(11, 1.0F / Speed);
			Guide.GuideTextOutput(21, Speed);
			GuardFacing = "towards";	
			Score[0] += 3;
			Score[1] += 0;
		}
		//++When attacking the door with the broken glass
	void Action_AttackDoorWithGlass() {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(11, 1.0F / Speed);
			Guide.GuideTextOutput(22, Speed);	
			Score[0] += 2;
			Score[1] += 0;
		}
		//++When attacking the door with the shiv
	void Action_AttackDoorWithShiv() {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(11, 1.0F / Speed);
			Guide.GuideTextOutput(23, Speed);	
			Score[0] += 2;
			Score[1] += 0;
		}
		//++When yelling through the door
	void Action_YellThroughDoor() {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(11, 1.0F / Speed);
			Guide.GuideTextOutput(24, Speed);
			GuardFacing = "towards";	
			Score[0] += 1;
			Score[1] += 1;
		}
		//++When forcing the door and the guard is facing away
	void Action_ForceDoor_GuardFacingAway() {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(12, 5.0F / Speed);
			Guide.GuideTextOutput(25, Speed);
			CurrentState = States.Corridor;		
			Score[0] += 3;
			Score[1] += 0;
		}
		//++When forcing the door and the guard is facing towards
	void Action_ForceDoor_GuardFacingTowards() {
			StartCoroutine(DisablePaging(-1, 1.0F / Speed));
			UI.UITextOutput(13, 5.0F / Speed);
			Guide.GuideTextOutput(26, Speed);	
			CurrentState = States.Corridor;	
			Score[0] += 3;
			Score[1] += 0;
		}
		//++When trying the door handle and the guard is facing away
	void Action_TryLock_GuardFacingAway() {
			StartCoroutine(DisablePaging(-1, 3.5F / Speed));
			UI.UITextOutput(12, 5.0F / Speed);
			Guide.GuideTextOutput(25, Speed);
			CurrentState = States.Corridor;	
			Score[0] += 0;
			Score[1] += 3;
		}
		//++When trying the door handle and the guard is facing towards
	void Action_TryLock_GuardFacingTowards() {
			StartCoroutine(DisablePaging(-1, 3.5F / Speed));
			UI.UITextOutput(13, 5.0F / Speed);
			Guide.GuideTextOutput(26, Speed);
			CurrentState = States.Corridor;	
			Score[0] += 0;
			Score[1] += 3;
		}
		//-----FINAL STAGE ACTIONS, only one can be chosen per game
		//++When attacking the guard WITH the glass or SHIV
	void FinalAction_AttackGuardWithWeapon() {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(15, 1.0F);
			Guide.GuideTextOutput(27, Speed);
			CurrentState = States.Ending;	
			Score[0] += 3;
			Score[1] += 1;
		}
		//++When attacking the guard WITHOUT a weapon
	void FinalAction_AttackGuardWithoutWeapon() {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(15, 1.0F);
			Guide.GuideTextOutput(28, Speed);
			CurrentState = States.Ending;	
			Score[0] += 4;
			Score[1] += 0;	
		}
		//++When talking to the guard while he's facing away
	void FinalAction_Talk_GuardFacingAway() {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(15, 1.0F);
			Guide.GuideTextOutput(29, Speed);
			CurrentState = States.Ending;	
			Score[0] += 0;
			Score[1] += 1;
		}
		//++When talking to the guard when he's facing towards
	void FinalAction_Talk_GuardFacingTowards() {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(15, 1.0F);
			Guide.GuideTextOutput(30, Speed);
			CurrentState = States.Ending;	
			Score[0] += 0;
			Score[1] += 1;
		}
		//++When sneaking passed the guard
	void FinalAction_Sneak() {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(15, 1.0F);
			Guide.GuideTextOutput(31, Speed);
			CurrentState = States.Ending;	
			Score[0] += 0;
			Score[1] += 1;
		}	
		//++When running passed the guard
	void FinalAction_Run() {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(15, 1.0F);
			Guide.GuideTextOutput(32, Speed);
			CurrentState = States.Ending;	
			Score[0] += 0;
			Score[1] += 1;    
		}
		
		//------------------------------------------------------
		//---------------------ENDING PAGES---------------------
		//------------------------------------------------------
		//++Easter Egg Ending
	void Ending_EasterEgg() {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(16, 4.0F);
			Guide.GuideTextOutput(33, Speed);
			CurrentState = States.End;	
		}
		//++Ending 1 - Violent
	void Ending_Violent() {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(16, 4.0F);
			Guide.GuideTextOutput(34, Speed);
			CurrentState = States.End;	
		}
		//++Ending 2 - Smart
	void Ending_Smart() {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(16, 4.0F);
			Guide.GuideTextOutput(35, Speed);
			CurrentState = States.End;	
		}
		//++Ending 3 - Useless
	void Ending_Useless() {
			StartCoroutine(DisablePaging(4, 4.0F / Speed));
			UI.UITextOutput(16, 4.0F);
			Guide.GuideTextOutput(36, Speed);
			CurrentState = States.End;	
		}
		//++Restart game
	void Ending_Restart() {
			Application.LoadLevel(0);				
		}	
	#endregion
	}




