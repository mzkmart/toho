using UnityEngine;
/// <summary>
/// 敵の移動パターン
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
    /// EnemyのFixedUpdateで呼ぶパターン
    /// </summary>
    /// <param name="transform">Transformコンポーネント</param>
    /// <param name="time">出現からの時間</param>
    /// <param name="pattern">パターン</param>
    /// <returns></returns>
    public static (float addPositionX, float addPositionY) MovePatternLoad(Transform transform, float time, int pattern)
    {
        float positionX = default;
        float positionY = default;

        switch (pattern)
        {
            case (int)Pattern.Stay:

                // 動かないよ

                break;

            case (int)Pattern.Down:

                // 真下
                if (time > 1f)
                {
                    positionY -= 0.01f;
                }

                break;

            case (int)Pattern.Left:

                // 左
                if (time > 1f)
                {
                    positionX -= 0.01f;
                }
                break;

            case (int)Pattern.Right:

                // 右
                if (time > 1f)
                {
                    positionX += 0.01f;
                }
                break;

            case (int)Pattern.LeftDown:

                // 左下
                if (time > 1f)
                {
                    positionX -= 0.01f;
                    positionY -= 0.01f;
                }
                break;

            case (int)Pattern.RightDown:

                // 右下
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
