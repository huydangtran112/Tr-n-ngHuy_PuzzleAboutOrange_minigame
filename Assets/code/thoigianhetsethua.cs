using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 45f;         // Thời gian đếm ngược
    public string sceneToLoad = "GameOver";  // Tên scene muốn chuyển khi hết giờ
    public string winScene = "WinScene";      // Tên scene khi người chơi thắng

    public TextMeshProUGUI countdownText;     // (Tuỳ chọn) Text UI để hiển thị thời gian

    private float timer;
    private bool hasEnded = false;

    void Start()
    {
        timer = countdownTime;
    }

    void Update()
    {
        if (hasEnded) return;

        timer -= Time.deltaTime;

        if (countdownText != null)
        {
            countdownText.text = Mathf.Ceil(timer).ToString();
        }

        // Khi hết thời gian thì chuyển scene
        if (timer <= 0)
        {
            hasEnded = true;
            SceneManager.LoadScene(6); // Chuyển đến scene khi hết giờ
        }
    }

    // Gọi hàm này khi người chơi thắng
    public void WinGame()
    {
        if (hasEnded) return;

        hasEnded = true;
        SceneManager.LoadScene(winScene); // Chuyển đến scene khi thắng
    }
}