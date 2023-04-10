using UnityEngine;

public class EnemyMove : EnemyBase
{
    // ���ˊԊu���v�邽�߂̕ϐ�
    private float shotCountTime = default;

    void FixedUpdate()
    {
        shotCountTime += Time.deltaTime;
        // �o������̎��Ԃ��J�E���g
        appeardTime += Time.deltaTime;

        EnemyMove(movePattern, speed, appeardTime);

        if (_isShot)
        {
            if (bulletShotInterval < shotCountTime &&
                appeardTime > shotData.ShotStartTime &&
                appeardTime < shotData.ShotEndTime)
            {
                base.EnemyShot(shotData, enemyBulletData, shotUpdateCount);

                shotUpdateCount++;
                shotCountTime = 0;
                bulletShotInterval = shotData.ShotIntervalTime;
            }
        }
    }

    public void OnEnableInitLoad(EnemyWaveData enemyWaveData, int enemyDataNumber)
    {
        // ���݂̃A�j���[�V�����p�Ɏg�p����摜�f�[�^�z���������
        loadSpriteArray = SpriteArray.idle;

        // ���ԃJ�E���^�[��������
        appeardTime = 0;
        shotCountTime = 0;

        // �l�������Ă���
        // ��{�p�����[�^
        hp = enemyWaveData.enemyData[enemyDataNumber].Hp; // �̗�
        movePattern = enemyWaveData.enemyData[enemyDataNumber].MovePattern; // �ړ��p�^�[��
        speed = enemyWaveData.enemyData[enemyDataNumber].Speed; // ����

        // �摜�Ɣ���̃p�����[�^
        EnemySpriteData enemySprite = enemyWaveData.enemyData[enemyDataNumber].EnemySpriteData;

        if (enemySprite != null)
        {
            offset = enemySprite.Offset; // �摜�Ɣ�������炷�ړ���
            idleImagePattern = enemySprite.EnemyImageIdle; // ���i���̉摜�z��
            rightImagePattern = enemySprite.EnemyImageRightMove; // ���ړ����̉摜�z��
        }
        else
        {
            // �f�[�^��null�̏ꍇ������
            offset = default;
            idleImagePattern = default;
            rightImagePattern = default;
        }

        // �ˌ��p�̉摜�E�p�����[�^
        shotData = enemyWaveData.enemyData[enemyDataNumber].ShotData;

        if (shotData != null)
        {
            bulletSpeed = shotData.BulletSpeed; // �ˌ�����e�̑���
            bulletColor = shotData.BulletColorType; // �ˌ�����e�̐F
            bulletShotInterval = shotData.ShotIntervalTime; // �e�̔��ˊԊu
            enemyBulletData = shotData.BulletData; // ���˂���e�̏��
            _isShot = true;
        }
        else
        {
            // �f�[�^��null�̏ꍇ������
            bulletSpeed = default;
            bulletColor = default;
            bulletShotInterval = default;
            enemyBulletData = default;
            _isShot = false;
        }

        // �����ʒu�𔽉f����ہA0���t�B�[���h�̒����ɂȂ�悤�ɒ���
        this.transform.position = new Vector2(enemyWaveData.enemyData[enemyDataNumber].AppearancePositionX,
            enemyWaveData.enemyData[enemyDataNumber].AppearancePositionY);
    }
}
