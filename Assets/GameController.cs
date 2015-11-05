using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public UIController UI;
	public GuideController Guide;
	public enum States {Intro, Cell, Mirror, Bed, Door};
	public States CurrentState;
	int Page;
	string[,] Inventory;
	string[,] Visited;
	
	
	// Use this for initialization
	void Start () {
		CurrentState = States.Intro;	
		Page = 4;
		Inventory = new string[,] {
			{"Mouldy Sheet", "no"}
			,{"Broken Glass", "no"}
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
		if(CurrentState == States.Intro & Page == 1) {
			StartCoroutine(DisablePaging(2, 6.0F));
			UI.UITextOutput(1, 6.0F);
			Guide.GuideTextOutput(1);
		}	
		else if(Input.GetKeyUp(KeyCode.Space) & CurrentState == States.Intro & Page == 2) {
			StartCoroutine(DisablePaging(3, 9.0F));
			UI.UITextOutput(2, 9.5F);
			Guide.GuideTextOutput(2);
		}	
		else if(Input.GetKeyUp(KeyCode.Space) & CurrentState == States.Intro & Page == 3) {
			StartCoroutine(DisablePaging(4, 6.0F));
			UI.UITextOutput(3, 6.5F);
			Guide.GuideTextOutput(3);
		}
			
		
		//------------------------------------------------------
		//--------------------Initial Visits--------------------
		//------------------------------------------------------
		//++Initial introduction to the cell
		else if(Input.GetKeyUp(KeyCode.Space) & CurrentState == States.Intro & Page == 4) {
			StartCoroutine(DisablePaging(-1, 14.0F));
			UI.UITextOutput(4, 14.0F);
			Guide.GuideTextOutput(4);
			CurrentState = States.Cell;
			Visited[0,1] = "yes";
		} 
		//++Initial introduction to the bed
		else if(Input.GetKeyUp(KeyCode.B) & Visited[1,1] == "no") {
			StartCoroutine(DisablePaging(-1, 4.0F));
			UI.UITextOutput(5, 4.0F);
			Guide.GuideTextOutput(5);
			CurrentState = States.Bed;
			Visited[1,1] = "yes";
		}
			
		//------------------------------------------------------
		//-------------------Further Visits---------------------
		//------------------------------------------------------		 
		//++When returning to the cell
		else if((Input.GetKeyUp(KeyCode.Space) | Input.GetKeyUp(KeyCode.R)) & Visited[0,1] == "yes") {
				StartCoroutine(DisablePaging(-1, 1.0F));
				UI.UITextOutput(4, 1.0F);
				Guide.GuideTextOutput(7);
				CurrentState = States.Cell;
		} 
		//++When returning to the bed WITH the sheet in inventory
		else if(Input.GetKeyUp(KeyCode.B) & Visited[1,1] == "yes" & Inventory[0,1] == "yes") {
			StartCoroutine(DisablePaging(-1, 1.5F));
			UI.UITextOutput(6, 1.5F);
			Guide.GuideTextOutput(8);
			CurrentState = States.Bed;
		} 
		//++When returning to the bed WITHOUT the sheet in inventory
		else if(Input.GetKeyUp(KeyCode.B) & Visited[1,1] == "yes" & Inventory[0,1] == "no") {
			StartCoroutine(DisablePaging(-1, 1.0F));
			UI.UITextOutput(5, 1.0F);
			Guide.GuideTextOutput(9);
			CurrentState = States.Bed;
		}
		//------------------------------------------------------
		//----------------------Actions-------------------------
		//------------------------------------------------------	
		//++When taking the sheet
		else if(Input.GetKeyUp(KeyCode.T) & CurrentState == States.Bed & Inventory[0,1] == "no") {
			StartCoroutine(DisablePaging(-1, 4.0F));
			UI.UITextOutput(6, 4.0F);
			Guide.GuideTextOutput(6);
			Inventory[0,1] = "yes";	
		}
	}
	
	//-------FUNCTIONS-------
	IEnumerator DisablePaging(int NextPage, float WaitTime) {
		Page = 0;
		yield return new WaitForSeconds(WaitTime);
		Page = NextPage;
	}
}




