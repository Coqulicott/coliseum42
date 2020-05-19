using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Coliseum
{
    public class PlayerAttack : MonoBehaviour
    {
        private PhotonView photonView;
        
        public GameObject Hand1;
        public GameObject Hand2;
        public Weapon myWeapon;
        public Animator anim;
        private playerMove EnemyKb;
        private float Timer;
        private int c;
        private PlayerHealth pH;

        void Start()
        {
            photonView = GetComponent<PhotonView>();
            myWeapon = Hand1.GetComponentInChildren<Weapon>();
            anim = GetComponent<Animator>();
            pH = GetComponent<PlayerHealth>();
        }

        void Update()
        {
            if (photonView.IsMine)
            {
                Timer += Time.deltaTime;
                Debug.DrawRay(Hand1.transform.position, transform.forward * myWeapon.attackRange);
                Debug.DrawRay(Hand2.transform.position, transform.forward * myWeapon.attackRange);
                if (Input.GetMouseButtonDown(0) && Timer > myWeapon.cooldown && pH.shield)
                {
                    Timer = 0f;
                    DoAttack();
                    c++;
                }

                anim.SetBool("leftClick", Input.GetMouseButton(0));
            }
        }

        private void DoAttack()
        {
            float damage = myWeapon.attackDamage;

            if (c > 2)
            {
                damage *= 1.5f;
                c = 0;
            }

            /*
            Ray ray1 = new Ray(Hand1.transform.position, transform.forward);
            Ray ray2 = new Ray(Hand2.transform.position, transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray1, out hit, myWeapon.attackRange) || Physics.Raycast(ray2, out hit, myWeapon.attackRange))
            {
                if(hit.collider.CompareTag("Player"))
                {
                    PlayerHealth eHealth = hit.collider.GetComponent<PlayerHealth>();
                    
                    
                    EnemyKb = hit.collider.GetComponent<playerMove>();
                    Vector3 knockback = Hand1.transform.forward * myWeapon.knockback;
                    knockback.y = 0;
                    
                    hit.transform.GetComponent().RPC ("Damage", damage);
                    
                    if (!eHealth.shield)
                    {
                        eHealth.TakeDamage(damage);
                        EnemyKb.knockback = knockback;
                    }
                }
            }*/
            
            Ray ray1 = new Ray(Hand1.transform.position, transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray1, out hit, myWeapon.attackRange))
            {
                if(hit.collider.CompareTag("Player"))
                {
                    RpcTarget target = hit.collider.GetComponent<RpcTarget>();
                    PhotonView enemyView = hit.transform.GetComponent<PhotonView>();
                    PlayerHealth enemyHealth = hit.transform.GetComponent<PlayerHealth>();
                    if (enemyView)
                    {
                        /*if (enemyHealth.bonus)
                        {
                            enemyView.RPC("Effect", target);
                        }*/
                        enemyView.RPC("Damage", target, damage);
                        //if(!enemyHealth.bonus)
                        enemyView.RPC("Knockback", target,(Hand1.transform.forward * myWeapon.knockback));
                    }
                }
            }
        }
    }
}