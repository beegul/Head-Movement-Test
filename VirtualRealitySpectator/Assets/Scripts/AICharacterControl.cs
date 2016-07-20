using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.ThirdPerson
{


    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling

        //Targets to aim for, in the order chosen.
        public Transform Target1;                                    
        public Transform Target2;
        public Transform Target3;
        public Transform Target4;

        public Transform Target5;
        public Transform Target6;
        public Transform Target7;
        public Transform Target8;

        public Transform Target9;
        public Transform Target10;
        public Transform Target11;
        public Transform Target12;

        public Transform Target13;
        public Transform Target14;
        public Transform Target15;
        public Transform Target16;

        public Transform Target17;
        public Transform Target18;
        public Transform Target19;

        public int CurrentTarget;
        public List<Transform> TargetList = new List<Transform>();

        Vector3 StartPosition;

        Animator MyAnimator;

        RaycastHit Hit;

        public bool StartMoving = false;

        public ParticleSystem GunParticles;

        AudioSource Audio;
        public AudioClip GunshotSound;
        public AudioClip FootstepSound;
        public AudioClip Ambience;
        public AudioClip ZombieHorde;
        public AudioClip Campfire;

        int LeftIndex;
        int RightIndex;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = true;
	        agent.updatePosition = true;

            CurrentTarget = 0;//Which target in the list the actor should navigate toward.

            //Adds our targets to the TargetList.
            TargetList.Add(Target1);
            TargetList.Add(Target2);
            TargetList.Add(Target3);
            TargetList.Add(Target4);

            TargetList.Add(Target5);
            TargetList.Add(Target6);
            TargetList.Add(Target7);
            TargetList.Add(Target8);

            TargetList.Add(Target9);
            TargetList.Add(Target10);
            TargetList.Add(Target11);
            TargetList.Add(Target12);

            TargetList.Add(Target13);
            TargetList.Add(Target14);
            TargetList.Add(Target15);
            TargetList.Add(Target16);

            TargetList.Add(Target17);
            TargetList.Add(Target18);
            TargetList.Add(Target19);

            StartPosition = transform.position;

            MyAnimator = GetComponent<Animator>();

            Audio = GetComponent<AudioSource>();

            LeftIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
            RightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || SteamVR_Controller.Input(LeftIndex).GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) || SteamVR_Controller.Input(RightIndex).GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                if (StartMoving == true)
                {
                    StartMoving = false;
                    SceneManager.LoadScene(0);
                }
                else
                {
                    Audio.PlayOneShot(ZombieHorde, 0.5f);
                    Audio.PlayOneShot(Ambience, 0.25f);
                    Audio.Play();
                    StartMoving = true;
                }
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if(StartMoving == true)
            {
                if (CurrentTarget != 19)
                {
                    agent.SetDestination(TargetList[CurrentTarget].position);
                }

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    character.Move(agent.desiredVelocity, false, false);
                }
                else
                {
                    character.Move(Vector3.zero, false, false);
                }

                if (Vector3.Distance(transform.position, agent.destination) <= 0.1f)//Checks to see if the actor has reached its destination. If so, move to a new waypoint.
                {
                    if (TargetList[CurrentTarget].name.Contains("ShootWaypoint"))
                    {
                        transform.rotation = TargetList[CurrentTarget].rotation;
                        StartCoroutine(Shoot());//When we have reached out waypoint and we are not at the last waypoint, play the "Shoot" coroutine.
                        CurrentTarget = CurrentTarget + 1;
                    }
                    else
                    {
                        CurrentTarget = CurrentTarget + 1;
                    }
                    if (CurrentTarget == 19)
                    {
                        //agent.SetDestination(StartPosition);
                        if (Vector3.Distance(transform.position, agent.destination) <= 0.1f)//If we have gone back to the start position.
                        {
                            //agent.Stop();//Stop the current pathfinding.
                        }
                        //CurrentTarget = 0;//Sets it so the actor moves between the points on a loop.
                       SceneManager.LoadScene(0);//Resets scene when each waypoint has been visited.
                    }
                }
            }
        }

        IEnumerator Shoot()
        {
            agent.Stop();//Stop the agent moving for the moment.
            MyAnimator.CrossFade("Shoot", 0.1f);//Play the desired animation while it is stopped.
            Audio.PlayOneShot(GunshotSound, 0.75f);
            yield return new WaitForSeconds(0.2f);
            GunParticles.Play();
            yield return new WaitForSeconds(2.3f);//Play the animation/wait in position for this amount of time.
            agent.Resume();//Resume the normal movement.
        }

        void FixedUpdate()//This runs our raycast to see if we wre pointing at an enemy.
        {
            Vector3 OffestAngle = Quaternion.AngleAxis(-180, transform.forward) * transform.forward;

            Ray MyRaycast = new Ray(transform.position, (transform.position + transform.forward * 1.5f) * 50);//transform.forward
            //RaycastHit Hit;

            if (!Physics.Raycast(MyRaycast, out Hit, 50))//Debug to show the direction of the Ray Cast.
            {
                Debug.DrawRay(transform.position, (transform.position + transform.forward * 1.5f) * 50, Color.red);//transform.forward
            }

            if (Physics.Raycast(MyRaycast, out Hit, Mathf.Infinity))
            {
                if (Hit.collider.gameObject.tag == "Enemy" && MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))//Checks if we are hitting an enemy, and if we are currently playing the "Shoot" animation.
                {
                    Debug.DrawRay(transform.position, transform.position + transform.forward * 50, Color.green);
                    //Debug.Log("Hit something with Raycast");
                    Hit.transform.SendMessage("HitByRay");
                }
            }
        }

    }
}
