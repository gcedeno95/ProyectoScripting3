using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIcotryTrigger : MonoBehaviour
{
    [SerializeField] UI_Manager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>())
        {
            manager.showVictory();
        }
    }
}
