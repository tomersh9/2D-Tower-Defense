using System.Collections.Generic;
using Managers;
using Tower_Related;
using UnityEngine;

namespace Enemy_Related {
    public class EnemyPathing : MonoBehaviour {
        private Enemy _enemy;
        private List<Transform> _wayPoints;
        private Transform _target;
        private int _pathIndex = 0;

        private GameManager _gameManager;
        private DamageDealer _damageDealer;

        private void Awake() {
            _gameManager = GameManager.GetInstance();
            _damageDealer = GetComponent<DamageDealer>();
            _enemy = GetComponent<Enemy>();
            _wayPoints = FindObjectOfType<Path>().GetPath();
        }

        // Start is called before the first frame update
        void Start() {
            _target = _wayPoints[_pathIndex]; //first waypoint in the list
        }

        // Update is called once per frame
        void Update() {
            MoveToTarget();
        }

        private void MoveToTarget() {
            if (_pathIndex < _wayPoints.Count) {
                //move towards the point
                _target = _wayPoints[_pathIndex];
                transform.position = Vector2.MoveTowards(transform.position, _target.position,
                    _enemy.GetMoveSpeed() * Time.deltaTime);
                CheckDistanceToNextPoint();
            }
            else {
                _gameManager.TakeDamage(_damageDealer.GetDamage()); //player health goes down
                _enemy.Die();
            }
        }

        private void CheckDistanceToNextPoint() {
            if (Vector2.Distance(transform.position, _target.position) <= 0.2f) {
                _target = _wayPoints[_pathIndex++];
            }
        }
    }
}