using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex;
        private Scene[] _scenes = new Scene[0];
        private Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// Called to begin the application
        /// </summary>
        public void Run()
        {
            //Call start for the entire application
            Start();

            float currentTime = 0;
            float lastTime = 0;
            float deltaTime = 0;

            //Loop until the application is told to close
            while (!_applicationShouldClose && !Raylib.WindowShouldClose())
            {
                //Get how much time has passed since the application started
                currentTime = _stopwatch.ElapsedMilliseconds / 1000.0f;

                //Set delta time to be the difference in time from the last time recorded to the current time
                deltaTime = currentTime - lastTime;

                //Update the application
                Update(deltaTime);
                //Draw all items
                Draw();

                //Set the last time recorded to be the current time
                lastTime = currentTime;
            }

            //Call end for the entire application
            End();
        }

        /// <summary>
        /// Called when the application starts 
        /// </summary>
        private void Start()
        {
            _stopwatch.Start();

            //Create a window using raylib
            Raylib.InitWindow(800, 450, "Math For Games");
            Raylib.SetTargetFPS(60);

            Scene scene = new Scene();
            Player player = new Player(5, 5, 50, "Player", "Images/player.png");
            player.SetScale(100, 5);
            player.SetTranslation(300, 300);

            CircleCollider playerCircleCollider = new CircleCollider(20, player);
            AABBCollider playerBoxCollider = new AABBCollider(50, 50, player);
            player.Collider = playerCircleCollider;

            Enemy enemy = new Enemy(100, 5, 60, 100, 1, player, "Actor", "Images/enemy.png");
            enemy.SetScale(50, 50);
            CircleCollider enemyCircleCollider = new CircleCollider(5, enemy);
            AABBCollider enemyBoxCollider = new AABBCollider(50, 50, enemy);
            enemy.Collider = enemyBoxCollider;

            Actor testChild = new Actor(1, 1, "TestChild", "Images/enemy.png");
            player.AddChild(testChild);

            //UI section
            UIText text = new UIText(10, 10, "TestTextBox", Color.BLUE, 70, 70, 15, "This is test text. \nIt is not to be taken seriously as it is only a test.\nAnyone who says this isn't a test is lying and should be ignored.");

            enemy.SpeechText = text;
            scene.AddUIElement(text);


            scene.AddActor(player);
            scene.AddActor(testChild);
            //scene.AddActor(enemy);
            _currentSceneIndex = AddScene(scene);
            _scenes[_currentSceneIndex].Start();
        }

        /// <summary>
        /// Called everytime the game loops
        /// </summary>
        private void Update(float deltaTime)
        {
            _scenes[_currentSceneIndex].Update(deltaTime);


            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        /// <summary>
        /// Called every time the game loops to update visuals
        /// </summary>
        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);

            //Adds all actor icons to buffer
            _scenes[_currentSceneIndex].Draw();
            _scenes[_currentSceneIndex].DrawUI();

            Raylib.EndDrawing();
        }

        /// <summary>
        /// Called when the application exits
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
            Raylib.CloseWindow();
        }

        /// <summary>
        /// Adds a scene to the engine's scene array
        /// </summary>
        /// <param name="scene">The scene that will be added to the scene array</param>
        /// <returns>The index where the new scene is located</returns>
        public int AddScene(Scene scene)
        {
            //Create a new temporary array
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy all values from old array into the new array
            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            //Set the last index to be the new scene
            tempArray[_scenes.Length] = scene;

            //Set the old array to be the new array
            _scenes = tempArray;

            //Return the last index
            return _scenes.Length - 1;
        }

        /// <summary>
        /// Gets the next key in the input stream
        /// </summary>
        /// <returns>The key that was pressed</returns>
        public static ConsoleKey GetNextKey()
        {
            //If there is no key being pressed...
            if (!Console.KeyAvailable)
                //...return
                return 0;

            //Return the current key being pressed
            return Console.ReadKey(true).Key;
        }

        /// <summary>
        /// Ends the application
        /// </summary>
        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }
    }
}
