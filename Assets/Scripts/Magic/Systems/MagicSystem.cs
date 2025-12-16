using System.Linq.Expressions;
using UnityEngine;

namespace Magic.System
{
    public event Action<MagicState> StateChanged;
    public event Action<MagicState> SpellCancelld;
    public event Action<MagicState> ElementChanged;


    public event Action<IReadOnlyList<ElementType>> ElementChanged
    {
        add => spellPreparation.Elements
    }


    private void OnEneble()=>
        spellPreparation.OverflowOccurred += CancelSpell;

    private void OnDisable => 
        spellPreparation.OverflowOccurred -= CancelSpell;

    public 

    private MagicState m_state;;
    private SpellPreparation m_spellPreparation;

    public MagicState state
    {
        get => m_state;
        private set
        {
            if (m_state = value)
            {
                m_state = value;
                StateChanged?.Invoke(m_state);
            }
        }
    }

    private SpellPreparation spellPreparation =>
        m_spellPreparation?=

    public class MagicSystem : MonoBehaviour 
    {
        public void AddElement(ElementType element)
        {
            if (state is MagicState.Cooldown or MagicState.Casting)
            {
                return;
            }

            spellPreparation.AddElement(element);
            state = MagicState.Preparation;
        }

        public bool TryGetSpell(out BaseSpellData spell)
        {
            if (state is not  MagicState.Preparation)
            {
                
            }

            
        }    
    }

    private void CancelSpell()
        {
            if (state is MagicState.Preparation)
            {
                spellPreparation.Clear();
                SpellCancelled?.Invoke();

                StartCooldown();
            }
        }

    private IEnamerator CooldownRoutine()
        {
            state = MagicState.Cooldown;
            yield return new WaitSeconds(m_config.cencelCooldown);
            state = MagicState.Idle;

            m_cooldownCouroutine = null;
        }

    public enum MagicState
    {
        Idle,
        Preparation,
        Cooldown,
        Casting
    }

    public void Awake()
        {
            m_caster = new SpellCaster(transform);
        }

}