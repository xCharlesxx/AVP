using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 


public class Main : MonoBehaviour {
	public enum style
	{
		Search, 
		Sweep, 
		Find,
		hybrid
	};
    const int GridHeight = 150;
    const int GridWidth = 500;
    const int numBlacks = 10000;
    [Range(1,3)]
    public int FileNumber = 1; 
    public GameObject blackTemplate; 
	GameObject[,] grid = new GameObject[GridWidth, GridHeight];
	BlackBehaviour[] blacks = new BlackBehaviour[numBlacks]; 
	public static style Style = style.Search;  

	// Use this for initialization
	void Start () {
		int count = 0;
        bool[,] pixelImage = GetValuesFromFile("..\\AVP\\LevelMaker\\LevelMaker\\Levels\\New Game\\"+ FileNumber.ToString());
        for (int i = 0; i < GridHeight; i++)
		{
		    for (int x = 0; x < GridWidth; x++) 
			{
				count++; 
				var go = new GameObject();
				go.name = "GridSquare : " + x.ToString () + i.ToString (); 
				//go.transform.parent = gameObject.transform; 
				go.transform.position = new Vector3(x, i, 0); 
				//go.AddComponent<SpriteRenderer> (); 
				//SpriteRenderer sr = go.GetComponent<SpriteRenderer> (); 
				//sr.sprite = Sprite.Create (Texture2D.whiteTexture, new Rect (0f, 0f, 4f, 4f), new Vector2 (0.5f, 0.5f), 100f);
				if (pixelImage[x, i] == true) 
				{
					go.tag = "Fill";
				}
				grid [x, i] = go; 
			}
		}
		//SET WHERE THEY SPAWN
		for (int i = 0; i < numBlacks; i++) 
		{
			int randintx = Random.Range (GridWidth-1, GridWidth-1); 
			int randinty = Random.Range (0, GridHeight); 
			GameObject temp = Instantiate (blackTemplate, grid [randintx, randinty].transform) as GameObject;
			blacks [i] = temp.GetComponent<BlackBehaviour> ();
			blacks[i].init(randintx, randinty, grid); 
		}
	}

	bool[,] GetValuesFromFile(string fileLocation)
    {
        bool[,] pixelImage = new bool[GridWidth, GridHeight];
        StreamReader file = new StreamReader(fileLocation);
		int x = 0; 
		int y = 0; 
        while (!file.EndOfStream)
        {
            string line = file.ReadLine();
			foreach (char c in line) 
			{				
				if (c == '0')
					pixelImage [x, y] = false;
				else
					pixelImage [x, y] = true; 
				x++; 
			}
			x = 0; 
			y++; 
        }

        file.Close();
        return pixelImage;
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
