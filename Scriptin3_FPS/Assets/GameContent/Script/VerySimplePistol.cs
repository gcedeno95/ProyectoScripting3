using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    Pistol,
    Shotgun,
    MachineGun,
    RocketLauncher,
    GranadeLauncher
}
public class VerySimplePistol : MonoBehaviour
{
    public Transform m_raycastSpot;
    public float m_damage = 80.0f;
    [Range(0, 1000)]
    public float m_forceToApply = 500.0f;
    public float m_weaponRange = 9999.0f;
    public float m_maxClip = 10;
    public float m_fireRate = 2;
    public float m_reloadTime = 1;
    public float m_accuracyDropPerShot = 10;
    public float m_accuracy = 100;
    public float m_accuracyRecoveredPerSecond = 20;
    public float m_recoilBack;
    public float m_recoilRecovery;
    public float m_BulletsPerShot;
    [Space(10)]
    public Texture2D m_crosshairTexture;
    public AudioClip m_fireSound;
    public AudioClip m_reloadSound;
    [Space(10)]
    public WeaponType myWeaponType;
    public GameObject m_rocket;
    public Transform m_rocketSpot;
    public GameObject m_bloodParticle;

    float bulletsShot;
    private bool m_canShot;
    [SerializeField] GameObject m_weapon;

    private float m_clip;
    private GameObject rocketToShoot;
    private float m_timeBtwShots;
    private float m_currentAccuracy;

    private void Start()
    {
        m_clip = m_maxClip;
        m_timeBtwShots = 0;
        bulletsShot = 0;
        m_currentAccuracy = m_accuracy;
        m_canShot = true;
        if (myWeaponType == WeaponType.RocketLauncher)
        {
            rocketToShoot = Instantiate(m_rocket, m_rocket.transform.position, m_rocket.transform.rotation);
            rocketToShoot.transform.parent = m_weapon.transform;
            m_rocket.SetActive(false);
        }

        UI_Manager.Instance.UpdateAmmo(m_clip);
    }

    private void Update()
    {
        m_canShot = (myWeaponType == WeaponType.MachineGun) ? true : m_canShot;


        if (m_canShot && m_timeBtwShots >= 1 / m_fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                if (myWeaponType == WeaponType.RocketLauncher || myWeaponType == WeaponType.GranadeLauncher)
                {
                    ShotRocket();
                }
                else
                {
                    Shot();
                }
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            m_canShot = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        m_timeBtwShots += Time.deltaTime;
        m_currentAccuracy = Mathf.Lerp(m_currentAccuracy, m_accuracy, m_accuracyRecoveredPerSecond * Time.deltaTime);
        m_weapon.transform.position = Vector3.Lerp(m_weapon.transform.position, transform.position, m_recoilRecovery * Time.deltaTime);
    }

    private void OnGUI()
    {
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Rect auxRect = new Rect(center.x - 20, center.y - 20, 20, 20);
        GUI.DrawTexture(auxRect, m_crosshairTexture, ScaleMode.StretchToFill);
    }


    private void Shot()
    {
        if (m_clip > 0)
        {
            m_canShot = false;
            m_clip--;
            UI_Manager.Instance.UpdateAmmo(m_clip);
            m_timeBtwShots = 0;

            float accuracyModifier = (100 - m_currentAccuracy) / 100;
            Vector3 directionForward = m_raycastSpot.forward;
            directionForward.x += Random.Range(-accuracyModifier, accuracyModifier);
            directionForward.y += Random.Range(-accuracyModifier, accuracyModifier);
            directionForward.z += Random.Range(-accuracyModifier, accuracyModifier);
            m_currentAccuracy -= m_accuracyDropPerShot;
            m_currentAccuracy = Mathf.Clamp(m_currentAccuracy, 0, 100);

            Ray ray = new Ray(m_raycastSpot.position, directionForward);

            RaycastHit hit;
            Debug.DrawRay(m_raycastSpot.position, ray.direction, Color.green, 10);

            if (Physics.Raycast(ray, out hit, m_weaponRange))
            {
                if (hit.transform.gameObject.GetComponent<Life>())
                {
                    hit.transform.gameObject.GetComponent<Life>().TakeDamage(m_damage);
                    GameObject blood = Instantiate(m_bloodParticle, hit.transform);
                    Destroy(blood, 1);
                }
                    
                   
                if (hit.rigidbody && !hit.transform.gameObject.GetComponent<EnemyBase>())
                    hit.rigidbody.AddForce(ray.direction * m_forceToApply);
            }

            m_weapon.transform.Translate(new Vector3(0, 0, m_recoilBack), Space.Self);

            GetComponent<AudioSource>().PlayOneShot(m_fireSound);

            bulletsShot++;
            if (bulletsShot < m_BulletsPerShot)
            {
                Shot();
            }
            else
            {
                bulletsShot = 0;
            }
        }

    }

    private void ShotRocket()
    {
        if (m_clip <= 0)
        {
            return;
        }

        m_timeBtwShots = 0;
        m_canShot = false;

        m_clip--;
        UI_Manager.Instance.UpdateAmmo(m_clip);
        GetComponent<AudioSource>().PlayOneShot(m_fireSound);
        if (myWeaponType == WeaponType.RocketLauncher)
        {
            rocketToShoot.GetComponent<RocketScript>().canShoot = true;
            rocketToShoot.GetComponent<Rigidbody>().useGravity = true;
            rocketToShoot.transform.parent = null;
            rocketToShoot = null;
        }
        else if (myWeaponType == WeaponType.GranadeLauncher)
        {
            GameObject proj = Instantiate(m_rocket, m_rocketSpot.position, m_rocketSpot.rotation) as GameObject;
            proj.GetComponent<Rigidbody>().velocity = transform.forward * 15 + transform.up * 3;
        }

        bulletsShot++;
        if (bulletsShot < m_BulletsPerShot)
        {
            ShotRocket();
        }
        else
        {
            bulletsShot = 0;
        }
    }

    private void Reload()
    {
        transform.parent.GetComponent<Animator>().speed = 1 / m_reloadTime;
        transform.parent.GetComponent<Animator>().SetTrigger("Reload");

        if (myWeaponType == WeaponType.RocketLauncher)
        {
            if (rocketToShoot == null)
            {
                rocketToShoot = Instantiate(m_rocket, m_rocket.transform.position, m_rocket.transform.rotation);
                rocketToShoot.transform.parent = m_weapon.transform;
                rocketToShoot.GetComponent<Rigidbody>().useGravity = false;
                rocketToShoot.SetActive(true);
            }
        }
        m_timeBtwShots -= m_reloadTime;
        m_clip = m_maxClip;
        UI_Manager.Instance.UpdateAmmo(m_clip);
    }
}
