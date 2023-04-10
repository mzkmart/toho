using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// �G�̃E�F�[�u�f�[�^��ScriptableObject���́A���X�g���G�f�B�^�[�g������Drawer�N���X
/// </summary>
[CustomPropertyDrawer(typeof(EnemyData))]
public class EnemyDataDrawer : PropertyDrawer
{
    // �o���ʒu����̍ہA��ʊO��������Ă���ꍇ������׃}�[�W����ݒ�
    private const float FIELD_MARGIN = 5;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // �g���{�^����Rect
        Rect propRect = new Rect(position)
        {
            x = position.x + 10f,
            y = position.yMin
        };

        // �g���{�^����\��
        if (!property.isExpanded)
        {
            property.isExpanded = EditorGUI.Foldout(propRect, property.isExpanded, new GUIContent(label));
        }
        else
        {
            // 0�w�肾��ReorderableList�̃h���b�O�Ɣ��̂�LineHeight���w��
            position.height = EditorGUIUtility.singleLineHeight;

            Rect foldRect = new Rect(position)
            {
                x = position.x + 10f,
            };

            // �g���̉���
            property.isExpanded = EditorGUI.Foldout(foldRect, property.isExpanded, new GUIContent(label));

            // List�p��1�̃v���p�e�B�ł��邱�Ƃ���������PropertyScope�ň͂�
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                // �E�F�[�u��
                Rect nameRect = new Rect(position)
                {
                    y = position.y + EditorGUIUtility.singleLineHeight + 2f
                };
                SerializedProperty nameProperty = property.FindPropertyRelative("name");

                nameProperty.stringValue = EditorGUI.TextField(nameRect, "�E�F�[�u��", nameProperty.stringValue);

                // �G�̃^�C�v
                Rect enemyTypeRect = new Rect(position)
                {
                    y = nameRect.y + EditorGUIUtility.singleLineHeight + 2f
                };
                SerializedProperty enemyTypeProperty = property.FindPropertyRelative("enemyType");

                enemyTypeProperty.enumValueIndex = EditorGUI.Popup(enemyTypeRect, "�G�̃^�C�v", enemyTypeProperty.enumValueIndex, Enum.GetNames(typeof(EnemyData.WaveEnemyType)));

                // �^�C�v�ɂ���Đݒ肷��v�f�𕪂���
                switch ((EnemyData.WaveEnemyType)enemyTypeProperty.enumValueIndex)
                {
                    case EnemyData.WaveEnemyType.Normal:

                        #region NormalData

                        // �G�̃f�[�^
                        Rect spriteDataRect = new Rect(enemyTypeRect)
                        {
                            y = enemyTypeRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty spriteDateProperty = property.FindPropertyRelative("enemySprite");
                        spriteDateProperty.objectReferenceValue = (EnemySpriteData)EditorGUI.ObjectField(spriteDataRect, "�G�̃f�[�^", spriteDateProperty.objectReferenceValue, typeof(EnemySpriteData), false);

                        // �o������
                        Rect appearanceTimeRect = new Rect(enemyTypeRect)
                        {
                            y = spriteDataRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty appearanceTimeProperty = property.FindPropertyRelative("appearanceTime");
                        appearanceTimeProperty.floatValue = Mathf.Max(0, EditorGUI.FloatField(appearanceTimeRect, "�o������", appearanceTimeProperty.floatValue));

                        // �ړ��p�^�[��
                        Rect movePatternRect = new Rect(enemyTypeRect)
                        {
                            y = appearanceTimeRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty movePatternProperty = property.FindPropertyRelative("movePattern");
                        movePatternProperty.enumValueIndex = EditorGUI.Popup(movePatternRect, "�ړ��p�^�[��", movePatternProperty.enumValueIndex, Enum.GetNames(typeof(EnemyMovePattern.Pattern)));

                        // ���x
                        Rect speedRect = new Rect(enemyTypeRect)
                        {
                            y = movePatternRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty speedProperty = property.FindPropertyRelative("speed");
                        speedProperty.floatValue = Mathf.Max(0, EditorGUI.FloatField(speedRect, "�ړ����x", speedProperty.floatValue));

                        // �o���ʒu��X���W
                        Rect appearancePosXRect = new Rect(enemyTypeRect)
                        {
                            y = speedRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty appearancePosXProperty = property.FindPropertyRelative("appearancePositionX");
                        appearancePosXProperty.floatValue = EditorGUI.IntSlider(appearancePosXRect, "�o���ʒu��X���W", (int)appearancePosXProperty.floatValue,
                            (int)appearancePosXProperty.floatValue - 3,
                            (int)appearancePosXProperty.floatValue + 3);

                        // �o���ʒu��Y���W
                        Rect appearancePosYRect = new Rect(enemyTypeRect)
                        {
                            y = appearancePosXRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty appearancePosYProperty = property.FindPropertyRelative("appearancePositionY");
                        appearancePosYProperty.floatValue = EditorGUI.IntSlider(appearancePosYRect, "�o���ʒu��Y���W", (int)appearancePosYProperty.floatValue,
                            (int)appearancePosYProperty.floatValue - 5,
                             (int)appearancePosYProperty.floatValue + 5);

                        // �ˌ��p�^�[��
                        Rect shotDateRect = new Rect(enemyTypeRect)
                        {
                            y = appearancePosYRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty shotDateProperty = property.FindPropertyRelative("shotData");
                        shotDateProperty.objectReferenceValue = (EnemyShotData)EditorGUI.ObjectField(shotDateRect, "�ˌ��̎��", shotDateProperty.objectReferenceValue, typeof(EnemyShotData), false);

                        // �̗�
                        Rect hpRect = new Rect(enemyTypeRect)
                        {
                            y = shotDateRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty hpProperty = property.FindPropertyRelative("hp");
                        hpProperty.intValue = Mathf.Max(0, EditorGUI.IntField(hpRect, "�̗�", hpProperty.intValue));

                        // P�̃h���b�v��
                        Rect powerItemDropRect = new Rect(enemyTypeRect)
                        {
                            y = hpRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty powerItemDropProperty = property.FindPropertyRelative("powerItemDropValue");
                        powerItemDropProperty.intValue = Mathf.Max(0, EditorGUI.IntField(powerItemDropRect, "P�̃h���b�v��", powerItemDropProperty.intValue));



                        // �_�̃h���b�v��
                        Rect scoreItemDropRect = new Rect(enemyTypeRect)
                        {
                            y = powerItemDropRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty scoreItemDropProperty = property.FindPropertyRelative("powerItemDropValue");
                        scoreItemDropProperty.intValue = Mathf.Max(0, EditorGUI.IntField(scoreItemDropRect, "�_�̃h���b�v��", scoreItemDropProperty.intValue));
                        #endregion
                        break;

                    default:

                        // �G�̃f�[�^
                        Rect bossDataRect = new Rect(enemyTypeRect)
                        {
                            y = enemyTypeRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty bossDataProperty = property.FindPropertyRelative("bossWave");
                        bossDataProperty.objectReferenceValue = (BossData)EditorGUI.ObjectField(bossDataRect, "�{�X�̃f�[�^", bossDataProperty.objectReferenceValue, typeof(BossData), false);

                        // �o������
                        Rect appearanceBossTimeRect = new Rect(enemyTypeRect)
                        {
                            y = bossDataRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty appearanceBossTimeProperty = property.FindPropertyRelative("appearanceTime");
                        appearanceBossTimeProperty.floatValue = Mathf.Max(0, EditorGUI.FloatField(appearanceBossTimeRect, "�o������", appearanceBossTimeProperty.floatValue));

                        break;
                }
            }
        }
    }

    /// <summary>
    /// �v�f�̍����̒���
    /// </summary>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height;

        SerializedProperty enemyTypeProperty = property.FindPropertyRelative("enemyType");

        switch ((EnemyData.WaveEnemyType)enemyTypeProperty.enumValueIndex)
        {
            case EnemyData.WaveEnemyType.Normal:

                // �U�R�G�̏ꍇ�A���̂܂܂̍����ɂ���
                height = EditorGUI.GetPropertyHeight(property);
                break;

            default:

                // �{�X�G�̏ꍇ�A�g�����͍�����Ⴍ����g�����͂��̂܂�
                if (!property.isExpanded)
                {
                    height = EditorGUI.GetPropertyHeight(property);
                }
                else
                {
                    height = EditorGUI.GetPropertyHeight(property) - ((EditorGUIUtility.singleLineHeight + 2f) * 9);
                }
                break;
        }

        return height;
    }
}