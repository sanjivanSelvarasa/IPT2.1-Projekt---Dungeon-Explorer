using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpCollectScript : MonoBehaviour
{
    [SerializeField] private ChestOpenEvent[] chestOpenEvent;

    // value for power-ups
    public int healthPowerUp = 15;
    public int xpPowerUp = 15;

    // event sources
    private HealthScript _HealthScript;
    private XPScript _XPScript;

    // sprite animation closed and open
    private SpriteRenderer _SpriteRenderer;
    public Sprite closedSprite;
    public Sprite openSprite;

    // text appears on looting
    public TextMeshPro lootingInfoText;
    private void Start()
    {
        GameObject healthBar = GameObject.Find("Health");
        _HealthScript = healthBar.GetComponent<HealthScript>();

        GameObject XPScript = GameObject.Find("Experience");
        _XPScript = XPScript.GetComponent<XPScript>();

        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _SpriteRenderer.sprite = closedSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _SpriteRenderer.sprite == closedSprite)
        {
            _SpriteRenderer.sprite = openSprite;
            RandomEvent();
        }
    }

    private void RandomEvent()
    {
        float totalDropChance = 0f;
        foreach(ChestOpenEvent dropevent in chestOpenEvent)
        {
            totalDropChance += dropevent.DropChance;
        }

        float rand = Random.Range(0, totalDropChance);
        float cumulativeEvent = 0f;

        foreach (ChestOpenEvent dropevent in chestOpenEvent)
        {
            cumulativeEvent += dropevent.DropChance;
            if (rand <= cumulativeEvent)
            {
                dropevent.eventhappen.Invoke();
                return;
            }
        }
    }

    public void IncreasePlayerHealth()
    {
        int randNum = Random.Range(5, healthPowerUp);
        _HealthScript.IncreaseHealth(randNum);
        lootingInfoText.text = $"+{randNum} HP";
        lootingInfoText.gameObject.SetActive(true);

        Invoke(nameof(HideLootText), 2f);
    }

    public void IncreasePlayerXP()
    {
        int randNum = Random.Range(5, xpPowerUp);
        _XPScript.IncreaseSliderValue(randNum);
        lootingInfoText.text = $"+{randNum} XP";
        lootingInfoText.gameObject.SetActive(true);

        Invoke(nameof(HideLootText), 2f);
    }

    public void OneMoreUpgrade()
    {
        _XPScript.currUpgrades++;

        lootingInfoText.text = "+1 Upgrade";
        lootingInfoText.gameObject.SetActive(true);

        Invoke(nameof(HideLootText), 2f);
    }

    private void HideLootText()
    {
        lootingInfoText.gameObject.SetActive(false);
    }

    public void DetroyAllEnemies()
    {
        GameObject[] findAllPowerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        foreach (GameObject powerUp in findAllPowerUps)
        {
            Destroy(powerUp);
        }
    }

    [System.Serializable]
    public class ChestOpenEvent
    {
        public string eventName;
        [Range(0f, 1f)] public float DropChance = 0.5f;
        public UnityEvent eventhappen; 
    }
}
