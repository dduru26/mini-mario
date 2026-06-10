using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    [SerializeField] private GameObject endPanel;   // the Game Over UI, hidden by default

    private Text lifeText;
    private int lifeCount = 3;

    private bool processingWater = false;
    private Vector3 lastSafePosition;
    private float respawnBackoff = 1.5f;

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

        if (endPanel != null) {
            endPanel.SetActive(false);   // make sure it's hidden at the start
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) {
            lastSafePosition = player.transform.position;
        }
    }

    public int GetLife() {
        return lifeCount;
    }

    public void SetCheckpoint(Vector3 pos) {
        lastSafePosition = pos;
    }

    public void PlayerEnteredWater(Vector3 playerPosition) {
        if (processingWater) return;
        processingWater = true;

        LoseLife();

        if (GetLife() > 0) {
            Respawn();
        } else {
            ShowEndScreen();
        }

        StartCoroutine(ResetWaterFlag());
    }

    void Respawn() {
        GameObject player = GameObject.FindWithTag("Player");
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        float facing = Mathf.Sign(player.transform.localScale.x);
        Vector3 respawnPos = lastSafePosition - new Vector3(facing * respawnBackoff, 0f, 0f);

        rb.linearVelocity = Vector2.zero;
        player.transform.position = respawnPos;
    }

    void ShowEndScreen() {
        if (endPanel != null) {
            endPanel.SetActive(true);
        }
        Time.timeScale = 0f;   // freeze the game behind the panel
    }

    // hooked to the Replay button
    public void Replay() {
        Time.timeScale = 1f;   // unfreeze before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // hooked to the Quit button
    public void Quit() {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;   // also stops Play mode in the editor
        #endif
    }

    IEnumerator ResetWaterFlag() {
        yield return new WaitForSeconds(0.5f);
        processingWater = false;
    }

    public void LoseLife() {
        lifeCount--;
        if (lifeCount < 0) lifeCount = 0;
        UpdateLifeUI();
    }

    void UpdateLifeUI() {
        if (lifeText != null) {
            lifeText.text = "x" + lifeCount;
        }
    }

} // class