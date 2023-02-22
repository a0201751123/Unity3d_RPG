using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
    {
    public class PlayerController : MonoBehaviour
    {
        Health health;
        GameObject player;
        private void Start()
        {
           health = GetComponent<Health>();
           player = GameObject.FindWithTag("Player");
        }
        // Update is called once per frame
        private void Update()
        {
            if (health.IsDead()) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
           RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                if (Input.GetMouseButton(0) && target.gameObject != player)
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hitPos;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hitPos);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hitPos.point, 1f);
                }
                return true;
            }
            return false;

        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

