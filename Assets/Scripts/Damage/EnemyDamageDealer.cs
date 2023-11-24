using System.Collections;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using UnityEngine;


public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
    bool hasDealtDamage;
    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;


    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = false;
    }
    void Update()
    {
        if (canDealDamage)
        {
            RaycastHit hit;
            int layerMask = 1 << 7;
            if (Physics.Raycast(transform.position, -transform.forward, out hit, -weaponLength, layerMask))
            {
                if (hit.transform.TryGetComponent(out PlayerState health))
                {
                    health.TakeDamage(weaponDamage);
                    hasDealtDamage = true;
                }
                else
                {
                    Debug.Log("Cannot Receive");
                }        
            }
        }
    }
    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage = false;
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.forward * weaponLength);
    }
}
