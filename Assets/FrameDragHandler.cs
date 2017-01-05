using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FrameDragHandler : drag {


	//snapping
	Vector3 snapVec;



	override protected void Awake()
	{
		base.Awake ();
		addSnapPoints ();
	}


	void addSnapPoints()
	{
		snapPoints.Add (new SnapInfo (new Vector3 (0.0f, 1.15f, 0.0f), "frame", false));
		snapPoints.Add (new SnapInfo (new Vector3 (1.8f, 0.0f, 0.0f), "frame", false));
		snapPoints.Add (new SnapInfo (new Vector3 (-1.8f, 0.0f, 0.0f), "frame", false));

		//snapPoints.Add (new SnapInfo (new Vector3 (0.0f, 2*1.15f, 0.0f), "frame", false));
		//snapPoints.Add (new SnapInfo (new Vector3 (0.0f, 3*1.15f, 0.0f), "frame", false));
		//snapPoints.Add (new SnapInfo (new Vector3 (0.0f, 4*1.15f, 0.0f), "frame", false));

		snapPoints.Add ( new SnapInfo(new Vector3 (0.0f, 0.00f, 0.6f), "door", false) );
	}

	void OnTriggerStay(Collider other)
	{
		if (other.transform.tag == "frame") {
			if (isColliding && isDragging) {
				transform.position = lastPosition;
			}
		}

	}

	// Collision and snapping
	void OnTriggerEnter(Collider other) {

		if(other.transform.tag == "door")
		{

        }
		else if(other.transform.tag == "frame")
		{
			isColliding = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		isColliding = false;
		if(other.transform.tag == "door") 
		{
			
		}
		else if(other.transform.tag == "frame")
		{
			if (!isDragging){
				rend.material.color = shaderColor;
				for (int i = 0; i < transform.childCount; i++) {
					GameObject temp = (GameObject)transform.GetChild (i).gameObject;
					FrameDragHandler frameDrag = (FrameDragHandler)temp.transform.GetComponent ("FrameDragHandler");
					frameDrag.rend.material.color = shaderColor;
				}
			}
			else
				isColliding = false;


		}
	}

	public void changeTexture (string str)
	{
		Texture txt; 
		if (str == "1") {
			txt = Resources.Load ("FR11_1") as Texture;
			rend.material.mainTexture = txt;
		} 
		else if (str == "2") {
			txt = Resources.Load ("FR11_2") as Texture;
			rend.material.mainTexture = txt;
		} 
		else if (str == "3") {
			txt = Resources.Load ("FR11_3") as Texture;
			rend.material.mainTexture = txt;
		}

	}


	override protected void checkRayCast ()
	{
		base.checkRayCast ();
        
        if (DragDummyObject.activeCollisions.Count != 0)
        {
            FrameDragHandler frameDragScript = null;
            float dist = 0.0f;
            float nearest = 0.0f;

            foreach (Collider coll in DragDummyObject.activeCollisions)
            {
                if (frameDragScript == null)
                {
                    frameDragScript = (FrameDragHandler)coll.transform.GetComponent("FrameDragHandler");
                    if (frameDragScript != null)
                        snapVec = frameDragScript.nearestSnap(worldPos, lastValidSnap, "frame");

                    dist = Vector3.Distance(snapVec, DragDummyObject.obj.transform.position);
                    nearest = dist;
                    continue;
                }


                Vector3 sanp = Vector3.zero;

                frameDragScript = (FrameDragHandler)coll.transform.GetComponent("FrameDragHandler");
                if (frameDragScript != null)
                    sanp = frameDragScript.nearestSnap(worldPos, lastValidSnap, "frame");

                dist = Vector3.Distance(sanp, DragDummyObject.obj.transform.position);

                if (dist < nearest)
                {
                    nearest = dist;
                    snapVec = sanp;
                }
            }

            if (snapVec.x != 10000.0f)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                frameDragScript.removeExistingSnap(lastValidSnap, "frame");
                frameDragScript.addSnap(snapVec, "frame");
                transform.parent = DragDummyObject.currentCollidedTransform.transform;
                transform.localPosition = snapVec;
                isSnapped = true;
                lastValidSnap = snapVec;
                lastPosition = transform.position;

                if (snapVec.x == 1.8f) // right
                {
                    addSnap(new Vector3(-1.8f, 0.0f, 0.0f), "frame");
                }
                else if (snapVec.x == -1.8f) // left
                {
                    addSnap(new Vector3(1.8f, 0.0f, 0.0f), "frame");
                }

                if (lastValidSnap.x == 1.8f) // right
                {
                    removeExistingSnap(new Vector3(-1.8f, 0.0f, 0.0f), "frame");
                }
                else if (lastValidSnap.x == -1.8f) // left
                {
                    removeExistingSnap(new Vector3(1.8f, 0.0f, 0.0f), "frame");
                }

                return;
            }

            DragDummyObject.visible(true);
            transform.position = lastPosition;
            transform.parent = lastParent;
        }
        else
        {
            transform.parent = rootParent;
            transform.position = worldPos;
            isSnapped = false;

        }
    }


}
