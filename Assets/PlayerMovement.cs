using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;


public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField] List<Transform> _wayMoveList = new List<Transform>();
    [SerializeField] int _wayPoint = 0;
    [SerializeField] Transform _target, _gun;
    [SerializeField] int _speed = 5;
    [SerializeField] GameObject _bullet;
    bool _isFire = false;
    Rigidbody _rigi;
    void Start()
    {
        //CountinueMove();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _rigi = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerDetect();
    }
    void PlayerMove()
    {
        _navMeshAgent.destination = _wayMoveList[_wayPoint].position;
        //if (Vector3.Distance(this.transform.position, _wayMoveList[_wayPoint].transform.position) < 0.2f)
        //{
        //    LookAtTarget();
        //    _wayPoint++;
        //    if (_wayPoint == _wayMoveList.Count)
        //    {
        //        _wayPoint = 0;
        //    }
        //}


        if (Vector3.Distance(this.transform.position, _wayMoveList[_wayPoint].transform.position) < 0.2f)
        {
            LookAtTarget();
        }
    }
    void LookAtTarget()
    {
        Vector3 lookPos = _target.transform.position - this.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
        float rot = Mathf.Atan2(lookPos.z, lookPos.x) * Mathf.Rad2Deg;
        //Debug.Log("Goc Xoay: " + (100 - this.transform.rotation.y * Mathf.Rad2Deg - rot));
        //if (100 - this.transform.rotation.y * Mathf.Rad2Deg - rot < 2)
        //{
        //    _wayPoint++;
        //    if (_wayPoint >= _wayMoveList.Count)
        //    {
        //        _wayPoint = 0;
        //    }
        //}
        if (_isEnemy)
        {
            _wayPoint++;
            if (_wayPoint >= _wayMoveList.Count)
            {
                _wayPoint = 0;
            }
        }
    }

    [SerializeField] int _length = 100;
    [SerializeField] bool _isEnemy = false;
    public GameObject _hitObj;
    private Ray _forwardRay;
    void PlayerDetect()
    {
        var Ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(Ray, out hit, _length))
        {
            _hitObj = hit.transform.gameObject;
        }
        if (_hitObj.transform.gameObject.CompareTag("Boss"))
        {
            _isEnemy = true;
        }
        else
        {
            _isEnemy = false;
        }
        _forwardRay = new Ray(transform.position, transform.TransformDirection(Vector3.forward) * _length);
        Debug.DrawRay(_forwardRay.origin, _forwardRay.direction * _length, Color.red);

    }



    void fire()
    {
        if (_isFire)
        {
            Instantiate(_bullet, _gun.transform.position, _gun.transform.rotation);
        }
        _isFire = false;
    }

}
