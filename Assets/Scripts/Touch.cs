using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                var rig = raycastHit.collider.GetComponent<WayPoint>();

                if (rig != null && rig.isPlaceable)
                {
                    FindObjectOfType<TowersScript>().AddTower(rig);
                }
                
            }
        }
    }

    public void ExitProgramm()
    {
        Application.Quit();
    }
}
