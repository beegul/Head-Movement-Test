using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]

    public class PatrolMovement : MonoBehaviour
    {
        public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling

        Transform Target;
        List<Transform> TargetList = new List<Transform>();
        Animator MyAnimator;

        UnityStandardAssets.Characters.ThirdPerson.AICharacterControl MyScript;

        void Start()
        {
            //MyScript = GameObject.Find("Policeman").GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
            MyScript = GameObject.Find("Character").GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
            gameObject.tag = "Patrol";//Sets the tag of every enemy prefab to "Patrol".

            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = true;
            agent.updatePosition = true;

            if (gameObject.name == "PatrolEnemy1(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint4").transform;
            }
            if (gameObject.name == "PatrolEnemy2(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint1").transform;
            }
            if (gameObject.name == "PatrolEnemy3(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint2").transform;
            }
            if (gameObject.name == "PatrolEnemy4(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint3").transform;
            }
            if (gameObject.name == "PatrolEnemy5(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint3").transform;
            }
            if (gameObject.name == "PatrolEnemy6(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint2").transform;
            }
            if (gameObject.name == "PatrolEnemy7(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint1").transform;
            }
            if (gameObject.name == "PatrolEnemy8(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint4").transform;
            }

            if (gameObject.name == "PatrolEnemy9(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint4").transform;
            }
            if (gameObject.name == "PatrolEnemy10(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint1").transform;
            }
            if (gameObject.name == "PatrolEnemy11(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint2").transform;
            }
            if (gameObject.name == "PatrolEnemy12(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint3").transform;
            }


            if (gameObject.name == "PatrolEnemy13(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint4").transform;
            }
            if (gameObject.name == "PatrolEnemy14(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint1").transform;
            }
            if (gameObject.name == "PatrolEnemy15(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint2").transform;
            }
            if (gameObject.name == "PatrolEnemy16(Clone)")
            {
                Target = GameObject.Find("PatrolWaypoint3").transform;
            }







            TargetList.Add(Target);
            MyAnimator = GetComponent<Animator>();
            MyAnimator.Play("Zombie");//PLays the "Zombie" animation for the enemies.

        }

        void Update()
        {
            if (TargetList[0] != null)
            {
                agent.SetDestination(TargetList[0].position);
            }

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }

            if (Vector3.Distance(transform.position, agent.destination) <= 0.5f)//Checks to see if the actor has reached its destination. If so, move to a new waypoint.
            {
                DestroyObject(gameObject);
            }
        }
    }
}


