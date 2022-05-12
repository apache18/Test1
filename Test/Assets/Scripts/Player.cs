using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  

public class Player : MonoBehaviour
{
    float _speed = 2;

    float _hor, _ver;

    public Joystick joystick;

    public LayerMask layerMask;

    NavMeshAgent _meshAgent;

    GameObject _obj;

    BoxCollider _boxCollider;
   
    private void Start()
    {
        _meshAgent = GetComponent<NavMeshAgent>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        MoveWithFlag();
        MoveWithJoystick();
    }

    void MoveWithJoystick()
    {
        _ver = joystick.Vertical * _speed * Time.deltaTime;
        _hor = joystick.Horizontal * _speed * Time.deltaTime;

        if (_hor > 0 || _ver > 0 || _hor < 0 || _ver < 0)
        {
            _meshAgent.enabled = false;
            _boxCollider.enabled = true;
            Destroy(_obj);
            transform.Translate(new Vector3(_hor, 0, _ver));
        }
    }

    void MoveWithFlag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200, layerMask))
            {
                if (hit.collider == null) { return; }
                if (_obj) { Destroy(_obj); }

                _obj = Instantiate(Resources.Load("Point"), hit.point, Quaternion.identity) as GameObject;

                _meshAgent.enabled = true;
                _boxCollider.enabled = false;
                _meshAgent.SetDestination(hit.point);
            }
        }
    }
}

