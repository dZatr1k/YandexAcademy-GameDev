using System;
using UnityEngine;

namespace Asteroids.Model
{
    public class Ship : Transformable
    {
        private int _health;
        private bool _isDestroyed;

        public Ship(Vector2 position, float rotation, int health) : base(position, rotation) 
        {
            _health = health;
        }

        public void TakeDamage() 
        {
            _health--;

            if(_health == 0)
                _isDestroyed = true;
        }

        public bool GetIsDestroyed()
        {
            return _isDestroyed;
        }

        public void MoveLooped(Vector2 delta)
        {
            var nextPosition = Position + delta;

            nextPosition.x = Mathf.Repeat(nextPosition.x, 1);
            nextPosition.y = Mathf.Repeat(nextPosition.y, 1);

            Position = nextPosition;
        }
    }
}
