using UnityEditor;
using UnityEngine;
/// <summary>
/// �G�̎ˌ��p�^�[����ScriptableObject�̃G�f�B�^�[���g������N���X
/// </summary>
[CustomEditor(typeof(EnemyShotData))]
public class EnemyShotDateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EnemyShotData enemyShotDate = target as EnemyShotData;

        // �ˌ��^�C�v�̎�ނ�ݒ�
        enemyShotDate.shotType = (BulletVectorCalculation.ShotType)EditorGUILayout.EnumPopup("�ˌ��̎��", enemyShotDate.shotType);

        // �e��
        enemyShotDate.BulletSpeed = Mathf.Max(0, EditorGUILayout.FloatField("�e��", enemyShotDate.BulletSpeed));

        // ��x�ɔ��˂���e�̐�
        enemyShotDate.OnceShotBullet = Mathf.Max(1, EditorGUILayout.IntField("��x�ɔ��˂���e���iN-Way�̏ꍇ�͗�)", enemyShotDate.OnceShotBullet));

        // ���ˊԊu����
        enemyShotDate.ShotIntervalTime = Mathf.Max(0.01f, EditorGUILayout.FloatField("���ˊԊu����", enemyShotDate.ShotIntervalTime));

        // �ˌ��J�n�E�I������
        enemyShotDate.ShotStartTime = Mathf.Max(0, EditorGUILayout.FloatField("�ˌ��J�n�܂ł̎���", enemyShotDate.ShotStartTime));
        enemyShotDate.ShotEndTime = Mathf.Max(0, EditorGUILayout.FloatField("�ˌ��I���܂ł̎���", enemyShotDate.ShotEndTime));

        // N-Way�e
        if (enemyShotDate.shotType == BulletVectorCalculation.ShotType.N_Way)
        {
            // �ˌ��^�C�v�̎�ނ�ݒ�
            enemyShotDate.angleType = (BulletVectorCalculation.AngleType)EditorGUILayout.EnumPopup("�ˌ��̎��", enemyShotDate.angleType);

            // �ˌ��͈�
            if (enemyShotDate.OnceShotBullet > 1)
            {
                enemyShotDate.AngleRange = Mathf.Max(1, EditorGUILayout.FloatField("�ˌ��͈�", enemyShotDate.AngleRange));
            }

            // Straight�ˌ��̏ꍇ�A���ˊp�x���w��
            if (enemyShotDate.angleType == BulletVectorCalculation.AngleType.Fixed)
            {
                enemyShotDate.ShotAngle = Mathf.Max(0, EditorGUILayout.FloatField("���ˊp�x", enemyShotDate.ShotAngle));
            }
        }

        // �����_���e
        if (enemyShotDate.shotType == BulletVectorCalculation.ShotType.Random)
        {
            // �ˌ��^�C�v�̎�ނ�ݒ�
            enemyShotDate.angleType = (BulletVectorCalculation.AngleType)EditorGUILayout.EnumPopup("�ˌ��̎��", enemyShotDate.angleType);

            // �ˌ��͈�
            enemyShotDate.AngleRange = Mathf.Max(1, EditorGUILayout.FloatField("�ˌ��͈�", enemyShotDate.AngleRange));

            // Straight�ˌ��̏ꍇ�A���ˊp�x���w��
            if (enemyShotDate.angleType == BulletVectorCalculation.AngleType.Fixed)
            {
                enemyShotDate.ShotAngle = Mathf.Max(0, EditorGUILayout.FloatField("���ˊp�x", enemyShotDate.ShotAngle));
            }
        }

        // ��]�e
        if (enemyShotDate.shotType == BulletVectorCalculation.ShotType.Spin)
        {
            // 1�������������̊p�x�ω���
            enemyShotDate.SpinAngleShift = EditorGUILayout.FloatField("�p�x�ω���", enemyShotDate.SpinAngleShift);
        }

        // �e�̎��
        enemyShotDate.BulletData = (BulletSpriteData)EditorGUILayout.ObjectField("�e�̎��", enemyShotDate.BulletData, typeof(BulletSpriteData), false);

        // �e�̐F
       // enemyShotDate.BulletColorType = Mathf.Clamp(EditorGUILayout.IntField("�e�̐F(�e�f�[�^���̔z��̔ԍ�)", enemyShotDate.BulletColorType), 0, enemyShotDate.BulletData.BulletImage.Length - 1);

        EditorUtility.SetDirty(target);
    }

}