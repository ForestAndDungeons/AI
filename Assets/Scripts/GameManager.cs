using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    static GameManager _instance;

    [Header("Boids")]
    List<Boid> _allBoids = new List<Boid>();

    [Header("Bounds")]
    [SerializeField] float _boundWidth;
    [SerializeField] float _boundHeight;
    [SerializeField] Color _color;

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

    public void AddBoid(Boid b)
    {
        if (!_allBoids.Contains(b))
            _allBoids.Add(b);
    }
    public List<Boid> GetAllBoids()
    {
        return _allBoids;
    }
}