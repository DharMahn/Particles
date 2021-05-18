using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace monogame_particles
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        const int max = 10;
        Particle[] particles = new Particle[max];
        System.Drawing.Rectangle bounds;
        Texture2D pixel;
        
        Random r = new Random();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //bounds = System.Windows.Forms.Screen.AllScreens.Select(screen => screen.Bounds).Aggregate(System.Drawing.Rectangle.Union);
            bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            graphics.PreferredBackBufferWidth = bounds.Width;
            graphics.PreferredBackBufferHeight = bounds.Height;
            Window.Position = new Point(0, 0);
            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
            TargetElapsedTime = TimeSpan.FromSeconds(1 / 144.0f);

            for (int i = 0; i < max; i++)
            {
                particles[i] = new Particle(new Vector2(r.Next(bounds.Width), r.Next(bounds.Height)), new Color(r.Next(256), r.Next(256), r.Next(256)), bounds.Width, bounds.Height,r.Next(2));
            }
            Window.IsBorderless = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[1] { Color.White });
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState ms = Mouse.GetState();
            Parallel.For(0, max, i =>
            {
                particles[i].Update(new Vector2(ms.X, ms.Y), ms.LeftButton == ButtonState.Pressed,particles);
            });
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            /*GraphicsDevice.Textures[0] = null;
            pixels = new uint[bounds.Width * bounds.Height];
            Parallel.For(0, max, i =>
            {
                int index = ((bounds.Width * (int)particles[i].position.Y) + (int)particles[i].position.X);
                pixels[index] = ToUint(particles[i].color);
            });
            canvas.SetData(pixels, 0, bounds.Width * bounds.Height);*/
            spriteBatch.Begin();
            for (int i = 0; i < max; i++)
            {
                spriteBatch.Draw(pixel, new Rectangle((int)particles[i].position.X, (int)particles[i].position.Y, 1, 1), particles[i].color);
            }
            /*Parallel.For(0, max, i =>
            {
                
            });*/
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private uint ToUint(Color c)
        {
            return (uint)(((c.A << 24) | (c.R << 16) | (c.G << 8) | c.B) & 0xffffffffL);
        }
    }
}
