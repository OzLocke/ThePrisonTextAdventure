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
	
	
	// Use this for initialization
	void Start () {
		CurrentState = States.Intro;	
		Page = 1;
		Inventory = new string[,] {
			{"Mouldy Sheet", "no"}
			,{"Broken Glass", "no"}
		};
	}
	
	// Update is called once per frame
	void Update () {
		
		//------------------------------------------------------
		//----------------------INTRO PAGES---------------------
		//------------------------------------------------------
		if(CurrentState == States.Intro & Page == 1) {
			StartCoroutine(DisablePaging(2, 6.0F));
			UI.UITextOutput(1);
			Guide.GuideTextOutput(1);
			
		} else if(Input.GetKeyUp(KeyCode.Space) & CurrentState == States.Intro & Page == 2) {
			StartCoroutine(DisablePaging(3, 9.0F));
			UI.UITextOutput(2);
			Guide.GuideTextOutput(2);
			
		} else if(Input.GetKeyUp(KeyCode.Space) & CurrentState == States.Intro & Page == 3) {
			StartCoroutine(DisablePaging(4, 6.0F));
			UI.UITextOutput(3);
			Guide.GuideTextOutput(3);
			
		} else if(Input.GetKeyUp(KeyCode.Space) & CurrentState == States.Intro & Page == 4) {
			StartCoroutine(DisablePaging(-1, 14.0F));
			UI.UITextOutput(4);
			Guide.GuideTextOutput(4);
			CurrentState = States.Cell;
			//------------------------------------------------------
			//--------------------IN-CELL STATES--------------------
			//------------------------------------------------------
		} else if(Input.GetKeyUp(KeyCode.S) & CurrentState == States.Cell & Page == -1 & Inventory[0,1] == "no") {
			StartCoroutine(DisablePaging(-1, 4.0F));
			UI.UITextOutput(5);
			Guide.GuideTextOutput(5);
			CurrentState = States.Bed;
		}
	}
	
	//-------FUNCTIONS-------
	IEnumerator DisablePaging(int NextPage, float WaitTime) {
		Page = 0;
		yield return new WaitForSeconds(WaitTime);
		Page = NextPage;
	}
}




