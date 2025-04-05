using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    // Start is called before the first frame update
    
    public TextMeshProUGUI scoreText;
    public Slider healthBar;
    public GameObject losePanel;
    public GameObject nextLevelButton;
    
    void Start()
    {
        healthBar.maxValue = SkillManager.instance.player.health;
    }

    void Update()
    {
        scoreText.SetText("Score: " + GameManager.instance.score.ToString());
        healthBar.value = SkillManager.instance.player.health;
    }

    public void Lose()
    {
        losePanel.SetActive(true);
    }

    public void Win()
    {
        nextLevelButton.SetActive(true);
    }
}
