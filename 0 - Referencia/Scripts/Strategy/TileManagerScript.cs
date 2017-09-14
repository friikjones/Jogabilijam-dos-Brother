using UnityEngine;
using System.Collections;

public class TileManagerScript : MonoBehaviour {

	public int selectedId;
	public Vector3 vectorValue;
	private int children;

	// Use this for initialization
	void Start () {
	
		children = transform.childCount;
		TileIdSet();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public int TileCheck()
	{
		
		for (int i = 0; i < children; i++) 
		{
			if (transform.GetChild(i).GetComponent<HexTile> ().selected)
			{
			selectedId = i;
			return selectedId;
			}
			//Debug.Log ("counting "+i);
			
			
		}
		return 1000;
	}
	
	void TileIdSet()
	{
		
		for (int i = 0; i < children; i++) 
		{
			transform.GetChild(i).GetComponent<HexTile> ().tileId = i;
		}
	}
	
	public void TileClean()
	{
		
		for (int i = 0; i < children; i++) 
		{
			transform.GetChild(i).GetComponent<HexTile> ().selected = false;
		}
	}
	
	public Vector3 TilePosition(int target)
	{
		
		for (int i = 0; i < children; i++) 
		{
			if(transform.GetChild(i).GetComponent<HexTile> ().tileId == target)
			{
				vectorValue = transform.GetChild(i).position;
			}
		}
		return vectorValue;
	}
}
