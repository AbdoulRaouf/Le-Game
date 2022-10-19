using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    float turnSpeed = 15f;
    float CamAngle;
    [SerializeField] float aimDuration = .3f;
    
    
    [SerializeField] Rig aimLayer;
    Camera mainCamera;
    RaycastWeapon weapon;
    void Start()
    { 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
        weapon=GetComponentInChildren<RaycastWeapon>();
    }

    void FixedUpdate()
    {
       
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, CamAngle,0), turnSpeed*Time.deltaTime);
    }
    private void Update()
    {
        CamAngle = mainCamera.transform.rotation.eulerAngles.y;
    }
    private void LateUpdate()
    {
        if (aimLayer)
        {
            if (Input.GetButton("Fire2"))
            {
                aimLayer.weight += Time.deltaTime / aimDuration;
            }
            else
            {
                aimLayer.weight -= Time.deltaTime / aimDuration;
            }
        }

        if (Input.GetButtonDown("Fire1") && Input.GetButton("Fire2")){
            weapon.StarFiring();
        }
        if (weapon.isFire)
        {
            weapon.UpdateFiring(Time.deltaTime);
        }
        weapon.UpdateBullets(Time.deltaTime);
        if (Input.GetButtonUp("Fire1"))
        {
            weapon.StopFiring();
        }
    }
}
