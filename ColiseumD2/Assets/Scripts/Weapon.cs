using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coliseum
{

    public class Weapon : MonoBehaviour
    {
        public float attackDamage;
        public float attackRange;
        public float cooldown;
        public string weapon;
        public float knockback;

        // Start is called before the first frame update
        void Start()
        {
            AssignStat();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void AssignStat()
        {
            switch (weapon)
            {
                case "marteau":
                    attackDamage = 12;
                    attackRange = 3;
                    cooldown = 15;
                    knockback = 10;
                    break;

                case "lance":
                    attackDamage = 6;
                    attackRange = 6;
                    cooldown = 5;
                    knockback = 7.5f;
                    break;

                case "lame":
                    attackDamage = 3;
                    attackRange = 1;
                    cooldown = 2;
                    knockback = 5;
                    break;

                case "claymore":
                    attackDamage = 10;
                    attackRange = 4;
                    cooldown = 8;
                    knockback = 10;
                    break;

                case "":
                    attackDamage = 1;
                    attackRange = 1;
                    cooldown = 1;
                    knockback = 3;
                    break;
            }
        }
    }
}