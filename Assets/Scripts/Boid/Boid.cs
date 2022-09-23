using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [Header("Classes")]
    BoidSteering _steering;
    BoidMovement _movement;

    BoidSeparation _separation;
    [SerializeField][Range(0f, 3f)] float _separationWeight = 1;

    BoidCohesion _cohesion;
    [SerializeField][Range(0f, 2f)] float _cohesionWeight = 1;

    BoidAlignment _alignment;
    [SerializeField][Range(0f, 2f)] float _alignmentWeight = 1;

    BoidArrive _arrive;
    [SerializeField][Range(0f, 2f)] float _arriveWeight = 1;

    BoidEvade _evade;
    [SerializeField][Range(0f, 2f)] float _evadeWeight = 1;

    [Header("Variables")]
    [SerializeField] float _maxSpeed;
    [SerializeField] float _maxForce;
    Vector3 _velocity;

    [Header("Radius")]
    [SerializeField] float _viewRadius;
    [SerializeField] UnityEngine.Color _viewColor;

    [SerializeField] float _separationRadius;
    [SerializeField] UnityEngine.Color _separationColor;

    void Start()
    {
        GameManager.Instance.AddBoid(this);
        _steering = new BoidSteering(this);
        _movement = new BoidMovement(this);
        _separation = new BoidSeparation(this);
        _cohesion = new BoidCohesion(this);
        _alignment = new BoidAlignment(this);
        _arrive = new BoidArrive(this);
        _evade = new BoidEvade(this);

    }

    void Update()
    {
        _velocity = _movement.GetVelocity();

        _movement.AddForce(_separation.Separation() * _separationWeight);
        _movement.AddForce(_cohesion.Cohesion() * _cohesionWeight);
        _movement.AddForce(_alignment.Alignment() * _alignmentWeight);
        _movement.AddForce(_arrive.Arrive() * _arriveWeight);
        _movement.AddForce(_evade.Evade() * _evadeWeight);
        _movement.Movement();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = _separationColor;
        Gizmos.DrawWireSphere(transform.position, _separationRadius);

        Gizmos.color = _viewColor;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
    }

    public BoidSteering GetSteering() { return _steering; }
    public Vector3 GetVelocity() { return _velocity; }
    public float GetMaxSpeed() { return _maxSpeed; }
    public float GetMaxForce() { return _maxForce; }
    public float GetViewRadius() { return _viewRadius; }
    public float GetSeparationRedius() { return _separationRadius; }
}