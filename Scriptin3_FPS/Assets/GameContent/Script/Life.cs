using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] float maxLife = 100;
    [SerializeField] bool isPlayer;
    [SerializeField] GameObject manekin;

    private float currentLife;

    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(float damage)
    {
        currentLife -= damage;
        if (this.gameObject.tag == "Player")
            UI_Manager.Instance.UpdateHealth(currentLife, maxLife);

        if (currentLife <= 0)
        {
            if (!isPlayer)
            {
                GameObject myManekin = Instantiate(manekin, transform.position, transform.rotation);
                Destroy(myManekin, 3);
                UI_Manager.Instance.UpdateScore(100);
                Destroy(gameObject);
            }
            else
            {
                GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.SetCursorLock(false);
                UI_Manager.Instance.showDeath();
            }
        }
    }
}
