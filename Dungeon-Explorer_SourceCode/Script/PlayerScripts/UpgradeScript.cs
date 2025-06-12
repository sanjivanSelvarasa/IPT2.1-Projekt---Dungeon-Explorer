using UnityEngine;
using static PowerUpCollectScript;

public class UpgradeScript : MonoBehaviour
{
    // reference Scripts
    private HealthScript _HealthScript;
    public EnemiesData _EnemiesData;
    public PowerUpCollectScript _PowerUpCollectScript;
    public EnemyHealthScript _EnemyHealthScript;
    public HitEnemyScript _HitEnemyScript;
    public HitboxRotateScript _HitboxRotateScript;
    public PlayerScript _playerScript;

    // look for available Upgrades
    public XPScript _XPScript;

    private void Start()
    {
        GameObject healthBar = GameObject.Find("Health");
        _HealthScript = healthBar.GetComponent<HealthScript>();

        GameObject XPScript = GameObject.Find("Experience");
        _XPScript = XPScript.GetComponent<XPScript>();
    }

    public void UpgradeHealth(int value)
    {
        if(_XPScript.currUpgrades > 0) { 
            _HealthScript.SetMaxHealth(value);
            _XPScript.currUpgrades--;
        }
    }

    public void UpgradeXPCollection(int value)
    {
        // _EnemyHealthScript.IncreaseXPValue(value);
    }

    public void UpgradeStrength(int value)
    {
        if (_XPScript.currUpgrades > 0)
        {
            _HitEnemyScript.damageValue += value;
            _XPScript.currUpgrades--;
        }
    }

    public void UpgradeRange(float value)
    {
        if (_XPScript.currUpgrades > 0)
        {
            Vector3 scale = _HitboxRotateScript.transform.localScale * value;
            _HitboxRotateScript.transform.localScale = scale;
            _XPScript.currUpgrades--;
        }
        
    }

    public void UpgradePlayerSpeed(float value)
    {
        if (_XPScript.currUpgrades > 0)
        {
            if (_playerScript.playerSpeed < 20f)
                _playerScript.playerSpeed += value;
            _XPScript.currUpgrades--;
        }
    }

}
