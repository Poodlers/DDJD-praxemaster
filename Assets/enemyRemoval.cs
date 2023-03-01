using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRemoval : MonoBehaviour
{
    public void RemoveEntity()
    {
        Destroy(transform.parent.gameObject);
    }
}
