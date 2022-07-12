using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameObject car;
    public GameObject miniMapCam;
    public GameObject Player;

   
    // Start is called before the first frame update
    private void Update()
    {
        miniMapCam.transform.position = (new Vector3(car.transform.position.x, miniMapCam.transform.position.y, car.transform.position.z));

       Player.transform.position = (new Vector3(car.transform.position.x, Player.transform.position.y, car.transform.position.z));
    }


}
