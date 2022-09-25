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

    //Player playerReference;

    void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
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
}