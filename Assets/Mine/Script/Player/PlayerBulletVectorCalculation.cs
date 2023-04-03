using UnityEngine;

/// <summary>
/// 射撃パターンデータ
/// </summary>
public static class PlayerBulletVectorCalculation
{
    // 一定方向に射撃する際に初期方向を下に調整する為の定数
    private const float STRAIGHT_SHOT_ADJUST = 90f;

    /// <summary>
    /// 打ち方の種類
    /// </summary>
    public enum ShotType
    {
        N_Way,
        Random,
        Spin,
        Up
    }

    /// <summary>
    /// 弾道の種類
    /// </summary>
    public enum AngleType
    {
        PlayerAim,
        Fixed
    }

    /// <summary>
    /// 弾の発射方向の計算
    /// </summary>
    /// <param name="bulletTransform">弾のトランスフォーム</param>
    /// <param name="shotDate">射撃データ</param>
    /// <param name="bulletNumber">一回に発射する弾の内の番号</param>
    /// <param name="shotUpdateCount">射撃した回数</param>
    /// <returns>弾の発射方向のVector2</returns>
    public static Vector2 BulletVector(Vector2 bulletTransform, PlayerShotData shotDate, int bulletNumber, int shotUpdateCount)
    {
        Vector2 moveDirection = default;
        float playerRad = default;
        float angleShift = default;

        switch (shotDate.shotType)
        {
            case ShotType.N_Way:

                // プレイヤー方向のラジアンを取得
                if (shotDate.angleType == AngleType.PlayerAim)
                {
                    // N-Way用のずらす分のラジアンを取得
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

                // プレイヤー方向のラジアンを取得
                if (shotDate.angleType == AngleType.PlayerAim)
                {
                    // ずらす分のラジアンを取得
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

                // 下を正面とみて、回転するようにずらす
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

        // ラジアンから進行方向を設定
        moveDirection = new Vector2(Mathf.Cos(playerRad - angleShift), Mathf.Sin(playerRad - angleShift));
        return moveDirection;
    }

    /// <summary>
    /// プレイヤーの方向を取得して進行方向に設定する
    /// </summary>
    /// <param name="bulletTransform">弾のポジション</param>
    /// <returns></returns>
    private static float GetPlayerRad(Vector2 bulletTransform)
    {
        float moveVectorX;
        float moveVectorY;

        float rad;

        // プレイヤーの位置を取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector2 playerPos = player.transform.position;
        Vector2 playerOffset = default;

        // プレイヤーを原点として進む方向を求める
        moveVectorX = (playerPos.x + playerOffset.x) - bulletTransform.x;
        moveVectorY = (playerPos.y + playerOffset.y) - bulletTransform.y;

        // 角度をラジアンに変換
        rad = Mathf.Atan2(moveVectorY, moveVectorX);

        return rad;
    }

    private static float WayShotAngleShift(int way, float angleRange, int bulletNumber)
    {
        // 弾に渡す角度の計算
        if (way > 1)
        {
            // 決められた角度の中からWAYの数だけ均等に割り、角度を決める
            return ((angleRange / (way - 1)) * bulletNumber) - (angleRange / 2);
        }
        else
        {
            // 真ん中の角度を返す
            return 0;
        }
    }

    private static float RandomShotAngleShift(float angleRange)
    {
        //弾に渡す角度の計算
        return Random.Range(0, angleRange) - (angleRange / 2);
    }

    private static float SpinShotAngleShift(int way, int bulletNumber, float interval, int shotUpdateCount)
    {
        //弾に渡す角度の計算
        return ((360 / way) * bulletNumber) - (360 / 2) + (interval * shotUpdateCount);
    }
}
