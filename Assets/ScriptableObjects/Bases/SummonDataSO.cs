using UnityEngine;

[CreateAssetMenu(fileName = "New Summon", menuName = "ScriptableObjects/Summon")]
public class SummonDataSO : ScriptableObject {
    public int maxHealth;
    public int meleeDamage;
    public int rangedDamage;
    public float range = 0f;
    public int defense;
    public float speed = 0f;
    public Sprite token;
    public float tokenScale = .3f;
    public GameObject unitPrefab;
}