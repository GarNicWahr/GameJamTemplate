using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageSystem : MonoBehaviour
{

    private PlayerStats _playerStats;
    private NPCStateMachine _npcStateMachine;

    // Start is called before the first frame update
    void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _npcStateMachine = GameObject.FindWithTag("Enemy").GetComponent<NPCStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag =="Enemy")
        {
            Debug.Log("Hit Enemy!");
            _playerStats.SetValues(0, _playerStats.StatValues(true) - _npcStateMachine.Damage);
        }
    }
}
