using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    [SerializeField] private bool isFiring;
    [SerializeField] private ParticleSystem[] MuzzeleFlash;
   public void StarFiring()
    {
        isFiring = true;
        foreach (var particle in MuzzeleFlash)
        {
            while (isFiring == true)
            {
                particle.Emit(1);
            }
        }
    }
    public void StopFiring()
    {
        isFiring = false;
    }
}
