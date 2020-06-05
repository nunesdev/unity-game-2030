using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam3Player : MonoBehaviour
{
    public GameObject head;
    public GameObject[] positions;
    private int indexCam = 0;
    public float MovimentSpeed = 2;
    private RaycastHit hit;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("c") && indexCam < (positions.Length -1)) {
            indexCam++;
        } else if(Input.GetKeyDown("c") && indexCam >= (positions.Length - 1)) {
            indexCam = 0;
        }
    }

    // Chamada em tempo fixo. 30 por x segundo (manter pressionado, movimentos, etc...)
    void LateUpdate()
    {
        transform.LookAt(head.transform);
        
        //CHEGAR SE TEM COLISOR
        if(!Physics.Linecast(head.transform.position, positions[indexCam].transform.position)) { 
            transform.position = Vector3.Lerp(transform.position, positions[indexCam].transform.position,MovimentSpeed * Time.deltaTime);
            Debug.DrawLine(head.transform.position, positions[indexCam].transform.position);
        } else if(Physics.Linecast(head.transform.position, positions[indexCam].transform.position, out hit)) {
            transform.position = Vector3.Lerp(transform.position, hit.point, (MovimentSpeed * 2) * Time.deltaTime);
            Debug.DrawLine(head.transform.position, hit.point);
        }
    }
}
