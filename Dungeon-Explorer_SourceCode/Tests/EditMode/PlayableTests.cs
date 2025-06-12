using System;
using System.Collections;
using System.Drawing.Text;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayableTests
{
    private HighScoreScript _HighScoreScript;
    private int _backupHighScore;

    [Test]
    public void CheckIncreaseHighScore()
    {
        GameObject obj = new GameObject();
        _HighScoreScript = obj.AddComponent<HighScoreScript>();

        _backupHighScore = Convert.ToInt32(_HighScoreScript.GetHighScoreText());

        _HighScoreScript.ResetHighScore();

        int before = Convert.ToInt32(_HighScoreScript.GetHighScoreText());
        Assert.AreEqual(0, before);

        int increase = 3;
        _HighScoreScript.IncreaseHighScore(increase);
        int after = Convert.ToInt32(_HighScoreScript.GetHighScoreText());

        Assert.AreEqual(before + increase, after);
    }

    [TearDown]
    public void TearDown()
    {
        _HighScoreScript.ChangeHighScore(_backupHighScore);
    }
}
