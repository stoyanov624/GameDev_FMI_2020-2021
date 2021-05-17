using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour {

    [SerializeField] Transform firePoint;
    [SerializeField] LineRenderer laser;
    public static Action onShootingAction;

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            onShootingAction?.Invoke();
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot() {

        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        if (hitInfo) {
            EnemyScript enemy = hitInfo.transform.GetComponent<EnemyScript>();
            if (enemy != null) {
                StartCoroutine(enemy.Die());
            }

            laser.SetPosition(0, firePoint.position);
            laser.SetPosition(1, hitInfo.point);
        } else {
            laser.SetPosition(0, firePoint.position);
            laser.SetPosition(1, firePoint.position + firePoint.right * 100f);
        }

        laser.enabled = true;

        yield return 0;

        laser.enabled = false;
    
    }
}
