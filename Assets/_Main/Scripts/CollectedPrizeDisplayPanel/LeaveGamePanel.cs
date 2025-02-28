using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CaseDemo.Scripts.CollectedPrizeDisplayPanel
{
    public class LeaveGamePanel : MonoBehaviour
    {
        [SerializeField] private Button leaveGameButton;
        [SerializeField] private Button continueGameButton;

        private void OnEnable()
        {
            leaveGameButton.onClick.AddListener(LeaveGame);
            continueGameButton.onClick.AddListener(ContinueGame);
        }
        private void OnDisable()
        {
            leaveGameButton.onClick.RemoveListener(LeaveGame);
            continueGameButton.onClick.RemoveListener(ContinueGame);
        }

        private void LeaveGame()
        {
            SceneManager.LoadSceneAsync("main_menu");
        }

        private void ContinueGame()
        {
            gameObject.SetActive(false);
        }
    }
}