using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersScript : MonoBehaviour
{
    [SerializeField] GameObject myTower;

    public void AddTower(WayPoint wayPoint)
    {
        var newTower = Instantiate(myTower, wayPoint.transform.position, Quaternion.identity);
        newTower.transform.parent = this.transform;
        wayPoint.isPlaceable = false;
    }
}
