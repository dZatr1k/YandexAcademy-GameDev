using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Model
{
    public class EnemiesSpawner
    {
        private readonly EnemiesSimulation _simulation;
        private readonly Transformable _player;

        private readonly Func<Enemy>[] _variants;
        private readonly Timers<Func<Enemy>> _queue = new Timers<Func<Enemy>>();

        private List<Nlo> _blueTeam;
        private List<Nlo> _redTeam;

        public EnemiesSpawner(EnemiesSimulation simulation, Transformable player)
        {
            _simulation = simulation;
            _player = player;

            _variants = new Func<Enemy>[]
            {
                CreateAsteroid,
                CreateBlueMember,
                CreateRedMember
            };

            _blueTeam = new List<Nlo>(1);
            _redTeam = new List<Nlo>(1);
        }

        public void FillTestQueue()
        {
            for (int stacks = 0; stacks < 100; stacks++)
            {
                int countInStack = Random.Range(0, 2);

                while(countInStack-- > 0)
                    _queue.Start(_variants[0], stacks * 2, (factory) => _simulation.Simulate(factory.Invoke()));
            }

            for (int i = 1; i < 10; i++)
            {
                _queue.Start(_variants[1], i, (factory) => _simulation.Simulate(factory.Invoke()));
                _queue.Start(_variants[2], i, (factory) => _simulation.Simulate(factory.Invoke()));
            }
        }

        public void Update(float deltaTime)
        {
            _queue.Tick(deltaTime);
        }

        private Vector2 GetRandomPositionOutsideScreen()
        {
            return Random.insideUnitCircle.normalized + new Vector2(0.5F, 0.5F);
        }

        private Nlo CreateBlueMember()
        {
            _blueTeam.Add(new Nlo(TeamTag.blue, null, GetRandomPositionOutsideScreen(), Config.NloSpeed));
            return _blueTeam[_blueTeam.Count - 1];
        }

        private Nlo CreateRedMember()
        {
            _redTeam.Add(new Nlo(TeamTag.red, _blueTeam[_blueTeam.Count - 1], GetRandomPositionOutsideScreen(), Config.NloSpeed));
            _blueTeam[_blueTeam.Count - 1].SetTarget(_redTeam[_redTeam.Count - 1]);
            return _redTeam[_blueTeam.Count - 1];
        }

        private Asteroid CreateAsteroid()
        {
            Vector2 postion = GetRandomPositionOutsideScreen();
            Vector2 direction = GetDirectionThroughtScreen(postion);

            return new Asteroid(postion, direction, Config.AsteroidSpeed);
        }

        private static Vector2 GetDirectionThroughtScreen(Vector2 postion)
        {
            return (new Vector2(Random.value, Random.value) - postion).normalized;
        }
    }
}
