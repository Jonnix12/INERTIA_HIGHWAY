using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private CheckPointSystem _checkPointSystem;
    private LineRenderer lineRenderer;

    public GameObject car;
    public GameObject miniMapCam;
    public GameObject Player;

   
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
       
        for (int x = 0; x <_checkPointSystem.CheckPointPosition.Count; x++)
        {
            lineRenderer.SetPosition(x,new Vector3(_checkPointSystem.CheckPointPosition[x].x,60,_checkPointSystem.CheckPointPosition[x].z));
        }
        lineRenderer.SetPosition(_checkPointSystem.CheckPointPosition.Count, lineRenderer.GetPosition(0));
         
        lineRenderer.startWidth = 9f;
        lineRenderer.endWidth = 9f;
    }

    private void Update()
    {
        miniMapCam.transform.position = (new Vector3(car.transform.position.x, miniMapCam.transform.position.y, car.transform.position.z));

       Player.transform.position = (new Vector3(car.transform.position.x, Player.transform.position.y, car.transform.position.z));
    }


}
