using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;   // singleton so other scripts can reach it

    private Text lifeText;
    private int lifeCount = 3;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
    }

    void Start() {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        UpdateLifeUI();
    }

    public int GetLife() {
        return lifeCount;
    }
    
    public void PlayerEnteredWater(Vector3 playerPosition) {
        LoseLife();
        Debug.Log("Player entered water. Life now: " + GetLife());   // temporary
}
    public void LoseLife() {
        lifeCount--;
        if (lifeCount < 0) lifeCount = 0;
        UpdateLifeUI();
    }

    void UpdateLifeUI() {
        if (lifeText != null) {
            lifeText.text = "x" + lifeCount;   // matches the existing "x3" format
        }
    }

} // class