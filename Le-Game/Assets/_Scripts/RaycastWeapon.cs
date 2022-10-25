using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }

    [SerializeField] public bool isFire;
    [SerializeField] private int fireRate = 25;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDrop = 0.0f;
    [SerializeField] private float accumulationTime;
    [SerializeField] float fireinterval;
    float maxlifetime = 3.0f;

    [SerializeField] private TrailRenderer tracerEffect;
    [SerializeField] private ParticleSystem[] MuzzeleFlash;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private Transform Raycastorigin;
    [SerializeField] private Transform RaycastDestination;

    Ray ray;
    RaycastHit hitInfo;
    List<Bullet> Bullets= new List<Bullet> ();

    Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition)+ (bullet.initialVelocity*bullet.time)+(0.5f*gravity*bullet.time*bullet.time);
    }
    Bullet CreateBullet(Vector3 Position, Vector3 Velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = Position;
        bullet.initialVelocity = Velocity;
        bullet.time = 0.0f;
        bullet.tracer=Instantiate(tracerEffect,Position,Quaternion.identity);
        bullet.tracer.AddPosition(Position);
        return bullet;
    }

   public void StarFiring()
    {
        isFire = true;
        accumulationTime = 0.0f;
        fireBullet();
    }

    public void UpdateFiring(float deltatime)
    {
        accumulationTime+=deltatime;
        fireinterval = 1.0f / fireRate;
        
        while(accumulationTime >= 0.0f)
        {
            fireBullet();
            
            accumulationTime -=fireinterval;
        }
        
    }
    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }
    void SimulateBullets(float deltaTime)
    {
        Bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }
    private void DestroyBullets()
    {
        Bullets.RemoveAll(bullet => bullet.time >= maxlifetime);
    }
    void RaycastSegment(Vector3 star, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - star;
        float distance = direction.magnitude;
        ray.origin = star;
        ray.direction = direction;
        if (Physics.Raycast(ray, out hitInfo, distance))
        {

            //Debug.DrawLine(ray.origin, hitInfo.point, Color.blue, 1.0f);
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            bullet.tracer.transform.position = hitInfo.point;
            bullet.time = maxlifetime;
        }
        else
        {
            bullet.tracer.transform.position = end;
            //bullet.time = maxlifetime;
        }
    }
   
    private void fireBullet()
    {
        foreach (var particle in MuzzeleFlash)
        {
            particle.Emit(1);
        }

        Vector3 velocity = (RaycastDestination.position-Raycastorigin.position).normalized*bulletSpeed;
        var bullet = CreateBullet(Raycastorigin.position, velocity);
        Bullets.Add(bullet);
        
    }

    public void StopFiring()
    {
        isFire = false;
    }
}
