using System;
using System.Collections;
using UnityEngine;
using Players;

namespace Magic.Systems
{
    public sealed class MagicSystem : MonoBehaviour
    {
        public event Action<MagicState> StateChanged;
        public event Action SpellCancelled;

        public event Action<IReadOnlyList<ElementType>> ElementChanged
        {
            add => spellPreparation.ElementChanged += value;
            remove => spellPreparation.ElementChanged -= value;
        }

        [SerializeField] private MagicConfig m_config;
        [SerializeField] private NavMeshMouseResolver m_mouseResolver;

        private MagicState m_state;
        private SpellPreparation m_spellPreparation;
        private SpellCaster m_caster;
        private Coroutine m_cooldownCoroutine;

        public MagicState state
        {
            get => m_state;
            private set
            {
                if (m_state != value)
                {
                    m_state = value;
                    StateChanged?.Invoke(m_state);
                }
            }
        }

        private SpellPreparation spellPreparation =>
            m_spellPreparation ??= new SpellPreparation(m_config);

        private void OnEnable() =>
            spellPreparation.OverflowOccurred += CancelSpell;

        private void OnDisable() =>
            spellPreparation.OverflowOccurred -= CancelSpell;

        private void Awake()
        {
            m_caster = new SpellCaster(transform);
        }

        public void AddElement(ElementType element)
        {
            if (state is MagicState.Cooldown or MagicState.Casting)
            {
                return;
            }

            spellPreparation.AddElement(element);
            state = MagicState.Preparation;
        }

        public void TryCastSpell()
        {
            if (state is not MagicState.Preparation)
            {
                return;
            }

            var cursorPos = m_mouseResolver.GetCursoureWorldPosition();

            if (spellPreparation.TryGetSpell(out var spell) && cursorPos.HasValue)
            {
                state = MagicState.Casting;

                m_caster.Cast(spell, cursorPos.Value);

                spellPreparation.Clear();
                state = MagicState.Idle;
            }
            else
            {
                CancelSpell();
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

        private void StartCooldown()
        {
            if (m_cooldownCoroutine is not null)
            {
                StopCoroutine(m_cooldownCoroutine);
            }

            m_cooldownCoroutine = StartCoroutine(CooldownRoutine());
        }

        private IEnumerator CooldownRoutine()
        {
            state = MagicState.Cooldown;
            yield return new WaitForSeconds(m_config.cancelCooldown);
            state = MagicState.Idle;

            m_cooldownCoroutine = null;
        }

        public enum MagicState
        {
            Idle,
            Preparation,
            Cooldown,
            Casting
        }
    }
}