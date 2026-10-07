using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NEA___2D_Physics_Simulator
{
    internal class PhysicsObject
    {
        public bool Moveable { get; set; } = true;
        public float Mass { get; set; } = 10;
        public Vector2 Force = Vector2.Zero;
        public Vector2 Velocity = Vector2.Zero;
        public Vector2 Position;
        public Vector2 Size;
        public PhysicsObject(Vector2 pos, Vector2 size)
        {
            Position = pos - (size / 2);
            Size = size; 
        }
        public void UpdatePhysics(float deltaTime)
        {
            Force = Vector2.Zero;
            if (Moveable) Force += CalcEnvironmentalForces(deltaTime);
            Vector2 Accel = Force / Mass;
            Position += deltaTime * (Velocity + 0.5f * Accel * deltaTime);
            Velocity += Accel * deltaTime;
        }
        private Vector2 CalcEnvironmentalForces(float deltaTime)
        {
            Vector2 Force = Vector2.Zero;
            Force += new Vector2(0, Mass * Program.Config.Gravity);

            
            return Force;
        }
    }
}
