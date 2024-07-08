using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public float increaseSpeed;
    public Vector2 moveInput;
    private float currentSpeed;
    private PlayerStates playerStates;
    Vector2 mousePos;
    // Start is called before the first frame update
    public override void Init()
    {
        LoadStats();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        mousePos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - m_rd.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x);
        weapon.SetRotate(angle);

    }
    protected override void Move()
    {
        if(IsDead || m_isKnockBack) return;

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        if (moveInput != Vector2.zero)
        {
            currentSpeed += increaseSpeed * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerStates.moveSpeed);
            m_rd.velocity = moveInput * currentSpeed * Time.deltaTime;
            m_anim.SetBool(AniimationConstant.player_Run,true);
        }
        else
        {
            currentSpeed = 0;
            m_rd.velocity = Vector2.zero;
            m_anim.SetBool(AniimationConstant.player_Run, false);
        }
    }
    private void LoadStats()
    {
        if(statsData == null) return;
        playerStates = (PlayerStates)statsData;
        playerStates.Load();
        CurrentHP = playerStates.hp;
    }
}
