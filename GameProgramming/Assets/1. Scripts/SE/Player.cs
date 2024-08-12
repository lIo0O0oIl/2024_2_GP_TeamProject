using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
            {
                Debug.Log(hit.collider.name);
            }

    /*        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool result = Physics.Raycast(ray, out hit, mainCam.farClipPlane);

            if (result)
            {
                hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
            }*/
        }
    }
}
