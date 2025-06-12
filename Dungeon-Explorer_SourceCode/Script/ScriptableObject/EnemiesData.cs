using UnityEngine;


[CreateAssetMenu(fileName = "EnemyDataStore", menuName = "ScriptableObject/EnemyDataStores", order = 1)]
public class EnemiesData : ScriptableObject
{
    public string EnemyName;
    public int Health;
    public float speed;
    public float RunDis;
    public float AttackDis;
    public int xpValue;
    public int damageValue;
    public int ScoreValue;
}
