using UnityEditor;
using UnityEngine;
/// <summary>
/// �G�̎ˌ��p�^�[����ScriptableObject�̃G�f�B�^�[���g������N���X
/// </summary>
[CustomEditor(typeof(PlayerShotData))]
public class PlayerShotDateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerShotData enemyShotDate = target as PlayerShotData;

        // �ˌ��^�C�v�̎�ނ�ݒ�
        enemyShotDate.shotType = (PlayerBulletVectorCalculation.ShotType)EditorGUILayout.EnumPopup("�ˌ��̎��", enemyShotDate.shotType);

        // �e��
        enemyShotDate.BulletSpeed = Mathf.Max(0, EditorGUILayout.FloatField("�e��", enemyShotDate.BulletSpeed));

        // ��x�ɔ��˂���e�̐�
        enemyShotDate.OnceShotBullet = Mathf.Max(1, EditorGUILayout.IntField("��x�ɔ��˂���e���iN-Way�̏ꍇ�͗�)", enemyShotDate.OnceShotBullet));

        // ���ˊԊu����
        enemyShotDate.ShotIntervalTime = Mathf.Max(0.01f, EditorGUILayout.FloatField("���ˊԊu����", enemyShotDate.ShotIntervalTime));

        // N-Way�e
        if (enemyShotDate.shotType == PlayerBulletVectorCalculation.ShotType.N_Way)
        {
            // �ˌ��^�C�v�̎�ނ�ݒ�
            enemyShotDate.angleType = (PlayerBulletVectorCalculation.AngleType)EditorGUILayout.EnumPopup("�ˌ��̎��", enemyShotDate.angleType);

            // �ˌ��͈�
            if (enemyShotDate.OnceShotBullet > 1)
            {
                enemyShotDate.AngleRange = Mathf.Max(1, EditorGUILayout.FloatField("�ˌ��͈�", enemyShotDate.AngleRange));
            }

            // Straight�ˌ��̏ꍇ�A���ˊp�x���w��
            if (enemyShotDate.angleType == PlayerBulletVectorCalculation.AngleType.Fixed)
            {
                enemyShotDate.ShotAngle = Mathf.Max(0, EditorGUILayout.FloatField("���ˊp�x", enemyShotDate.ShotAngle));
            }
        }

        // �����_���e
        if (enemyShotDate.shotType == PlayerBulletVectorCalculation.ShotType.Random)
        {
            // �ˌ��^�C�v�̎�ނ�ݒ�
            enemyShotDate.angleType = (PlayerBulletVectorCalculation.AngleType)EditorGUILayout.EnumPopup("�ˌ��̎��", enemyShotDate.angleType);

            // �ˌ��͈�
            enemyShotDate.AngleRange = Mathf.Max(1, EditorGUILayout.FloatField("�ˌ��͈�", enemyShotDate.AngleRange));

            // Straight�ˌ��̏ꍇ�A���ˊp�x���w��
            if (enemyShotDate.angleType == PlayerBulletVectorCalculation.AngleType.Fixed)
            {
                enemyShotDate.ShotAngle = Mathf.Max(0, EditorGUILayout.FloatField("���ˊp�x", enemyShotDate.ShotAngle));
            }
        }

        // ��]�e
        if (enemyShotDate.shotType == PlayerBulletVectorCalculation.ShotType.Spin)
        {
            // 1�������������̊p�x�ω���
            enemyShotDate.SpinAngleShift = EditorGUILayout.FloatField("�p�x�ω���", enemyShotDate.SpinAngleShift);
        }

        // �e�̎��
        enemyShotDate.BulletData = (PlayerBulletData)EditorGUILayout.ObjectField("�e�̎��", enemyShotDate.BulletData, typeof(PlayerBulletData), false);

        EditorUtility.SetDirty(target);
    }

}