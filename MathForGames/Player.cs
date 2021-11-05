using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private float _health = 3;
        private int _lives = 3;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public float Health
        {
            get { return _health; }
        }

        public int Lives
        {
            get { return _lives; }
        }

        public Player(char icon, float x, float y, float speed, string name = "Actor", ConsoleColor color = ConsoleColor.White) 
            : base(icon, x, y, name, color)
        {
            _speed = speed;
        }


        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            Vector2 moveDirection = new Vector2();

            ConsoleKey keyPressed = Engine.GetNextKey();

            if (keyPressed == ConsoleKey.A)
                moveDirection = new Vector2 { X = -1 };
            if (keyPressed == ConsoleKey.D)
                moveDirection = new Vector2 { X = 1 };
            if (keyPressed == ConsoleKey.W)
                moveDirection = new Vector2 { Y = -1 };
            if (keyPressed == ConsoleKey.S)
                moveDirection = new Vector2 { Y = 1 };


            Velocity = moveDirection * Speed;

            Position += Velocity;
        }

        public override void Draw()
        {
            Engine.Render(Icon, Position);
        }

        public override void OnCollision(Actor actor)
        {
            TakeDamage(1);
        }

        public float TakeDamage(float damageAmount)
        {
            float damageTaken = _health - damageAmount;

            if (damageTaken < 0)
                return 0;

            _health -= damageAmount;

            if (Health <= 0)
                TryRespawn();

            return damageAmount;
        }

        private void TryRespawn()
        {
            if (Lives <= 0)
            { 
                Engine.CloseApplication();
                return;
            }


            Position = new Vector2 { X = 5, Y = 5 };
            _lives--;
            _health = 3;
        }
    }
}
