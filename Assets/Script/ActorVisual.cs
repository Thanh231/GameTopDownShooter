using UnityEngine;

[RequireComponent(typeof(Actor))]
public class ActorVisual : MonoBehaviour
{
    private FlashVfx m_flashVfx;
    protected Actor m_actor;

    protected virtual void Awake()
    {
        m_actor = GetComponent<Actor>();
        m_flashVfx = GetComponent<FlashVfx>();
    }

    public virtual void OnTakeDamage()
    {
        if (m_flashVfx == null || m_actor == null || m_actor.IsDead) return;
        m_flashVfx.Flash(m_actor.statsData.knockBackTime);
    }
}
