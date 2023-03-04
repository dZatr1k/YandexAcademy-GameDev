using System;
using UnityEngine;

namespace Asteroids.Model
{
    public enum TeamTag
    {
        none,
        blue,
        red
    }

    public class Nlo : Enemy
    {
        private readonly TeamTag _tag;
        private readonly float _speed;

        private Transformable _target;

        public Nlo(Transformable target, Vector2 position, float speed) : base(position, 0)
        {
            _tag = TeamTag.none;
            _target = target;
            _speed = speed;
        }
        public Nlo(TeamTag tag, Transformable target, Vector2 position, float speed) : base(position, 0)
        {
            _tag = tag;
            _target = target;
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            Position = Vector2.MoveTowards(Position, _target.Position, _speed * deltaTime);
            LookAt(_target.Position);
        }

        public void SetTarget(Transformable target)
        {
            _target = target;
        }

        public TeamTag GetTeamTag() => _tag;

        private void LookAt(Vector2 point)
        {
            Rotate(Vector2.SignedAngle(Quaternion.Euler(0, 0, Rotation) * Vector3.up, (Position - point)));
        }
    }
}
