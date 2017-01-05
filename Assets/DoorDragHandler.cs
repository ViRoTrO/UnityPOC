using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DoorDragHandler : drag {

	//snapping
	Vector3 snapVec;

	override protected void Awake()
	{
		base.Awake ();
		//snapVec = new Vector3 (0.0f, 0.00f, 0.6f);
		addSnapPoints();
	}

	void addSnapPoints()
	{
		
	}

	// Collision and snapping
	void OnTriggerEnter(Collider other) {

		if (other.transform.tag == "frame")
		{

//			if (!isDragging) {
//				//rend.material.color = Color.red;
//			} else if (!isSnapped) {
//				FrameDragHandler frameDrag = (FrameDragHandler)other.transform.GetComponent ("FrameDragHandler");
//				snapVec = frameDrag.nearestSnap (transform.position,new Vector3 (10000.0f, 0.0f, 0.0f), "door");
//
//				if (snapVec.x != 10000.0f) {
//					transform.parent = other.transform;
//					transform.localPosition = snapVec;
//					isSnapped = true;
//					lastParent = other.transform.gameObject;
//					lastPosition = snapVec;
//				}
//			}
//			else {
//				isColliding = true;
//				FrameDragHandler frameDrag1 = (FrameDragHandler)other.transform.GetComponent ("FrameDragHandler");
//				frameDrag1.rend.material.color = Color.red;
//
//				for (int i = 0; i < other.transform.childCount; i++) {
//					GameObject temp = (GameObject)other.transform.GetChild (i).gameObject;
//					temp.transform.GetComponent<Renderer> ().material.color = Color.red;
//
//				}
//			}
		}
	}

	public void changeTexture (string str)
	{
		Texture txt;
		if (str == "1") {
			txt = Resources.Load ("DR08_1") as Texture;
			rend.material.mainTexture = txt;
		}
		else if (str == "2") {
			txt = Resources.Load ("DR08_2") as Texture;
			rend.material.mainTexture = txt;
		} 
		else if (str == "3") {
			txt = Resources.Load ("DR08_3") as Texture;
			rend.material.mainTexture = txt;
		}

	}

	override protected void checkRayCast ()
	{
		if (!isSnapped) {
			base.checkRayCast ();
			return;
		}


	}
}
