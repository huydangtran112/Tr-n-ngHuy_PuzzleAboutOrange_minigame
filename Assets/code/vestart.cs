using UnityEngine;
using UnityEngine.SceneManagement;

public class vestart : MonoBehaviour
{
    public static SceneHistoryManager Instance;
    public void QuayVeMenu()
    {
        SceneManager.LoadScene("start"); // Đổi tên theo đúng tên scene menu của bạn
    }
    public void GoBack()
    {
        SceneHistoryManager.Instance.GoBack();
    }
}