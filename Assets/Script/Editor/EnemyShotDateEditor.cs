using UnityEditor;
using UnityEngine;
/// <summary>
/// 敵の射撃パターンのScriptableObjectのエディターを拡張するクラス
/// </summary>
[CustomEditor(typeof(EnemyShotData))]
public class EnemyShotDateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EnemyShotData enemyShotDate = target as EnemyShotData;

        // 射撃タイプの種類を設定
        enemyShotDate.shotType = (BulletVectorCalculation.ShotType)EditorGUILayout.EnumPopup("射撃の種類", enemyShotDate.shotType);

        // 弾速
        enemyShotDate.BulletSpeed = Mathf.Max(0, EditorGUILayout.FloatField("弾速", enemyShotDate.BulletSpeed));

        // 一度に発射する弾の数
        enemyShotDate.OnceShotBullet = Mathf.Max(1, EditorGUILayout.IntField("一度に発射する弾数（N-Wayの場合は列数)", enemyShotDate.OnceShotBullet));

        // 発射間隔時間
        enemyShotDate.ShotIntervalTime = Mathf.Max(0.01f, EditorGUILayout.FloatField("発射間隔時間", enemyShotDate.ShotIntervalTime));

        // 射撃開始・終了時間
        enemyShotDate.ShotStartTime = Mathf.Max(0, EditorGUILayout.FloatField("射撃開始までの時間", enemyShotDate.ShotStartTime));
        enemyShotDate.ShotEndTime = Mathf.Max(0, EditorGUILayout.FloatField("射撃終了までの時間", enemyShotDate.ShotEndTime));

        // N-Way弾
        if (enemyShotDate.shotType == BulletVectorCalculation.ShotType.N_Way)
        {
            // 射撃タイプの種類を設定
            enemyShotDate.angleType = (BulletVectorCalculation.AngleType)EditorGUILayout.EnumPopup("射撃の種類", enemyShotDate.angleType);

            // 射撃範囲
            if (enemyShotDate.OnceShotBullet > 1)
            {
                enemyShotDate.AngleRange = Mathf.Max(1, EditorGUILayout.FloatField("射撃範囲", enemyShotDate.AngleRange));
            }

            // Straight射撃の場合、発射角度を指定
            if (enemyShotDate.angleType == BulletVectorCalculation.AngleType.Fixed)
            {
                enemyShotDate.ShotAngle = Mathf.Max(0, EditorGUILayout.FloatField("発射角度", enemyShotDate.ShotAngle));
            }
        }

        // ランダム弾
        if (enemyShotDate.shotType == BulletVectorCalculation.ShotType.Random)
        {
            // 射撃タイプの種類を設定
            enemyShotDate.angleType = (BulletVectorCalculation.AngleType)EditorGUILayout.EnumPopup("射撃の種類", enemyShotDate.angleType);

            // 射撃範囲
            enemyShotDate.AngleRange = Mathf.Max(1, EditorGUILayout.FloatField("射撃範囲", enemyShotDate.AngleRange));

            // Straight射撃の場合、発射角度を指定
            if (enemyShotDate.angleType == BulletVectorCalculation.AngleType.Fixed)
            {
                enemyShotDate.ShotAngle = Mathf.Max(0, EditorGUILayout.FloatField("発射角度", enemyShotDate.ShotAngle));
            }
        }

        // 回転弾
        if (enemyShotDate.shotType == BulletVectorCalculation.ShotType.Spin)
        {
            // 1発分撃った時の角度変化量
            enemyShotDate.SpinAngleShift = EditorGUILayout.FloatField("角度変化量", enemyShotDate.SpinAngleShift);
        }

        // 弾の種類
        enemyShotDate.BulletData = (BulletSpriteData)EditorGUILayout.ObjectField("弾の種類", enemyShotDate.BulletData, typeof(BulletSpriteData), false);

        // 弾の色
       // enemyShotDate.BulletColorType = Mathf.Clamp(EditorGUILayout.IntField("弾の色(弾データ内の配列の番号)", enemyShotDate.BulletColorType), 0, enemyShotDate.BulletData.BulletImage.Length - 1);

        EditorUtility.SetDirty(target);
    }

}