using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _moveSpeed;

    [HideInInspector]
    public GameObject target;

    [SerializeField]
    private float _moveDuration;

    [SerializeField]
    private float _moveDelay;

    private Vector3 _currentTargetPos, _startPos;

    private float _timer;
    private float _distanceToTarget;

    private EntityStats _targetStats;

    private EntityStats _entityStats;

    void Start()
    {
        _entityStats = GetComponent<EntityStats>();
        if(target != null)
        {
            _targetStats = target.GetComponent<EntityStats>();
        }
    }

    public void Update()
    {
        _distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if(_distanceToTarget <= _entityStats.AttackRange)
        {
            _entityStats.BasicAttack(_targetStats, _entityStats.AttackDamage);
        }
    }

    void FixedUpdate()
    {
        DoMovement();
    }

    public void DoMovement()
    {
        _timer += 1 * Time.deltaTime;
        if (_timer > _moveDelay)
        {
            _startPos = transform.position;
            _currentTargetPos = target.transform.position;
            _timer = 0;
        } else 
        {
            Vector3 direction = _currentTargetPos - transform.position;
            direction.Normalize();
            transform.Translate(direction * Time.deltaTime);
        }
    }
}
