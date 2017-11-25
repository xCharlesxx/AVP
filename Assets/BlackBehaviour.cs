using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlackBehaviour : MonoBehaviour {
 
	float speed = 1f; 
	GameObject[,] gridref; 
	GameObject prevPos; 
	Vector3 averageLocationPos = Vector3.zero; 
	public void init(int X, int Y, GameObject[,] GR)
	{
		transform.position = new Vector3 (X, Y);  
		gridref = GR; 
		prevPos = new GameObject (); 
		averageLocationPos = getAverageLocationPos (); 
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float changeX = 0; 
		float changeY = 0; 
		//speed = Mathf.Round(Random.Range (1, 10)) / 10; 
		if (transform.localPosition.x > 0.01)
			changeX -= speed;  
		if (transform.localPosition.x < -0.01)
			changeX += speed;  
		if (transform.localPosition.y > 0.01)
			changeY -= speed;  
		if (transform.localPosition.y < -0.01)
			changeY += speed;  
		transform.position = new Vector3 (transform.position.x + changeX, transform.position.y + changeY, 0); 


		if (changeX == 0 && changeY == 0 && transform.parent.tag != "Fill")
			switch (Main.Style) 
			{
		case Main.style.Search: 
			move (); 
			break; 
			case Main.style.Sweep:  
			sweep (); 
			break; 
			case Main.style.hybrid: 
			break; 
			}
	}
		
	void sweep()
	{
		int posx = Mathf.RoundToInt (transform.parent.position.x); 
		int posy = Mathf.RoundToInt (transform.parent.position.y); 
		//Up
		if (posy != gridref.GetLength (1) - 1)
		if (gridref [posx, posy + 1].transform.childCount == 0)
		{
			transform.SetParent (gridref [posx, posy + 1].transform);
			return; 
		}
		else if (gridref [posx, posy + 1].transform.childCount == 1 && gridref [posx, posy + 1].tag == "Fill")
		{
			transform.SetParent (gridref [posx, posy + 2].transform);
			return; 
		}
		//Left
		if (posx != 0)
		if (gridref [posx - 1, posy].transform.childCount == 0)
		{
			transform.SetParent (gridref [posx - 1, posy].transform );
			return; 
		}
		//Right
		if (posx != gridref.GetLength (0) - 1)
		if (gridref [posx + 1, posy].transform.childCount == 0)
		{
			transform.SetParent (gridref [posx + 1, posy].transform);
			return; 
		}
		//Down
		if (posy != 0)
		if (gridref [posx, posy - 1].transform.childCount == 0)
		{
			transform.SetParent (gridref [posx, posy - 1].transform);
			return; 
		}
	}
	void find()
	{
		//if (!(gridref [posx - 1, posy].transform.childCount == 1 && gridref [posx - 1, posy].tag == "Fill"))
	}

	void move()
	{
		int posx = Mathf.RoundToInt (transform.parent.position.x); 
		int posy = Mathf.RoundToInt (transform.parent.position.y); 
		List<System.Action> methodList = new List<System.Action>(); 
		methodList.Add (() => Left (posx, posy));
		methodList.Add (() => Right (posx, posy));
		//methodList.Add (() => Down (posx, posy));
		methodList.Add (() => Up (posx, posy));
		methodList.Add (() => Up (posx, posy));
		methodList.Add (() => Up (posx, posy));
		methodList.Add (() => Up (posx, posy));

		//prevPos.transform.parent = gridref [posx, posy].transform; 
		for (int i = 0; i < methodList.Count; i++) 
		{
			int randpos = Random.Range (i, methodList.Count);
			System.Action temp = methodList [i]; 
			methodList[i] = methodList[randpos];
			methodList [randpos] = temp; 
		}
		for (int i = 0; i < methodList.Count; i++)
			methodList [i] (); 
	}


	void Left(int posx, int posy)
	{
		//Left
		if (posx != 0)
		if (gridref [posx - 1, posy].transform.childCount == 0) 
		{
			transform.SetParent (gridref [posx - 1, posy].transform);
			return; 
		}
	}

	void Right(int posx, int posy)
	{
		//Right
		if (posx != gridref.GetLength (0) - 1)
		if (gridref [posx + 1, posy].transform.childCount == 0) 
		{
			transform.SetParent (gridref [posx + 1, posy].transform);
			return; 
		}
	}

	void Up(int posx, int posy)
	{
		//Up
		if (posy != gridref.GetLength (1) - 1)
		{
			if (gridref [posx, posy + 1].transform.childCount == 0) 
			{
				transform.SetParent (gridref [posx, posy + 1].transform);
				return; 
			}
			else if (gridref [posx, posy + 1].transform.childCount == 1 && gridref [posx, posy + 1].tag == "Fill")
			{
				if (posy != gridref.GetLength (1) - 2)
				transform.SetParent (gridref [posx, posy + 2].transform);
				return; 
			}
		}
	}
	void Down(int posx, int posy)
	{
		//Down
		if (posy != 0)
		if (gridref [posx, posy - 1].transform.childCount == 0) 
		{
			transform.SetParent (gridref [posx, posy - 1].transform);
			return; 
		}
	}
	Vector3 getAverageLocationPos()
	{
		int num = 0; 
		Vector3 averagepos = Vector3.zero; 
		for (int i = 0; i < gridref.GetLength (1); i++) {
			for (int x = 0; x < gridref.GetLength (0); x++) {
				if (gridref [x, i].tag == "Fill" && gridref [x, i].transform.childCount == 0) 
				{
					averagepos += gridref [x, i].transform.position; 
					num++; 
				}
			}
		}
		averageLocationPos = averagepos / num; 
		return averagepos / num; 
	}
}
