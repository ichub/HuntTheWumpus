﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HuntTheWumpus.Source
{
    /// <summary>
    /// Class which represents the player in each individual room.
    /// </summary>
    class PlayerAvatar : IDrawable, IUpdateable, IInitializable, ICollideable
    {
        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 TextureSize { get; set; }
        public List<BoundingBox> BoundingBoxes { get; set; }
        public Team ObjectTeam { get; set; }

        public bool ContentLoaded { get; set; }
        public bool Initialized { get; set; }

        private Vector2 velocity;
        private Vector2 lastPosition;
        private int moveSpeed = 2;

        public PlayerAvatar(MainGame mainGame, ILevel parentLevel)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;

            this.ObjectTeam = Team.Player;
            this.Position = new Vector2(100, 100);
            this.BoundingBoxes = new List<BoundingBox>();
            this.lastPosition = this.Position;
        }

        public void Move()
        {
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.A))
                this.velocity += new Vector2(-moveSpeed, 0);
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.W))
                this.velocity += new Vector2(0, -moveSpeed);
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.D))
                this.velocity += new Vector2(moveSpeed, 0);
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.S))
                this.velocity += new Vector2(0, moveSpeed);

            this.Position += velocity;
            this.velocity /= 1.2f;

            // limits speed vector to a length of 5 pixels per frame.
            if (this.velocity.LengthSquared() > 5 * 5)
            {
                this.velocity /= this.velocity.Length();
                this.velocity *= 5;
            }
        }

        public void CollideWithWalls()
        {
            if (this.Position.X < 0)
            {
                this.Position = new Vector2(0, this.Position.Y);
                this.velocity = Vector2.Zero;
            }
            if (this.Position.Y < 0)
            {
                this.Position = new Vector2(this.Position.X, 0);
                this.velocity = Vector2.Zero;
            }
            if (this.Position.X > this.MainGame.Graphics.PreferredBackBufferWidth - this.TextureSize.X)
            {
                this.Position = new Vector2(this.MainGame.Graphics.PreferredBackBufferWidth - this.TextureSize.X, this.Position.Y);
                this.velocity = Vector2.Zero;
            }
            if (this.Position.Y > this.MainGame.Graphics.PreferredBackBufferHeight - this.TextureSize.Y)
            {
                this.Position = new Vector2(this.Position.X, this.MainGame.Graphics.PreferredBackBufferHeight - this.TextureSize.Y);
                this.velocity = Vector2.Zero;
            }

        }

        public void FireProjectile()
        {
            var projectile = new Projectile(this.MainGame, this.ParentLevel, Team.Player, "fireball");
            projectile.Position = this.Position;

            if (MainGame.InputManager.IsClicked(Keys.Up))
            {
                projectile.Velocity = new Vector2(0, -4);
                this.ParentLevel.GameObjects.Add(projectile);
                projectile.rotation = 4.71238898;
            }
            if (MainGame.InputManager.IsClicked(Keys.Down))
            {
                projectile.Velocity = new Vector2(0, 4);
                this.ParentLevel.GameObjects.Add(projectile);
                projectile.rotation = 1.57079633;
            }
            if (MainGame.InputManager.IsClicked(Keys.Left))
            {
                projectile.Velocity = new Vector2(-4, 0);
                this.ParentLevel.GameObjects.Add(projectile);
                projectile.rotation = 3.14159265;
            }
            if (MainGame.InputManager.IsClicked(Keys.Right))
            {
                projectile.Velocity = new Vector2(4, 0);
                this.ParentLevel.GameObjects.Add(projectile);
                projectile.rotation = 0;
            }
        }

        public void CollideWith(ICollideable gameObject)
        {
            if (gameObject is Projectile)
            {
                System.Diagnostics.Debug.Print(DateTime.Now.Second.ToString());
            }
        }

        public void Initialize()
        {
            this.BoundingBoxes.Add(Extensions.Box2D(this.Position, this.Position + this.TextureSize));
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("player");
        }

        public void Update(GameTime gameTime)
        {
            this.BoundingBoxes[0] = Extensions.Box2D(this.Position, this.Position + this.TextureSize);
            this.lastPosition = this.Position;
            FireProjectile();
            this.Move();
            this.CollideWithWalls();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }
    }
}
