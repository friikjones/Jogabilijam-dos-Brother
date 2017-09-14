using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {
	
	public int cameraAngle;
	public int cameraZoom;
    public int cameraAngleIncrement;
	
	public int cameraRadius;
	private int cameraFieldofView;
	public int cameraZoomCap;
	public int cameraZoomFieldVariable;
	public int cameraZoomFieldBase;
	public int cameraPanVariable;
	private float centerX;
	private float calculatedX;
	private float centerZ;
	private float calculatedZ;
	private float centerY;
	private float calculatedY;
	
	private KeyCode zoomIn;
	private KeyCode zoomOut;
	private KeyCode rotateLeft;
	private KeyCode rotateRight;
	private KeyCode moveLeft;
	private KeyCode moveRight;
	private KeyCode moveFront;
	private KeyCode moveBack;
	private KeyCode moveUp;
	private KeyCode	moveDown;
	
	
	// Use this for initialization
	void Start () 
	{
		cameraAngle = 0;
        cameraAngleIncrement = 15;
		cameraRadius = 10;
		cameraZoomCap = 2;
	    cameraZoom = 1;
		
		cameraZoomFieldBase = 30;
		cameraZoomFieldVariable = 30;
		cameraPanVariable = 2;
		
		calculatedY = 10;

		zoomIn = KeyCode.Z;
		zoomOut = KeyCode.X;
		rotateLeft = KeyCode.Q;
		rotateRight = KeyCode.E;
		moveLeft = KeyCode.A;
		moveRight = KeyCode.D;
		moveFront = KeyCode.W;
		moveBack = KeyCode.S;
		moveUp = KeyCode.R;
		moveDown = KeyCode.F;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		cameraRefresh();
		cameraInput();
	}
	
	void cameraRefresh()
	{
		calculatedX = Mathf.Sin(cameraAngle * Mathf.PI/180) * cameraRadius * -1 + centerX;
		calculatedZ = Mathf.Cos(cameraAngle * Mathf.PI/180) * cameraRadius * -1 + centerZ;
		
		transform.position = new Vector3(calculatedX, calculatedY, calculatedZ);
		transform.eulerAngles = new Vector3(45, cameraAngle, 0);
		
		Camera.main.fieldOfView = (cameraZoom * cameraZoomFieldVariable) + cameraZoomFieldBase;
	}
	
	void cameraInput()
	{
		if (Input.GetKeyDown(zoomOut))
		{
			if (cameraZoom < cameraZoomCap)
			cameraZoom++;
		}
		
		if (Input.GetKeyDown(zoomIn))
		{
			if (cameraZoom > 0)
			cameraZoom--;
		}
		
		if (Input.GetKeyDown(rotateLeft))
		{
			cameraAngle = cameraAngle + cameraAngleIncrement;
		}
		
		if (Input.GetKeyDown(rotateRight))
		{
			cameraAngle = cameraAngle - cameraAngleIncrement;
		}
		
		if (Input.GetKeyDown(moveRight))
		{
			centerX = centerX + Mathf.Cos(cameraAngle * Mathf.PI/180) * cameraPanVariable;
			centerZ = centerZ - Mathf.Sin(cameraAngle * Mathf.PI/180) * cameraPanVariable;
		}
		
		if (Input.GetKeyDown(moveLeft))
		{
			centerX = centerX - Mathf.Cos(cameraAngle * Mathf.PI/180) * cameraPanVariable;
			centerZ = centerZ + Mathf.Sin(cameraAngle * Mathf.PI/180) * cameraPanVariable;
		}
		
		if (Input.GetKeyDown(moveFront))
		{
			centerX = centerX + Mathf.Sin(cameraAngle * Mathf.PI/180) * cameraPanVariable;
			centerZ = centerZ + Mathf.Cos(cameraAngle * Mathf.PI/180) * cameraPanVariable;
		}
		
		if (Input.GetKeyDown(moveBack))
		{
			centerX = centerX - Mathf.Sin(cameraAngle * Mathf.PI/180) * cameraPanVariable;
			centerZ = centerZ - Mathf.Cos(cameraAngle * Mathf.PI/180) * cameraPanVariable;
		}

		if (Input.GetKeyDown (moveUp)) 
		{
			calculatedY = calculatedY + 5;
		}

		if (Input.GetKeyDown (moveDown)) 
		{
			calculatedY = calculatedY - 5;
		}
	}
}
