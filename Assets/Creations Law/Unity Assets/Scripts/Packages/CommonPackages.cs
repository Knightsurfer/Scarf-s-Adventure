using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using ScriptableObjects;

namespace PlayerVariables
{
    public class CharacterPackage : MonoBehaviour
    {
        /// <summary>
        /// Determines how intense gravity is.
        /// </summary>
        protected float gravity = 3;

        /// <summary>
        /// When the controls need to change for a specific situation, this controls that.
        /// </summary>
        [HideInInspector] public string moveType = "Normal";







        /// <summary>
        /// The interactable object that is currently in focus.
        /// </summary>
        protected Interactable focus;

        /// <summary>
        /// The armature object.
        /// </summary>
        protected Transform skeleton;

        /// <summary>
        /// Grabs variables from the game manager.
        /// </summary>
        protected GameManager game;

        /// <summary>
        /// The animator attached to the character.
        /// </summary>
        [HideInInspector] public Animator anim;

        /// <summary>
        /// The component that moves the player around, handles the collisions and also creates gravity.
        /// </summary>
        [HideInInspector] public CharacterController player;

        /// <summary>
        /// Holds the prefab used to spawn the player model.
        /// </summary>
        public _Character playerInfo;

        #region Stats
        /// <summary>
        /// The max amount of health the character can have.
        /// </summary>
        [HideInInspector] public int healthMax = 100;

        /// <summary>
        /// The max amount of magic the character can have.
        /// </summary>
        [HideInInspector] public int magicMax = 100;

        /// <summary>
        /// The current level the character is at.
        /// </summary>
        [HideInInspector] public int level = 1;

        /// <summary>
        /// The current amount of magic the character has.
        /// </summary>
        [HideInInspector] public int magic = 50;

        /// <summary>
        /// The current amount of health the character has.
        /// </summary>
        [HideInInspector] public int health = 70;

        /// <summary>
        /// The current amount of exp the character has.
        /// </summary>
        [HideInInspector] protected int exp;
        #endregion

        public CharacterController bot;
        public NavMeshAgent botAI;

        public Transform target;

        protected bool inBattle;

        public int botType;

        public bool canMove;


        protected void AssignPlayer()
        {
            if (!GetComponentInChildren<SkinnedMeshRenderer>())
            {
                GameObject playerPrefab;
                playerPrefab = Instantiate(playerInfo.prefab, this.transform);
                anim = playerPrefab.GetComponent<Animator>();

                Destroy(GetComponent<MeshFilter>());
                Destroy(GetComponent<MeshRenderer>());
            }
            else
            {
                anim = GetComponentInChildren<Animator>();
            }
        }

        public void FacePlayer()
        {
            if (target)
            {
                GetComponentInChildren<SkinnedMeshRenderer>().transform.LookAt(new Vector3(target.position.x, 0, target.position.z));
            }
        }

        protected void FaceTarget()
        {
           
            if(target)
            {
                //neck.LookAt(new Vector3(target.position.x,target.position.y + 1.5f, target.position.z -1.5f));
            }
            
        }
    }
}
