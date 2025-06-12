using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class EditModeTests
{
    private GameObject healthObj;
    private HealthScript healthScript;
    private UnityEngine.UI.Slider slider;

    [SetUp]
    public void SetUp()
    {
        healthObj = new GameObject("Health");
        slider = healthObj.AddComponent<UnityEngine.UI.Slider>();
        healthScript = healthObj.AddComponent<HealthScript>();
        GameObject textObj = new GameObject("ValueText");
        textObj.transform.SetParent(healthObj.transform);
    }


    [UnityTest]
    public IEnumerator PlayerMovementTest()
    {
        GameObject fakePlayer = new GameObject();
        Rigidbody2D rb = fakePlayer.AddComponent<Rigidbody2D>();
        PlayerScript playerScript = fakePlayer.AddComponent<PlayerScript>();

        playerScript.Start();
        playerScript.xAxis = 1;
        playerScript.yAxis = 0;

        playerScript.FixedUpdate();

        Assert.AreEqual(playerScript.playerSpeed, rb.linearVelocity.x, 0.01f);
        Assert.AreEqual(0f, rb.linearVelocity.y, 0.01f);
        yield return null;
    }

    [UnityTest]
    public IEnumerator UpgradeTesting()
    {
        GameObject healthObj = new GameObject();
        HealthScript _HealthScript = healthObj.AddComponent<HealthScript>();

        GameObject xpObj = new GameObject();
        XPScript _XPScript = xpObj.AddComponent<XPScript>();

        GameObject UpgradeObj = new GameObject();
        UpgradeScript _UpgradeScript = UpgradeObj.AddComponent<UpgradeScript>();

        _XPScript.currUpgrades = 1;

        int newHealth = 200;
        _HealthScript.SetMaxHealth(200);

        yield return null;

        _UpgradeScript.UpgradeHealth(newHealth);
        Assert.AreEqual(newHealth, _HealthScript.GetMaxHealth()); 
    }

    [UnityTest]
    public IEnumerator IncreaseHealthTest()
    {
        healthScript.SetHealth(20);
        healthScript.IncreaseHealth(30);
        yield return null;
        Assert.AreEqual(50, slider.value);
    }

    [UnityTest]
    public IEnumerator DecreaseHealthTest()
    {
        healthScript.SetHealth(70);
        healthScript.DecreaseHealth(30);
        yield return null;
        Assert.AreEqual(40, slider.value);
    }

    [UnityTest]
    public IEnumerator SetHealthTest()
    {
        healthScript.SetHealth(55);
        yield return null;
        Assert.AreEqual(55, slider.value);
    }

    [UnityTest]
    public IEnumerator SetMaxHealthTest()
    {
        healthScript.SetHealth(40);
        healthScript.SetMaxHealth();
        yield return null;
        Assert.AreEqual(100, slider.value);
    }

    [UnityTest]
    public IEnumerator SetMaxHealthTestWithParam()
    {
        healthScript.SetHealth(60);
        healthScript.SetMaxHealth(50);
        yield return null;
        Assert.AreEqual(60, slider.value);
        Assert.AreEqual(150, slider.maxValue);
    }

    [Test]
    public void GetMaxHealthTest_Return()
    {
        var result = healthScript.GetMaxHealth();
        Assert.AreEqual(100, result);
    }
}
