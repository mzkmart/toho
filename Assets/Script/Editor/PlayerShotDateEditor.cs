using UnityEditor;
using UnityEngine;
/// <summary>
/// GΜΛp^[ΜScriptableObjectΜGfB^[πg£·ιNX
/// </summary>
[CustomEditor(typeof(PlayerShotData))]
public class PlayerShotDateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerShotData enemyShotDate = target as PlayerShotData;

        // Λ^CvΜνήπέθ
        enemyShotDate.shotType = (BulletVectorCalculation.ShotType)EditorGUILayout.EnumPopup("ΛΜνή", enemyShotDate.shotType);

        // e¬
        enemyShotDate.BulletSpeed = Mathf.Max(0, EditorGUILayout.FloatField("e¬", enemyShotDate.BulletSpeed));

        // κxΙ­Λ·ιeΜ
        enemyShotDate.OnceShotBullet = Mathf.Max(1, EditorGUILayout.IntField("κxΙ­Λ·ιeiN-WayΜκΝρ)", enemyShotDate.OnceShotBullet));

        // ­ΛΤuΤ
        enemyShotDate.ShotIntervalTime = Mathf.Max(0.01f, EditorGUILayout.FloatField("­ΛΤuΤ", enemyShotDate.ShotIntervalTime));

        // N-Waye
        if (enemyShotDate.shotType == BulletVectorCalculation.ShotType.N_Way)
        {
            // Λ^CvΜνήπέθ
            enemyShotDate.angleType = (BulletVectorCalculation.AngleType)EditorGUILayout.EnumPopup("ΛΜνή", enemyShotDate.angleType);

            // ΛΝΝ
            if (enemyShotDate.OnceShotBullet > 1)
            {
                enemyShotDate.AngleRange = Mathf.Max(1, EditorGUILayout.FloatField("ΛΝΝ", enemyShotDate.AngleRange));
            }

            // StraightΛΜκA­Λpxπwθ
            if (enemyShotDate.angleType == BulletVectorCalculation.AngleType.Fixed)
            {
                enemyShotDate.ShotAngle = Mathf.Max(0, EditorGUILayout.FloatField("­Λpx", enemyShotDate.ShotAngle));
            }
        }

        // _e
        if (enemyShotDate.shotType == BulletVectorCalculation.ShotType.Random)
        {
            // Λ^CvΜνήπέθ
            enemyShotDate.angleType = (BulletVectorCalculation.AngleType)EditorGUILayout.EnumPopup("ΛΜνή", enemyShotDate.angleType);

            // ΛΝΝ
            enemyShotDate.AngleRange = Mathf.Max(1, EditorGUILayout.FloatField("ΛΝΝ", enemyShotDate.AngleRange));

            // StraightΛΜκA­Λpxπwθ
            if (enemyShotDate.angleType == BulletVectorCalculation.AngleType.Fixed)
            {
                enemyShotDate.ShotAngle = Mathf.Max(0, EditorGUILayout.FloatField("­Λpx", enemyShotDate.ShotAngle));
            }
        }

        // ρ]e
        if (enemyShotDate.shotType == BulletVectorCalculation.ShotType.Spin)
        {
            // 1­ͺΑ½ΜpxΟ»Κ
            enemyShotDate.SpinAngleShift = EditorGUILayout.FloatField("pxΟ»Κ", enemyShotDate.SpinAngleShift);
        }

        // eΜνή
        enemyShotDate.BulletData = (BulletSpriteData)EditorGUILayout.ObjectField("eΜνή", enemyShotDate.BulletData, typeof(BulletSpriteData), false);

        EditorUtility.SetDirty(target);
    }

}