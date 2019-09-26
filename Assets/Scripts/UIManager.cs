using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _livesSprites;
    private Image _currentLivesSprite;
    private Text _scoreText;
    private Player _player;
    private bool _gameOver;
    [SerializeField] private GameObject _gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        _gameOver = false;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        _currentLivesSprite = GetComponentInChildren<Image>();
        UpdateScore();
        UpdateLives(3);
    }

    public void UpdateScore()
    {
        _scoreText.text = "Score: " + _player.GetScore();
    }

    public void UpdateLives(int currentLives)
    {
        _currentLivesSprite.sprite = _livesSprites[currentLives];
    }

    public void GameOver()
    {
        _gameOver = true;
        StartCoroutine(GameOverText());
    }

    private IEnumerator GameOverText()
    {
        while (_gameOver)
        {
            _gameOverText.SetActive(true);
            yield return new WaitForSeconds(1);
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(.3f);
        }
    }
}
