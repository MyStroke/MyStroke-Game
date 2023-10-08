using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }
    public int scoreValue = 0;

    // import all files
    private API data;
    private Obstacle obj;
    private popup popupbox;
    private Countdown countdown;
    private RandomML randomML;
    private PlayerData plrdata;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public TextMeshProUGUI gameOverText;
    public Button retryButton;

    private Player player;
    private Spawner spawner;

    private float score;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        data = FindObjectOfType<API>();
        popupbox = FindObjectOfType<popup>();
        countdown = FindObjectOfType<Countdown>();
        randomML = FindObjectOfType<RandomML>();
        plrdata = FindObjectOfType<PlayerData>();

        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        UpdateHiscore();
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        popupbox.box.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHiscore();
        plrdata.SendProfileToServer(); // Update from WebGL
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = scoreValue.ToString("D2");
    }

    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (scoreValue > hiscore)
        {
            hiscore = scoreValue;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D2");
    }

    public void Objprocess()
    {
        gameSpeed = 0f;
        spawner.gameObject.SetActive(false);
        popupbox.popupbox();
        player.animator.Play("HeroKnight_Attack1");
    }

    public void DestroyObstacles()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

    }

    public void ContinueGame()
    {
        gameSpeed = initialGameSpeed;
        enabled = true;
        player.animator.Play("HeroKnight_Run");
        scoreValue += 1;
        DestroyObstacles();
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        popupbox.box.SetActive(false);
        countdown.TimeLeft = 10;
    }

}
