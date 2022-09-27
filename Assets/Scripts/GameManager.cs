using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    static GameManager _instance;

    [Header("Entities")]
    List<Boid> _allBoids = new List<Boid>();
    [SerializeField] Hunter _hunter;

    [Header("Bounds")]
    [SerializeField] float _boundWidth;
    [SerializeField] float _boundHeight;
    [SerializeField] Color _color;

    [Header("Food")]
    List<GameObject> _allFood = new List<GameObject>();
    float _foodTimer;
    [SerializeField] float _foodSpawnTime;
    [SerializeField] GameObject _foodPrefab;

    [Header("Waypoints")]
    [SerializeField] GameObject _waypointPrefab;
    [SerializeField] List<GameObject> _waypoints = new List<GameObject>();
    int _randomWaypoints;


    //Player playerReference;

    void Awake()
    {
        _randomWaypoints = Random.Range(1, 6);
        if (Instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            SpawnWaypoint();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }



    void Update()
    {
        _foodTimer -= Time.deltaTime;

        if(_foodTimer <= 0.0f)
        {
            SpawnFood();
            _foodTimer = _foodSpawnTime;
        }

        if (_waypoints.Count <= _randomWaypoints)
        {
            SpawnWaypoint();
        }
    }

    public Vector3 ApplyBounds(Vector3 objectPosition)
    {
        if (objectPosition.x > _boundWidth)
            objectPosition.x = -_boundWidth;
        else if (objectPosition.x < -_boundWidth)
            objectPosition.x = _boundWidth;

        if (objectPosition.z > _boundHeight)
            objectPosition.z = -_boundHeight;
        else if (objectPosition.z < -_boundHeight)
            objectPosition.z = _boundHeight;

        return objectPosition;
    }
    void SpawnFood()
    {
        GameObject food = Instantiate(_foodPrefab);

        Vector3 pos = new Vector3(Random.Range(-_boundWidth, _boundWidth), 0, Random.Range(-_boundHeight, _boundHeight));

        food.transform.position = pos;
        _allFood.Add(food);
    }
    public void SpawnWaypoint()
    {
        GameObject waypoint = Instantiate(_waypointPrefab);

        Vector3 pos = new Vector3(Random.Range(-_boundWidth, _boundWidth), 0, Random.Range(-_boundHeight, _boundHeight));

        waypoint.transform.position = pos;
        _waypoints.Add(waypoint);

    }
    void OnDrawGizmos()
    {
        Gizmos.color = _color;
        
        Vector3 topLeft = new Vector3(-_boundWidth, 0, _boundHeight);
        Vector3 topRight = new Vector3(_boundWidth, 0, _boundHeight);
        Vector3 botLeft = new Vector3(-_boundWidth, 0, -_boundHeight);
        Vector3 botRight = new Vector3(_boundWidth, 0, -_boundHeight);
        
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, botRight);
        Gizmos.DrawLine(botRight, botLeft);
        Gizmos.DrawLine(botLeft, topLeft);

    }

    public Hunter GetHunter() { return _hunter; }

    public List<GameObject> GetAllWaypoints() { return _waypoints; }
    public List<Boid> GetAllBoids() { return _allBoids; }
    public List<GameObject> GetAllFood() { return _allFood; }
    public void AddBoid(Boid b)
    {
        if (!_allBoids.Contains(b))
            _allBoids.Add(b);
    }
    public void RemoveBoid(Boid b)
    {
        if (_allBoids.Contains(b)) { 
            _allBoids.Remove(b);
            _hunter.SetTarget(null);
        }
    }
    public void RemoveFood(GameObject f)
    {
        if (_allFood.Contains(f))
            _allFood.Remove(f);
    }

    public void RemoveWaypoint(GameObject w)
    {
        if (_waypoints.Contains(w))
        {
            _waypoints.Remove(w);
            Destroy(w.gameObject);
        }
    }
}