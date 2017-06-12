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
            Animal fittest = null;
            foreach (var animal in animals)
            {
                animal.Fitness = 0;// rnd.Next(0, 255);
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
                    node.Speed += force / node.Weight;
                    node.Position += node.Speed;

                    if (node.Position.X <= 1)
                    {
                        node.Speed.X = (float)Math.Abs(node.Speed.X * 0.5);
                        node.Position.X = 5;
                    }
                    if (node.Position.X >= _maxX)
                    {
                        node.Speed.X = (float)Math.Abs(node.Speed.X * 0.5) * -1;
                        node.Position.X = _maxX - 5;
                    }
                    if (node.Position.Y <= 0)
                    {
                        node.Speed.Y = (float)Math.Abs(node.Speed.Y * 0.5);
                        node.Position.Y = 5;
                    }
                    if (node.Position.Y >= _floor)
                    {
                        node.Position.Y = _floor;
                        if (node.Speed.Y > 0)
                        {
                            node.Speed.Y = 0;
                        }
                        node.Speed.X = (float)(node.Speed.X / (Math.Sqrt(node.Weight) / 5 + 1));

                    }
                }
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
                var a = m.PosP - m.PosC;
                if (m.NodeC == node.ID)
                {
                    a = m.PosC - m.PosP;
                }

                var currentLength = a.Length();
                var delta = Math.Abs(currentLength - m.Length);
                var forceMagnitude = ComputeForce(delta, m.Strength);
                var forceVector = new Vector2((a.X * forceMagnitude), (a.Y * forceMagnitude));
                if (float.IsInfinity(forceVector.X) || float.IsInfinity(forceVector.Y))
                {
                    forceVector = new Vector2(0, 0);
                }
                while (forceVector.Length() > 150)
                {
                    forceVector = forceVector / 10;
                    _clipping++;
                }
                var contract = (m.Length < currentLength);
                force += (forceVector * (contract ? -1 : 1));
            }
            return force;
        }
        private float ComputeForce(float delta, byte strength)
        {            
            var pressure = Math.Pow(delta / 3, 2);
            var balance = (500000 / strength);
            return (float)(pressure / balance);
        }
        private Vector2 FindFrictionForce(Node node)
        {
            return node.Speed * -5;
        }
    }
}
