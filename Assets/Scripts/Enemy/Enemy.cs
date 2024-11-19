using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CombatManager combatmanager;
    public EnemySpawner spawner;
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (spawner != null && combatmanager != null)
        {
            spawner.OnEnemyKilled();
            combatmanager.OnEnemyKilled();
        }
    }
}
