using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float shotForce = 0;
    [SerializeField] float shotCooldown = 0;
    public int ammo = 0;
    public int magazineCapacity = 0;
    private bool readyToShoot = true;
    readonly KeyCode reload = KeyCode.R;

    public GameObject bullet;
    public Transform shotPoint;
    PlayerCamera playerCamera;
    public GameEvent OnShot;

    private void Awake()
    {
        ammo = magazineCapacity;
    }

    private void Start()
    {
        playerCamera = FindObjectOfType<PlayerCamera>();
    }

    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(reload))
        {
            ReloadAmmo();
        }

        if (Input.GetMouseButtonDown(0) && readyToShoot)
        {
            // Handle shot
            SpawnBullet();
            readyToShoot = false;

            Invoke(nameof(ResetShoot), shotCooldown);

            // Handle ammo
            ammo--;
            OnShot.Raise(ammo);

            if (ammo <= 0)
            {
                // Reload animation during which you can't shoot
                ReloadAmmo();
            }
        }
    }

    void SpawnBullet()
    {
        // Get rotation of shotPoint
        Quaternion bulletRotation = shotPoint.rotation;
        
        // Spawn bullet
        Instantiate(bullet, shotPoint.position , bulletRotation);
    }

    void ResetShoot()
    {
        readyToShoot = true;
    }

    void ReloadAmmo()
    {
        ammo = magazineCapacity;
        //uiDisplay.UpdateAmmoDisplay();
    }
}
