using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  

public class Player : MonoBehaviour
{
    private float _speed = 2;

    float hor, ver;

    public Joystick joystick;

    public LayerMask layerMask;

    private NavMeshAgent meshAgent;

    GameObject obj;

    BoxCollider boxCollider;
   
    private void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        boxCollider = GetComponent<BoxCollider>();
    }    

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200, layerMask))
            {       
                if (hit.collider == null) { return; }
                if (obj) { Destroy(obj);}

                obj = Instantiate(Resources.Load("Point"), hit.point, Quaternion.identity) as GameObject;

                meshAgent.enabled = true;
                boxCollider.enabled = false;
                meshAgent.SetDestination(hit.point);
            }
        }
        else
        {
            ver = joystick.Vertical * _speed * Time.deltaTime;
            hor = joystick.Horizontal * _speed * Time.deltaTime;

            if (hor > 0 || ver > 0 || hor < 0 || ver < 0)
            {
                meshAgent.enabled = false;
                boxCollider.enabled = true;
                Destroy(obj);
                transform.Translate(new Vector3(hor, 0, ver));
            }
        }
    }
}
