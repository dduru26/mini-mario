using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

    private bool canDamage;

    void Awake () {
        canDamage = true;
    }

    void Start() {
        Time.timeScale = 1f;
    }

    public void DealDamage() {
        if (canDamage) {
            GameManager.Instance.TakeHit();   // lose a life AND respawn at checkpoint

            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage() {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

} // class