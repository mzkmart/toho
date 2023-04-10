using UnityEngine;
/// <summary>
/// �G�̈ړ��p�^�[��
/// </summary>
public static class EnemyMovePattern
{
    public enum Pattern : int
    {
        Stay,
        Down,
        Right,
        Left,
        RightDown,
        LeftDown,
    }

    /// <summary>
    /// Enemy��FixedUpdate�ŌĂԃp�^�[��
    /// </summary>
    /// <param name="transform">Transform�R���|�[�l���g</param>
    /// <param name="time">�o������̎���</param>
    /// <param name="pattern">�p�^�[��</param>
    /// <returns></returns>
    public static (float addPositionX, float addPositionY) MovePatternLoad(Transform transform, float time, int pattern)
    {
        float positionX = default;
        float positionY = default;

        switch (pattern)
        {
            case (int)Pattern.Stay:

                // �����Ȃ���

                break;

            case (int)Pattern.Down:

                // �^��
                if (time > 1f)
                {
                    positionY -= 0.01f;
                }

                break;

            case (int)Pattern.Left:

                // ��
                if (time > 1f)
                {
                    positionX -= 0.01f;
                }
                break;

            case (int)Pattern.Right:

                // �E
                if (time > 1f)
                {
                    positionX += 0.01f;
                }
                break;

            case (int)Pattern.LeftDown:

                // ����
                if (time > 1f)
                {
                    positionX -= 0.01f;
                    positionY -= 0.01f;
                }
                break;

            case (int)Pattern.RightDown:

                // �E��
                if (time > 1f)
                {
                    positionX += 0.01f;
                    positionY -= 0.01f;
                }
                break;

            default:
                break;
        }

        return (positionX, positionY);
    }
}
