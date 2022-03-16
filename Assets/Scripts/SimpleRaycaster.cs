using Creatures;
using UnityEngine;

public class SimpleRaycaster : MonoBehaviour, IDamageDealer
{
    public float Damage { get; set; }
    
    public LayerMask RaycastOnly;
    public float MaxRaycastDistance = 200;
    private RaycastHit _hit;


    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, MaxRaycastDistance, RaycastOnly))
        {
            DealDamage(_hit.transform.GetComponent<IDamageReceiver>());
        }
        
    }

    public void DealDamage(IDamageReceiver damageReceiver)
    {
        damageReceiver.DealDamage(Damage);
    }
}