using Lion.Player;
using System;
using UnityEngine;

namespace Lion.Enemy.Boss
{
    /// <summary>
    /// �ߐڍU���I�u�W�F�N�g�B
    /// </summary>
    public class MeleeAttackObject : MonoBehaviour
    {
        public Stage1BossParameters Parameters { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �v���C���[�ɓ���������_���[�W��^���鏈���������ɏ����B
        }
    }
}