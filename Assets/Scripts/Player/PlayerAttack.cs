using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerAttack : MonoBehaviour
{
    public GameObject Weapon;
    public float AttackRange = 1.5f;

    [Range(1, 100)]
    public float AttackDamge = 10f;
    public LayerMask AttackLayers;

    [SerializeField]
    private TextMeshProUGUI promptText;

    private InputManger inputManger;

    void Start() {
        inputManger = GetComponent<InputManger>();
    }

    void Update()
    {
        if (inputManger.onFoot.Hit.triggered)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (Weapon != null)
        {
            promptText.text = "Player attacks!";
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, AttackRange, transform.forward, 0f, AttackLayers);
            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    promptText.text = "Hit enemy: " + hit.collider.name;
                    hit.collider.GetComponent<EnemyHealth>().TakeDamage(AttackDamge);
                }
            }
        }
        else
        {
            promptText.text = "No weapon equipped!";
        }
    }
}
