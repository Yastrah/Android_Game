using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangedEnemy : Enemy
{
    [SerializeField] private BehaviourType behaviour; // тип поведения
    
    private System.Random random = new System.Random();
    // UnityEngine.Random
    
    private enum BehaviourType {
        Default,
    }

    private new void Update() {
        base.Update();

        switch (status)
        {
            case StatusType.Passive:
                base.PassiveBehaviour();
                break;
            case StatusType.Aggressive:
                AggressiveBehaviour();
                break;
        }

        // move = new Vector2(0f, 0f); // в наследниках
        // transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void FixedUpdate() // итоговые изменения НА ЭКРАНЕ 
    {
        // rb.velocity = new Vector2(move.x, move.y) * speed;
        // rb.velocity = new Vector2(player.transform.position.x, player.transform.position.y) * speed;
    }

    private void AggressiveBehaviour() { // агрессивное поведение
        switch (behaviour){
            case BehaviourType.Default:
            
                // write

                break;
        }
    }
}
