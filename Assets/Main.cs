using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public enum style
	{
		Search, 
		Sweep, 
		Find,
		hybrid
	};
	const int row = 100;
	const int column = 100; 
	const int numBlacks = 1000; 
	public GameObject blackTemplate; 
	GameObject[,] grid = new GameObject[column, row];
	BlackBehaviour[] blacks = new BlackBehaviour[numBlacks]; 
	public static style Style = style.Search;  
	// Use this for initialization
	void Start () {
		int count = 0; 
		for (int i = 0; i < row; i++)
		{
		    for (int x = 0; x < column; x++) 
			{
				count++; 
				var go = new GameObject();
				go.name = "GridSquare : " + x.ToString () + i.ToString (); 
				go.transform.parent = gameObject.transform; 
				go.transform.position = new Vector3(x, i, 0); 
				//go.AddComponent<SpriteRenderer> (); 
				//SpriteRenderer sr = go.GetComponent<SpriteRenderer> (); 
				//sr.sprite = Sprite.Create (Texture2D.whiteTexture, new Rect (0f, 0f, 4f, 4f), new Vector2 (0.5f, 0.5f), 100f);
				if (count % 7 == 0) 
				{
					go.tag = "Fill";
				}
				grid [x, i] = go; 

			}
		}
		for (int i = 0; i < numBlacks; i++) 
		{
			int randintx = Random.Range (0, column); 
			int randinty = Random.Range (0, 0); 
			GameObject temp = Instantiate (blackTemplate, grid [randintx, randinty].transform) as GameObject;
			blacks [i] = temp.GetComponent<BlackBehaviour> ();
			blacks[i].init(randintx, randinty, grid); 
		}
	}


	// Update is called once per frame
	void Update () 
	{
//		for (int i = 0; i < blacks.Length; i++) 
//		{
//			blacks [i].GetComponentInParent<transform.position> ();
//		}

	}
}
