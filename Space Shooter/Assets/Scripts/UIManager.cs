using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _GameOverText;

    [SerializeField]
    private Sprite[] _LivesSprites;

    [SerializeField]
    private Image _LivesImage;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private GameManager _GameManager;

    // Start is called before the first frame update
    void Start()
    {

        _GameOverText.SetActive(false);
        _scoreText.text = "SCORE: "+ 0;
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(_GameManager == null)
        {
            Debug.LogError("_GameManager is null");
        }
    }

   public void UpdateScore(int playerScore)
    {
        _scoreText.text = "SCORE: " + playerScore;
    }
    public void UpdateLives(int curLives)
    {
        _LivesImage.sprite = _LivesSprites[curLives];
        if(curLives <= 0)
        {
            this.GameOverSequence();
        }
    }

    public void GameOverSequence()
    {
        _GameManager.GameOver();
        _GameOverText.SetActive(true);
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            _GameOverText.SetActive(true);
           
            yield return new WaitForSeconds(.75f);
           _GameOverText.SetActive(false);
            
            yield return new WaitForSeconds(.5f);

        }   
    }
}
