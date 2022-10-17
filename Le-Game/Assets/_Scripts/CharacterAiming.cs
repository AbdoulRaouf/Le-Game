using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    Camera mainCamera;
    float turnSpeed = 15f;
    float CamAngle;
    [SerializeField] Rig aimLayer;
    [SerializeField] float aimDuration = .3f;
    void Start()
    { 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
       
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, CamAngle,0), turnSpeed*Time.deltaTime);
    }
    private void Update()
    {
        CamAngle = mainCamera.transform.rotation.eulerAngles.y;
        if (Input.GetMouseButton(1))
        {
            aimLayer.weight += Time.deltaTime / aimDuration;
        }
        else
        {
            aimLayer.weight -= Time.deltaTime / aimDuration;
        }
    }
}
