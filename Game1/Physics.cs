using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    public class Physics
    {
        private int _maxX;
        private int _maxY;
        private int _floor;
        private Vector2 _gravity;
        private int _clipping;

        public Physics(int floor, int maxX, int maxY, float gravity)
        {
            _maxX = maxX;
            _maxY = maxY;
            _floor = floor;
            _gravity = new Vector2(0, gravity);
        }
        public int Clipping
        {
            get { return _clipping; }
        }
        public void ProcessPhysics(IEnumerable<Animal> animals)
        {
            foreach (var animal in animals)
            {
                foreach (var m in animal.Muscles)
                {
                    m.PosC = animal.Nodes[m.NodeC].Position;
                    m.PosP = animal.Nodes[m.NodeP].Position;
                    if (m.OscRange > 0)
                    {
                        m.OscState += (m.OscDirection ? -1 : 1) * m.OscSpeed;
                        if (m.OscState >= m.OscRange)
                        {
                            m.OscState = m.OscRange;
                            m.OscDirection = !m.OscDirection;
                        }
                        if (m.OscState <= 0)
                        {
                            m.OscState = 0;
                            m.OscDirection = !m.OscDirection;
                        }
                        m.Length = m.LengthAlpha + m.OscState;
                    }
                }
                foreach (Node node in animal.Nodes)
                {
                    Vector2 force = FindGravityForce(node);
                    force += FindMuscleForce(node);
                    force += FindFrictionForce(node);
                    node.Velocity += force / node.Weight;
                    node.Position += node.Velocity;
                    ApplyWalls(node);
                }
            }
        }
        private void ApplyWalls(Node node)
        {
            if (node.Position.X <= 1)
            {
                node.Velocity.X = (float)Math.Abs(node.Velocity.X * 0.5);
                node.Position.X = 5;
            }
            if (node.Position.X >= _maxX)
            {
                node.Velocity.X = (float)Math.Abs(node.Velocity.X * 0.5) * -1;
                node.Position.X = _maxX - 5;
            }
            if (node.Position.Y <= 0)
            {
                node.Velocity.Y = (float)Math.Abs(node.Velocity.Y * 0.5);
                node.Position.Y = 5;
            }
            if (node.Position.Y >= _floor)
            {
                node.Position.Y = _floor;
                if (node.Velocity.Y > 0)
                {
                    node.Velocity.Y = 0;
                }
                node.Velocity.X = (float)(node.Velocity.X / (Math.Sqrt(node.Weight) / 5 + 1));
            }
        }
        private Vector2 FindGravityForce(Node node)
        {
            return _gravity * node.Weight;
        }
        private Vector2 FindMuscleForce(Node node)
        {
            Vector2 force = new Vector2(0, 0);
            foreach (var m in node.Muscles)
            {
                Vector2 a;
                if (m.NodeC == node.ID)
                {
                    a = m.PosC - m.PosP;
                }
                else
                {
                    a = m.PosP - m.PosC;
                }

                var delta = ComputeDelta(a, m);
                var forceMagnitude = ComputeMagnitude(delta, m.Strength);
                var forceVector = ComputeVector(a, forceMagnitude);
                while (forceVector.Length() > 150)
                {
                    forceVector = forceVector / 10;
                    _clipping++;
                }

                force += forceVector;
            }
            return force;
        }
        private Vector2 ComputeVector(Vector2 a, float magnitude)
        {
            return a * magnitude;
        }
        private float ComputeDelta(Vector2 a, Muscle m)
        {
            return m.Length - a.Length();
        }
        private float ComputeMagnitude(float delta, byte strength)
        {
            var pressure = Math.Pow(delta * strength / 100, 3);
            return (float)(pressure / 500000);
        }
        private Vector2 FindFrictionForce(Node node)
        {
            return node.Velocity * -5;
        }
    }
}
