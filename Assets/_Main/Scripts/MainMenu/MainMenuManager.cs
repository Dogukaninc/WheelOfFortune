using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CaseDemo.Scripts.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Button startGameButton;

        private void OnEnable()
        {
            startGameButton.onClick.AddListener(StartGame);
        }

        private void OnDisable()
        {
            startGameButton.onClick.AddListener(StartGame);
        }
        
        private void StartGame()
        {
            SceneManager.LoadSceneAsync("scene_demo");
        }
    }
}