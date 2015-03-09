using UnityEngine;
using System.Collections;

public class ItemPickupOnClick : MonoBehaviour {

	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if(hit.collider.gameObject.GetComponent<ItemDataComponent>() != null)
                    hit.collider.gameObject.GetComponent<ItemDataComponent>().Pickup(true);
            }
        }
	}
}
