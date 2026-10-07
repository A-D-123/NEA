using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using System.Linq;

namespace NEA___2D_Physics_Simulator
{

    internal class Program
    {
        public static SimulationConfig Config = new();
        static void Main(string[] args)
        {
            List<PhysicsObject> objects = new();

            Raylib.InitWindow(1500, 1000, "2D Physics Simulator");
            Raylib.SetTargetFPS(120);
            objects.Add(new(new(30, 40), new(60, 1)));
            objects.Last().Moveable = false;
            int totalCollisions = 0;
            while (!Raylib.WindowShouldClose())
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
                    objects.Add(new PhysicsObject(Raylib.GetMousePosition() / Config.PPM, new(2, 2)));
                foreach (PhysicsObject obj in objects)
                    obj.UpdatePhysics(Raylib.GetFrameTime());
                totalCollisions += CollisionDetection(objects);
                GenerateNewFrame(objects, totalCollisions);
            }

            Raylib.CloseWindow();
        }
        static void GenerateNewFrame(List<PhysicsObject> objects, int totalCollisions)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);
            DrawObjects(objects);
            ShowDevUI(objects, totalCollisions);
            Raylib.EndDrawing();
        }
        static void DrawObjects(List<PhysicsObject> objects)
        {
            foreach (PhysicsObject obj in objects)
                Raylib.DrawRectangleV(obj.Position * Config.PPM, obj.Size * Config.PPM, Color.RED);
        }
        static void ShowDevUI(List<PhysicsObject> objects, int totalCollisions)
        {
            Raylib.DrawText("FPS = " + Raylib.GetFPS().ToString(), 5, 5, 20, Color.BLACK);
            Raylib.DrawText("Obj Count = " + objects.Count.ToString(), 5, 30, 20, Color.BLACK);
            Raylib.DrawText("Collision Count = " + totalCollisions.ToString(), 5, 55, 20, Color.BLACK);
        }
        static int CollisionDetection(List<PhysicsObject> objects)
        {
            List<PhysicsObject> collidingObjects = new();
            foreach (PhysicsObject obj in objects)
            {
                collidingObjects.AddRange(objects.FindAll(collObj => collObj != obj &&
                obj.Position.X + obj.Size.X >= collObj.Position.X &&
                obj.Position.Y + obj.Size.Y >= collObj.Position.Y &&
                collObj.Position.X + collObj.Size.X >= obj.Position.X &&
                collObj.Position.Y + collObj.Size.Y >= obj.Position.Y));
            }
            int collisions = 0;
            foreach (PhysicsObject obj in collidingObjects) collisions++;
            return collisions;
        }
    }
}
