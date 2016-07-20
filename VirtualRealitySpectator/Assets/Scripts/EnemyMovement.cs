using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class EnemyMovement : MonoBehaviour
    {
        public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling

        //Targets to aim for
        Transform Target;

        List<Transform> TargetList = new List<Transform>();

        UnityStandardAssets.Characters.ThirdPerson.AICharacterControl MyScript;

        Animator MyAnimator;
        private void Start()
        {
            //MyScript = GameObject.Find("Policeman").GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
            MyScript = GameObject.Find("Character").GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();

            gameObject.tag = "Enemy";//Sets the tag of every enemy prefab to "Enemy".

            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = true;
            agent.updatePosition = true;

            //Adds our targets to the TargetList.

            if (gameObject.name == "Enemy1(Clone)")
            {
                Target = GameObject.Find("EnemyWaypoint1").transform;
            }
            if (gameObject.name == "Enemy2(Clone)")//Enemy2(Clone)
            {
                Target = GameObject.Find("EnemyWaypoint2").transform;
            }
            if (gameObject.name == "Enemy3(Clone)")
            {
                Target = GameObject.Find("EnemyWaypoint3").transform;
            }
            if (gameObject.name == "Enemy4(Clone)")
            {
                Target = GameObject.Find("EnemyWaypoint4").transform;
            }
            if (gameObject.name == "Enemy5(Clone)")
            {
                Target = GameObject.Find("EnemyWaypoint5").transform;
            }
            if (gameObject.name == "Enemy6(Clone)")
            {
                Target = GameObject.Find("EnemyWaypoint6").transform;
            }
            if (gameObject.name == "Enemy7(Clone)")
            {
                Target = GameObject.Find("EnemyWaypoint7").transform;
            }
            if (gameObject.name == "Enemy8(Clone)")
            {
                Target = GameObject.Find("EnemyWaypoint8").transform;
            }

            TargetList.Add(Target);
            MyAnimator = GetComponent<Animator>();
            MyAnimator.Play("Zombie");//PLays the "Zombie" animation for the enemies.
        }

        private void Update()
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
                StartCoroutine(Death());
            }

            if (MyScript.StartMoving == false)
            {
                AllDespawn();
            }

        }
        void HitByRay()//If the enemy attached to this script is hit by a raycast, Start the "Death" co routine.
        {
            StartCoroutine(Death());
        }


        IEnumerator Death()
        {
            yield return new WaitForSeconds(0.1f);
            agent.Stop();//Stop the agent moving for the moment.
            MyAnimator.Play("Dead");//Play the desired animation while it is stopped.
            yield return new WaitForSeconds(5.0f);//Play the animation/wait in position for this amount of time.
            Destroy(gameObject);
        }

        void AllDespawn()
        {
            Destroy(gameObject);
        }
    }
}
