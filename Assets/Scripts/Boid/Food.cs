using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    void Update()
    {
        foreach(var boid in GameManager.Instance.GetAllBoids())
        {
            if (Vector3.Distance(boid.transform.position, this.transform.position) <= 1f)
            {
                Destroy(this.gameObject);
                GameManager.Instance.RemoveFood(this.gameObject);
            }
        }
    }
}