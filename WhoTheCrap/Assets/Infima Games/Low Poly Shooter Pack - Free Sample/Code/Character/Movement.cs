// Copyright 2021, Infima Games. All Rights Reserved.

using System.Linq;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

namespace InfimaGames.LowPolyShooterPack
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class Movement : MovementBehaviour
    {
        #region FIELDS SERIALIZED

        [Header("Speeds")]

        [SerializeField]
        private float speedWalking = 5.0f;

        [Tooltip("How fast the player moves while running."), SerializeField]
        private float speedRunning = 9.0f;

        [SerializeField]
        EventReference birdEventRef;

        EventInstance animalEv;

        [SerializeField]
        EventReference brEvent;// = "event:/Environment/Breeze";

        EventInstance breezeEv;

        [SerializeField]
        EventReference reverbSnap;//
        EventInstance snapEv;


        #endregion

        #region PROPERTIES

        //Velocity.
        private Vector3 Velocity
        {
            //Getter.
            get => rigidBody.velocity;
            //Setter.
            set => rigidBody.velocity = value;
        }

        #endregion

        #region FIELDS

        /// <summary>
        /// Attached Rigidbody.
        /// </summary>
        private Rigidbody rigidBody;
        /// <summary>
        /// Attached CapsuleCollider.
        /// </summary>
        private CapsuleCollider capsule;

        /// <summary>
        /// Player Character.
        /// </summary>
        private CharacterBehaviour playerCharacter;
        /// <summary>
        /// The player character's equipped weapon.
        /// </summary>
        private WeaponBehaviour equippedWeapon;
        
        /// <summary>
        /// Array of RaycastHits used for ground checking.
        /// </summary>
        private readonly RaycastHit[] groundHits = new RaycastHit[8];

        #endregion

        #region UNITY FUNCTIONS

        /// <summary>
        /// Awake.
        /// </summary>
        protected override void Awake()
        {
            //Get Player Character.
            playerCharacter = ServiceLocator.Current.Get<IGameModeService>().GetPlayerCharacter();

            animalEv = RuntimeManager.CreateInstance(birdEventRef);
            RuntimeManager.AttachInstanceToGameObject(animalEv, transform, GetComponent<Rigidbody>());


            breezeEv = RuntimeManager.CreateInstance(brEvent);

            snapEv = RuntimeManager.CreateInstance(reverbSnap);
            snapEv.setParameterByName("Room", 1);
            snapEv.start();
        }

        /// Initializes the FpsController on start.
        protected override  void Start()
        {
            //Rigidbody Setup.
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            //Cache the CapsuleCollider.
            capsule = GetComponent<CapsuleCollider>();
        }

        private void OnDestroy()
        {
            animalEv.release();
            breezeEv.release();
            snapEv.release();
        }

        /// Checks if the character is on the ground.
        private void OnCollisionStay()
        {
            //Bounds.
            Bounds bounds = capsule.bounds;
            //Extents.
            Vector3 extents = bounds.extents;
            //Radius.
            float radius = extents.x - 0.01f;
            
            //Cast. This checks whether there is indeed ground, or not.
            Physics.SphereCastNonAlloc(bounds.center, radius, Vector3.down,
                groundHits, extents.y - radius * 0.5f, ~0, QueryTriggerInteraction.Ignore);
            
            //We can ignore the rest if we don't have any proper hits.
            if (!groundHits.Any(hit => hit.collider != null && hit.collider != capsule)) 
                return;
            
            //Store RaycastHits.
            for (var i = 0; i < groundHits.Length; i++)
                groundHits[i] = new RaycastHit();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (GameManager.instance.getTerrain() != 1 && other.CompareTag("GrassTrigger"))
            {
                setTer(1);
                breezeEv.start();
                animalEv.start();

                snapEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
            else if (GameManager.instance.getTerrain() != 2 && other.CompareTag("SandTrigger"))
            {
                setTer(2);
                breezeEv.start();
                animalEv.start();

                snapEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
            else if (GameManager.instance.getTerrain() != 0 && other.CompareTag("HallwayTrigger"))
            {
                setTer(0);
                breezeEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

                snapEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                snapEv.setParameterByName("Room", 0);
                snapEv.start();
            }
            else if (GameManager.instance.getTerrain() != 3 && other.CompareTag("RoomTrigger"))
            {
                GameManager.instance.setTerrain(3);
                breezeEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

                snapEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                snapEv.setParameterByName("Room", 1);
                snapEv.start();
            }
        }
        private void setTer(int ter)
        {
            GameManager.instance.setTerrain(ter);
            RuntimeManager.StudioSystem.setParameterByName("Terrain", ter);
        }


        protected override void FixedUpdate()
        {
            //Move.
            MoveCharacter();
        }

        /// Moves the camera to the character, processes jumping and plays sounds every frame.
        protected override  void Update()
        {
            //Get the equipped weapon!
            equippedWeapon = playerCharacter.GetInventory().GetEquipped();

            RuntimeManager.AttachInstanceToGameObject(animalEv, transform, GetComponent<Rigidbody>());
        }

        #endregion

        #region METHODS

        private void MoveCharacter()
        {
            #region Calculate Movement Velocity

            //Get Movement Input!
            Vector2 frameInput = playerCharacter.GetInputMovement();
            //Calculate local-space direction by using the player's input.
            var movement = new Vector3(frameInput.x, 0.0f, frameInput.y);
            
            //Running speed calculation.
            if(playerCharacter.IsRunning())
                movement *= speedRunning;
            else
            {
                //Multiply by the normal walking speed.
                movement *= speedWalking;
            }

            //World space velocity calculation. This allows us to add it to the rigidbody's velocity properly.
            movement = transform.TransformDirection(movement);

            #endregion
            
            //Update Velocity.
            Velocity = new Vector3(movement.x, Velocity.y, movement.z);
        }

        #endregion
    }
}