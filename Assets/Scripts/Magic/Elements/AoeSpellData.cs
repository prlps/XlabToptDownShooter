using UnityEngine;

public class AoeSpellData : BaseSpellData 
{
    [SerializeField][Min(0f)] private float m_radius;

    public float radius => m_radius;
}
