using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
	[SerializeField] private m_defaultSpell;
	[SerializeField] private SpellEnemyData[] m_spells;
	[SerializeField][Min(0)] private float m_healt;
	[SerializeField] [Range(0f,100f)] private float m_speed;
	[SerializeField] [Min(0f)] private float m_attackTime;

	[SerializeField] private AttackEnemyType m_enemyType;
	[SerializeField][Min(0)] private float m_attackRange;
	[SerializeField] private BaseSpellData m_spell;

	[Header("Attack")]

	//TODO Add ProjectileRange - область поражения снаряда
	//TODO Add Damage - 
	
	public IReadOnlyList<SpellEnemyData> Spells => m_spells;

	public float healt => m_healt;
	public float speed => m_speed;
	public float attacTime => m_attackTime;
	public float attackRange => m_attackRange;

	public AttackEnemyType enemyType => m_enemyType;

	[Serializable]
	public struct SpellEnemyData
	{
		[SerializeField] public int count;
		[SerializeField] public BaseSpellData spell;
	}
