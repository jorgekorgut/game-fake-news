using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioSource jumpAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
		private Collider2D[] ground2d;
        public Health health;
        public bool controlEnabled = false;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public UICollectable uiCollectable;

        public Bounds Bounds => collider2d.bounds;

        public int collectable22 = 0;
        public int collectable13 = 0;
        public int try22 = 0;
        public int try13 = 0;
        public int point22 = 0;
        public int point13 = 0;

        public GameObject followCamera;
        public GameObject mainCamera;

        public void initialize()
        {
            collectable22 = 0;
            collectable13 = 0;
            try22 = 0;
            try13 = 0;
            point22 = 0;
            point13 = 0;
            controlEnabled = false;
        }

        public void addCollectable(int type)
        {
            if(type == 22)
            {
                collectable22++;
            }
            else
            {
                collectable13++;
            }
            uiCollectable.updateUI();
        }

        public void removeCollectable(int type)
        {
            if(type == 22)
            {
                collectable22--;
            }
            else
            {
                collectable13--;
            }
            uiCollectable.updateUI();
        }

        public void addTry(int type)
        {
            if(type == 22)
            {
                try22++;
            }
            else
            {
                try13++;
            }
        }

        public void addPoint(int type)
        {
            if(type == 22)
            {
                point22++;
            }
            else
            {
                point13++;
            }
        }

        void Awake()
        {
            health = GetComponent<Health>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground");
            GameObject[] wallObjects = GameObject.FindGameObjectsWithTag("Wall");
            ground2d = new Collider2D[groundObjects.Length + wallObjects.Length];

            
            
            for(int i = 0; i < groundObjects.Length; i++)
            {
                ground2d[i] = groundObjects[i].GetComponent<Collider2D>();
            }

            for(int i = 0; i < wallObjects.Length; i++)
            {
                ground2d[groundObjects.Length + i] = wallObjects[i].GetComponent<Collider2D>();
            }
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                {
                    jumpAudio.Play();
                    jumpState = JumpState.PrepareToJump;
                }
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    bool isFlying = true;
                    for(int i = 0 ; i < ground2d.Length; i++)
                    {
                        if (collider2d.IsTouching(ground2d[i]) && ground2d[i].gameObject.tag == "Ground")
                        {
                            isFlying = false;
                        }
                    }
                    
                    if(isFlying)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }

                    else
                    {
                        jumpState = JumpState.Grounded;
                    }
                    
                    break;
                case JumpState.InFlight:
                    for(int i = 0 ; i < ground2d.Length; i++)
                    {
                        if (collider2d.IsTouching(ground2d[i]) && ground2d[i].gameObject.tag == "Ground")
                        {
                            Schedule<PlayerLanded>().player = this;
                            jumpState = JumpState.Landed;
                        }
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            for(int i = 0 ; i < ground2d.Length; i++)
            {
                if (jump && collider2d.IsTouching(ground2d[i]))
                {
                    velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                    jump = false;
                }
                else if (stopJump)
                {
                    stopJump = false;
                    if (velocity.y > 0)
                    {
                        velocity.y = velocity.y * model.jumpDeceleration;
                    }
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            bool isGrounded = false;
            for(int i = 0 ; i < ground2d.Length; i++)
            {
                if (collider2d.IsTouching(ground2d[i]))
                {
                    isGrounded = true;
                }
            }
            animator.SetBool("grounded", isGrounded);
            
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}