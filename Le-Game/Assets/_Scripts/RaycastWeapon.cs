using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    bool isFire;
    [SerializeField] private TrailRenderer tracerEffect;
    [SerializeField] private ParticleSystem[] MuzzeleFlash;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private Transform Raycastorigin;
    [SerializeField] private Transform RaycastDestination;

    Ray ray;
    RaycastHit hitInfo;
   public void StarFiring()
    {
        isFire = true;

        foreach (var particle in MuzzeleFlash)
        {
                particle.Emit(1);
        }

        ray.origin = Raycastorigin.position;

        ray.direction = RaycastDestination.position-Raycastorigin.position;

        var tracer = Instantiate(tracerEffect,ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if(Physics.Raycast(ray, out hitInfo))
        {
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.blue, 1.0f);
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            tracer.transform.position = hitInfo.point;
            
        }
    }
    public void StopFiring()
    {
        isFire = false;
    }
}
