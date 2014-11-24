using UnityEngine;
using System.Collections;

public class button_handler : MonoBehaviour {


	public Sprite buttonDownSprite,buttonUpSprite;

	public string buttonLable;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.GetChild(0).GetComponent<GUIText>().text = buttonLable;
	}

	void OnMouseDown(){

		gameObject.GetComponent<SpriteRenderer>().sprite = buttonDownSprite ;
	} 

	void OnMouseUp(){
		gameObject.GetComponent<SpriteRenderer>().sprite = buttonUpSprite ;
	}


}