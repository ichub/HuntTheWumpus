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
    class Projectile : IEntity
    {
        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public AnimatedTexture Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 LastPosition { get; set; }
        public Vector2 Velocity { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public Team ObjectTeam { get; set; }

        public bool ContentLoaded { get; set; }
        public bool Initialized { get; set; }
        public bool HasCollided { get; private set; }
        public float SpeedDampening { get; set; }

        private string imageName;

        public Projectile(MainGame mainGame, ILevel parentLevel, Team team, string picture)
        {
            this.ObjectTeam = team;
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;

            this.Position = new Vector2(100, 100);
            this.BoundingBox = new BoundingBox();
            this.Velocity = new Vector2(0, 1);
            this.imageName = picture;
        }

        public void CollideWith(ICollideable gameObject, bool isCollided)
        {
            if (isCollided)
            {
                if (gameObject is Enemy)
                {
                    this.HasCollided = true;
                }
            }
        }

        public void CollideWithLevelBounds()
        {
            this.ParentLevel.GameObjects.Remove(this);
        }

        public void Initialize()
        {
            this.BoundingBox = Extensions.Box2D(this.Position, this.Position + this.Texture.Size);
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = new AnimatedTexture(content.Load<Texture2D>("Textures\\" + this.imageName), 5, 20, 20, 60);
        }

        public void Update(GameTime gameTime)
        {
            this.Position += Velocity * 3;
            this.BoundingBox = Extensions.Box2D(this.Position, this.Position + this.Texture.Size);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.Texture.Draw(spriteBatch, this.Position, this.MainGame.GameTime);
        }
    }
}