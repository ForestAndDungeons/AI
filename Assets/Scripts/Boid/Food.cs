using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] GameObject _prefab;

    void Update()
    {
        foreach(var boid in GameManager.Instance.GetAllBoids())
        {
            if (Vector3.Distance(boid.transform.position, this.transform.position) <= 0.5f)
            {
                GameManager.Instance.RemoveFood(this.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}