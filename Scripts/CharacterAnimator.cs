using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replacableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;
    const float locomotionAnimationSmoothTime = 0.1f;
    protected Animator animator;
    protected CharacterCombat combat;
    // public GameObject health;
    // private CharacterStats currentHealth;
    public AnimatorOverrideController overrideController;
    NavMeshAgent agent;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //currentHealth = health.GetComponent<CharacterStats>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        if(overrideController == null){
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        animator.runtimeAnimatorController = overrideController;
        //overrideController["Attack_D"] = 

        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

        animator.SetBool("inCombat",combat.InCombat);

        if(Input.GetKeyDown(KeyCode.C)){
            Cry();
        }

        // if(currentHealth.currentHealth <= 0){
        //     die();
        // }
    }

    protected virtual void OnAttack(){
        animator.SetTrigger("Attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replacableAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }

    void Cry(){
        animator.SetTrigger("cry");
    }

    // void die(){
    //     animator.SetTrigger("death");
    // }
}