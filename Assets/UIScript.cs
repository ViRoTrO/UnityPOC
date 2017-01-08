using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	public GameObject frame;
	public GameObject door;

	public static GameObject currentSelection;
    public static Vector3 currentSelectionPosition;
    public static bool showDelete;

    Button btn_1;
	Button btn_2;
	Button btn_3;
	Button btn_4;
	Button btn_5;
	Button btn_6;

	Button zoomIn;
	Button zoomOut;
    Texture deleteTexture;

    void Start()
    {

        //		if (btn_1.onClick) {
        //			frameTexture = "1";
        //		} 
        //		else if (btn_2.onClick) {
        //			frameTexture = "2";
        //		} 
        //		else if (btn_3.onClick) {
        //			frameTexture = "3";
        //		}

        btn_1 = GameObject.Find("btn_1").GetComponent<Button>();
        btn_2 = GameObject.Find("btn_2").GetComponent<Button>();
        btn_3 = GameObject.Find("btn_3").GetComponent<Button>();
        btn_5 = GameObject.Find("btn_5").GetComponent<Button>();
        btn_6 = GameObject.Find("btn_6").GetComponent<Button>();


        btn_1.onClick.AddListener(clicked_1);
        btn_2.onClick.AddListener(clicked_2);
        btn_3.onClick.AddListener(clicked_3);
        btn_5.onClick.AddListener(createFrame);
        btn_6.onClick.AddListener(createDoor);

        deleteTexture = Resources.Load("delete") as Texture;

    }

	private void clicked_1(){
		if (currentSelection.transform.tag == "frame") {
			FrameDragHandler frameDrag = (FrameDragHandler)currentSelection.GetComponent ("FrameDragHandler");
			frameDrag.changeTexture ("1");
		} 
		else 
		{
			DoorDragHandler doorDrag = (DoorDragHandler)currentSelection.GetComponent ("DoorDragHandler");
			doorDrag.changeTexture ("1");
		}

	}

	private void clicked_2(){
		if (currentSelection.transform.tag == "frame") {
			FrameDragHandler frameDrag = (FrameDragHandler)currentSelection.GetComponent ("FrameDragHandler");
			frameDrag.changeTexture ("2");
		} 
		else 
		{
			DoorDragHandler doorDrag = (DoorDragHandler)currentSelection.GetComponent ("DoorDragHandler");
			doorDrag.changeTexture ("2");
		}
	}

	private void clicked_3(){
		if (currentSelection.transform.tag == "frame") {
			FrameDragHandler frameDrag = (FrameDragHandler)currentSelection.GetComponent ("FrameDragHandler");
			frameDrag.changeTexture ("3");
		} 
		else 
		{
			DoorDragHandler doorDrag = (DoorDragHandler)currentSelection.GetComponent ("DoorDragHandler");
			doorDrag.changeTexture ("3");
		}
	}

	private void createFrame(){
		Instantiate (frame);
	}

	private void createDoor(){
		Instantiate (door);

	}

	// Use this for initialization
	void OnGUI () {

        //		btn_1 = GameObject.find("btn_1");
        //		btn_2 = GameObject.find("btn_2");
        //		btn_3 = GameObject.find("btn_3");

        if (!showDelete)
            return;

        Vector3 pos = Camera.main.WorldToScreenPoint(currentSelectionPosition);
        pos.x = pos.x - 12.5f;
        pos.y = (Screen.height - pos.y) - 55;

        Rect rect = new Rect(pos.x, pos.y, 25, 25);
        

        if (GUI.Button(rect, deleteTexture, GUIStyle.none))
        {
            if (currentSelection != null)
            {
                Destroy(currentSelection);
                currentSelection = null;
            }

            showDelete = false;
        }

    }
	

}
