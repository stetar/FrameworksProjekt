using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using FrameworksProjekt.Interfaces;
using FrameworksProjekt.Items;
using FrameworksProjekt.Builder;

namespace FrameworksProjekt.Components
{
    public enum Miniontype
    {
        Chubby,
        Vegan,
        Hipster,

    }
    public class Minion : Component, ILoadable, IUpdateable, IOnCollisionStay
    {
        private float speed;
        private float strength;
        public Animator animator;
        private Direction direction;
        private Miniontype type;
        private Vector2 target;
        private Level currentLevel;
        private Level targetLevel;
        // Has minion been Shanghaiied yet
        private bool wild;
        // Cost of conversion
        private Item cost;
        private bool loaded = false;
        private bool plundering = false;
        private static Random r = new Random();

        public Miniontype Type
        {
            get { return type; }
            set { type = value; }
        }

        public Level CurrentLevel
        {
            get
            {
                return currentLevel;
            }

            set
            {
                currentLevel = value;
            }
        }

        public bool Wild
        {
            get
            {
                return wild;
            }

            set
            {
                wild = value;
            }
        }

        public Item Cost
        {
            get
            {
                return cost;
            }

            set
            {
                cost = value;
            }
        }

        public Level TargetLevel
        {
            get
            {
                return targetLevel;
            }

            set
            {
                targetLevel = value;
            }
        }

        public float Strength
        {
            get
            {
                return strength;
            }

            set
            {
                strength = value;
            }
        }

        public Minion(GameObject gameObject) : base(gameObject)
        {
            this.direction = Direction.Down;
            this.Cost = new ItemGenerator().GenerateItem((Category)r.Next(5), 5);
#if DEBUG
            GameWorld.Instance.MainInventory.AddItem(Cost);
#endif
            this.wild = true;
        }

        public void LoadContent(ContentManager content)
        {
            if (!loaded)
            {
                this.animator = (Animator)GameObject.GetComponent("Animator");
                SetTypeValues();
                CreateAnimation();

                loaded = true;
            }
        }

        public void Update()
        {
            CheckActions();

            FindTarget();

            Move();
        }

        public void CheckActions()
        {
            // leaving indoor room when first converted or leaving headquarter
            if (targetLevel != currentLevel
                && currentLevel is InsideLevel 
                && target == ((InsideLevel)currentLevel).MinionExitPoint 
                && VeryCloseToTarget())
            {
                MoveLevel(((InsideLevel)currentLevel).City);
                target = Vector2.Zero;
            }
            // entering headquarter
            else if (targetLevel != null
                && targetLevel.Name == "Headquarters" 
                && currentLevel.Name == "Grenaa"
                // Headquarter x-coord in Grenaa
                && target.X == 300
                && VeryCloseToTarget())
            {
                LevelDirector ld = new LevelDirector(new HeadQuartersBuilder());
                MoveLevel(ld.Construct());
                target = Vector2.Zero;
            }
            // taking buss
            else if (targetLevel != null
                && targetLevel.Name != currentLevel.Name
                && currentLevel is OutsideLevel
                && VeryCloseToTarget())
            {
                MoveLevel(targetLevel);
                target = Vector2.Zero;
            }
            // plundering store
            else if(targetLevel is OutsideLevel
                && currentLevel.Name == targetLevel.Name
                && VeryCloseToTarget())
            {
                ((OutsideLevel)currentLevel).MinionShopAction(this);
                LevelDirector ld = new LevelDirector(new HeadQuartersBuilder());
                this.TargetLevel = ld.Construct();
            }
        }

        public void FindTarget()
        {
            // just converted - leaving inside level
            if(wild == false && currentLevel is InsideLevel && targetLevel.Name != currentLevel.Name)
            {
                target = ((InsideLevel)currentLevel).MinionExitPoint;
            }
            // In Grenaa - returning to headquarter
            else if (TargetLevel != null && TargetLevel.Name == "Headquarters" && currentLevel.Name == "Grenaa")
            {
                // Headquarter coords in Grenaa
                target = new Vector2(300, 500);
            }
            // Outside in wrong city - going to busstop (only have to go into headquarter if outside and above statement checks that looting is in targetlevel)
            else if(currentLevel is OutsideLevel && TargetLevel.Name != currentLevel.Name)
            {
                target = ((OutsideLevel)currentLevel).MapPosition;
            }
            // outside in targetLevel - out plundering
            else if(currentLevel is OutsideLevel && TargetLevel.Name == currentLevel.Name)
            {
                target = ((OutsideLevel)currentLevel).ShopPosition;
            }
            else if(target == Vector2.Zero)
            {
                animator.PlayAnimation("IdleFront");

                // random chance of choosing random target in level
                if (r.Next(1000) <= 1)
                {
                    target = new Vector2(r.Next(currentLevel.Boundaries.Item1, currentLevel.Boundaries.Item2 - ((SpriteRenderer)GameObject.GetComponent("SpriteRenderer")).Rectangle.Width), this.GameObject.GetTransform.Position.Y);
                }
            }
        }

        public void Move()
        {
            Vector2 translation = Vector2.Zero;

            if(VeryCloseToTarget())
            {
                target = Vector2.Zero;
                animator.PlayAnimation("IdleFront");
            }

            if(target != Vector2.Zero)
            { 
                if (this.GameObject.GetTransform.Position.X > target.X)
                {
                    translation += new Vector2(-1,0);
                    direction = Direction.Left;
                    animator.PlayAnimation("WalkLeft");
                }
                else if (this.GameObject.GetTransform.Position.X < target.X)
                {
                    translation += new Vector2(1,0);
                    direction = Direction.Right;
                    animator.PlayAnimation("WalkRight");
                }
            }

            UpdateAnimation();

            GameObject.GetTransform.Translate(translation * speed * GameWorld.Instance.Delta);
        }

        public void UpdateAnimation()
        {
            
        }

        public void CreateAnimation()
        {
            animator.CreateAnimation("IdleFront", new Animation(2, 0, 0, 128, 128, 2, new Vector2(0, 0)));
            animator.CreateAnimation("WalkRight", new Animation(2, 128, 0, 128, 128, 4, new Vector2(0, 0)));
            animator.CreateAnimation("WalkLeft", new Animation(2, 256, 0, 128, 128, 4, new Vector2(0, 0)));

            animator.PlayAnimation("IdleFront");
        }

        public void SetTypeValues()
        {
            switch (type)
            {
                    case Miniontype.Chubby:
                    Strength = 10;
                    speed = 70;
                    return;
                    
                    case Miniontype.Hipster:
                    Strength = 7;
                    speed = 90;
                    return;

                    case Miniontype.Vegan:
                    Strength = 3;
                    speed = 110;
                    return;

            }
        }

        public void OnCollisionStay(Collider other)
        {
            
        }

        public void MoveLevel(Level targetLevel)
        {
            // tiny bit of randomness to spawnpoint so minions wont clump up as much
            this.GameObject.GetTransform.Position = new Vector2(targetLevel.SpawnPoint.X + r.Next(100), targetLevel.SpawnPoint.Y + r.Next(20) - 10);
            this.currentLevel = targetLevel;
        }

        public bool VeryCloseToTarget()
        {
            return Math.Abs(target.X - this.GameObject.GetTransform.Position.X) < 1;
        }
    }
}
