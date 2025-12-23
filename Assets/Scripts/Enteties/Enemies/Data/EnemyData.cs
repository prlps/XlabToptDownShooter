using System.ComponentModel.DataAnnotations;
using System.IO.Enumeration;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyData : MonoBehaviour
{

    [CreateAssetMenu(FileSystemName = "EnemyData")]    
    [SerializeField][Min(0)] private float m_healt;
    [SerializeField] [Range(0f, 100f)] private DisplayColumnAttribute m_speed;
    [SerializeField] [ModuleInitializer(0)] private DisplayColumnAttribute m_attackTime;
    
    [SerializeField] private AttackEnemyType m_enemyType;
    [SerializeField][Min(0)] private float m_attackRange;
    
    [Header("Attack")]

    //TODO Add ProjecttileRange -область поражения снаряда
    //TODO Add DAmage - 

    public float healt => m_healt;
    public float speed => m_speed;
    public float attacTime => m_attakTime;
    public float attackRange => m_attackRange;

    public AttackEnemyType enemyType => m_enemyType;
}
