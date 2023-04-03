using UnityEngine;

/// <summary>
/// �ˌ��p�^�[���f�[�^
/// </summary>
public static class PlayerBulletVectorCalculation
{
    // �������Ɏˌ�����ۂɏ������������ɒ�������ׂ̒萔
    private const float STRAIGHT_SHOT_ADJUST = 90f;

    /// <summary>
    /// �ł����̎��
    /// </summary>
    public enum ShotType
    {
        N_Way,
        Random,
        Spin,
        Up
    }

    /// <summary>
    /// �e���̎��
    /// </summary>
    public enum AngleType
    {
        PlayerAim,
        Fixed
    }

    /// <summary>
    /// �e�̔��˕����̌v�Z
    /// </summary>
    /// <param name="bulletTransform">�e�̃g�����X�t�H�[��</param>
    /// <param name="shotDate">�ˌ��f�[�^</param>
    /// <param name="bulletNumber">���ɔ��˂���e�̓��̔ԍ�</param>
    /// <param name="shotUpdateCount">�ˌ�������</param>
    /// <returns>�e�̔��˕�����Vector2</returns>
    public static Vector2 BulletVector(Vector2 bulletTransform, PlayerShotData shotDate, int bulletNumber, int shotUpdateCount)
    {
        Vector2 moveDirection = default;
        float playerRad = default;
        float angleShift = default;

        switch (shotDate.shotType)
        {
            case ShotType.N_Way:

                // �v���C���[�����̃��W�A�����擾
                if (shotDate.angleType == AngleType.PlayerAim)
                {
                    // N-Way�p�̂��炷���̃��W�A�����擾
                    angleShift = WayShotAngleShift(shotDate.OnceShotBullet, shotDate.AngleRange, bulletNumber);
                    playerRad = GetPlayerRad(bulletTransform);
                }
                else
                {
                    angleShift = (WayShotAngleShift(shotDate.OnceShotBullet, shotDate.AngleRange, bulletNumber)
                        + STRAIGHT_SHOT_ADJUST + shotDate.ShotAngle);
                }

                break;

            case ShotType.Random:

                // �v���C���[�����̃��W�A�����擾
                if (shotDate.angleType == AngleType.PlayerAim)
                {
                    // ���炷���̃��W�A�����擾
                    angleShift = RandomShotAngleShift(shotDate.AngleRange);
                    playerRad = GetPlayerRad(bulletTransform);
                }
                else
                {
                    angleShift = (RandomShotAngleShift(shotDate.AngleRange)
                        + STRAIGHT_SHOT_ADJUST + shotDate.ShotAngle);
                }

                break;

            case ShotType.Spin:

                // ���𐳖ʂƂ݂āA��]����悤�ɂ��炷
                angleShift = (SpinShotAngleShift(shotDate.OnceShotBullet, bulletNumber, shotDate.SpinAngleShift, shotUpdateCount)
                + STRAIGHT_SHOT_ADJUST + shotDate.ShotAngle);

                break;

            case ShotType.Up:
                moveDirection = new Vector3(0, 1, 0);
                return moveDirection;

            default:
                break;
        }
        angleShift *= Mathf.Deg2Rad;

        // ���W�A������i�s������ݒ�
        moveDirection = new Vector2(Mathf.Cos(playerRad - angleShift), Mathf.Sin(playerRad - angleShift));
        return moveDirection;
    }

    /// <summary>
    /// �v���C���[�̕������擾���Đi�s�����ɐݒ肷��
    /// </summary>
    /// <param name="bulletTransform">�e�̃|�W�V����</param>
    /// <returns></returns>
    private static float GetPlayerRad(Vector2 bulletTransform)
    {
        float moveVectorX;
        float moveVectorY;

        float rad;

        // �v���C���[�̈ʒu���擾
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector2 playerPos = player.transform.position;
        Vector2 playerOffset = default;

        // �v���C���[�����_�Ƃ��Đi�ޕ��������߂�
        moveVectorX = (playerPos.x + playerOffset.x) - bulletTransform.x;
        moveVectorY = (playerPos.y + playerOffset.y) - bulletTransform.y;

        // �p�x�����W�A���ɕϊ�
        rad = Mathf.Atan2(moveVectorY, moveVectorX);

        return rad;
    }

    private static float WayShotAngleShift(int way, float angleRange, int bulletNumber)
    {
        // �e�ɓn���p�x�̌v�Z
        if (way > 1)
        {
            // ���߂�ꂽ�p�x�̒�����WAY�̐������ϓ��Ɋ���A�p�x�����߂�
            return ((angleRange / (way - 1)) * bulletNumber) - (angleRange / 2);
        }
        else
        {
            // �^�񒆂̊p�x��Ԃ�
            return 0;
        }
    }

    private static float RandomShotAngleShift(float angleRange)
    {
        //�e�ɓn���p�x�̌v�Z
        return Random.Range(0, angleRange) - (angleRange / 2);
    }

    private static float SpinShotAngleShift(int way, int bulletNumber, float interval, int shotUpdateCount)
    {
        //�e�ɓn���p�x�̌v�Z
        return ((360 / way) * bulletNumber) - (360 / 2) + (interval * shotUpdateCount);
    }
}
