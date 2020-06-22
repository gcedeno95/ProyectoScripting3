using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] VerySimplePistol[] allweapons;

    int currentWeapon;

    void Start()
    {
        allweapons = transform.GetComponentsInChildren<VerySimplePistol>();
        clearWeapons();
        allweapons[0].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && allweapons[0])
        {
            clearWeapons();
            allweapons[0].gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && allweapons[1])
        {
            clearWeapons();
            allweapons[1].gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && allweapons[2])
        {
            clearWeapons();
            allweapons[2].gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && allweapons[3])
        {
            clearWeapons();
            allweapons[3].gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && allweapons[4])
        {
            clearWeapons();
            allweapons[4].gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && allweapons[5])
        {
            clearWeapons();
            allweapons[5].gameObject.SetActive(true);
        }

        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScroll > 0)
        {
            currentWeapon = (currentWeapon + 1) % allweapons.Length;
            clearWeapons();
            allweapons[currentWeapon].gameObject.SetActive(true);
        }
        if (mouseScroll < 0)
        {
            currentWeapon--;
            if (currentWeapon < 0) currentWeapon = allweapons.Length - 1;
            clearWeapons();
            allweapons[currentWeapon].gameObject.SetActive(true);
        }
    }

    private void clearWeapons()
    {
        for (int i = 0; i < allweapons.Length; i++)
        {
            allweapons[i].gameObject.SetActive(false);
        }
    }
}
