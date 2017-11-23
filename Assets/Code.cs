using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code : MonoBehaviour {
	const int row = 10;
	const int column = 10; 
	const int numBlacks = 10; 
	GameObject[,] grid = new GameObject[column, row];
	GameObject[] blacks = new GameObject[numBlacks]; 
	// Use this for initialization
	void Start () {
		for (int i = 0; i < row; i++)
		{
		    for (int x = 0; x < column; x++) 
			{
				var go = new GameObject();
				go.name = "GridSquare : " + x.ToString () + i.ToString (); 
				go.transform.parent = gameObject.transform; 
				go.transform.position = new Vector3(x, i, 0); 
				grid [x, i] = go; 
			}
		}
		for (int i = 0; i < numBlacks; i++) 
		{
			GameObject black = new BlackBehaviour ();  
			black.transform.position = grid[i, 0].transform.position; 
			blacks [i] = black; 
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
