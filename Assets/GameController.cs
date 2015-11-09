using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public UIController UI;
	public GuideController Guide;
	public enum States {Intro, Cell, Mirror, Bed, Door};
	public States CurrentState;
	int Page;
	float Speed;
	string[,] Inventory;
	string[,] Visited;
	
	
	// Use this for initialization
	void Start () {
		Speed = 2.0F;
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
		//++Initial introduction to the door WITHOUT the broken glass & WITHOUT the sheet
		
		//++Initial introduction to the door WITH the broken glass & WITH the sheet
		
		//++Initial introduction to the door WITHOUT the broken glass & WITH the sheet
		
		//++Initial introduction to the door WITH the broken glass & WITHOUT the sheet
		
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
		//++Initial introduction to the door WITHOUT the broken glass & WITHOUT the sheet
		
		//++Initial introduction to the door WITH the broken glass & WITH the sheet
		
		//++Initial introduction to the door WITHOUT the broken glass & WITH the sheet
		
		//++Initial introduction to the door WITH the broken glass & WITHOUT the sheet
		
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
		}
		//++When taking the broken glass
		else if(Input.GetKeyUp(KeyCode.T) &
		        CurrentState == States.Mirror &
		        Inventory[1,1] == "no") {
			StartCoroutine(DisablePaging(-1, 2.0F / Speed));
			UI.UITextOutput(6, 2.0F / Speed);
			Guide.GuideTextOutput(14, Speed); 
			Inventory[1,1] = "yes";		
		}
		//++When attacking the bed with the broken glass
		else if(Input.GetKeyUp(KeyCode.A) & 
		        CurrentState == States.Bed & 
		        Inventory[1,1] == "yes") {
			StartCoroutine(DisablePaging(-1, 2.0F / Speed));
			UI.UITextOutput(6, 2.0F / Speed);
			Guide.GuideTextOutput(10, Speed);
		}
		//++When attacking the bed with the shiv
		else if(Input.GetKeyUp(KeyCode.A) & 
		        CurrentState == States.Bed & 
		        Inventory[2,1] == "yes") {
			StartCoroutine(DisablePaging(-1, 2.0F / Speed));
			UI.UITextOutput(6, 2.0F / Speed);
			Guide.GuideTextOutput(15, Speed);
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
					
		}
		
	}
	
	//-------FUNCTIONS-------
	IEnumerator DisablePaging(int NextPage, float WaitTime) {
		Page = 0;
		yield return new WaitForSeconds(WaitTime);
		Page = NextPage;
	}
}




