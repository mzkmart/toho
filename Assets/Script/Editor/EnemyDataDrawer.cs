using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 敵のウェーブデータのScriptableObject内の、リストをエディター拡張するDrawerクラス
/// </summary>
[CustomPropertyDrawer(typeof(EnemyData))]
public class EnemyDataDrawer : PropertyDrawer
{
    // 出現位置決定の際、画面外から入ってくる場合がある為マージンを設定
    private const float FIELD_MARGIN = 5;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 拡張ボタンのRect
        Rect propRect = new Rect(position)
        {
            x = position.x + 10f,
            y = position.yMin
        };

        // 拡張ボタンを表示
        if (!property.isExpanded)
        {
            property.isExpanded = EditorGUI.Foldout(propRect, property.isExpanded, new GUIContent(label));
        }
        else
        {
            // 0指定だとReorderableListのドラッグと被るのでLineHeightを指定
            position.height = EditorGUIUtility.singleLineHeight;

            Rect foldRect = new Rect(position)
            {
                x = position.x + 10f,
            };

            // 拡張の解除
            property.isExpanded = EditorGUI.Foldout(foldRect, property.isExpanded, new GUIContent(label));

            // List用に1つのプロパティであることを示すためPropertyScopeで囲む
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                // ウェーブ名
                Rect nameRect = new Rect(position)
                {
                    y = position.y + EditorGUIUtility.singleLineHeight + 2f
                };
                SerializedProperty nameProperty = property.FindPropertyRelative("name");

                nameProperty.stringValue = EditorGUI.TextField(nameRect, "ウェーブ名", nameProperty.stringValue);

                // 敵のタイプ
                Rect enemyTypeRect = new Rect(position)
                {
                    y = nameRect.y + EditorGUIUtility.singleLineHeight + 2f
                };
                SerializedProperty enemyTypeProperty = property.FindPropertyRelative("enemyType");

                enemyTypeProperty.enumValueIndex = EditorGUI.Popup(enemyTypeRect, "敵のタイプ", enemyTypeProperty.enumValueIndex, Enum.GetNames(typeof(EnemyData.WaveEnemyType)));

                // タイプによって設定する要素を分ける
                switch ((EnemyData.WaveEnemyType)enemyTypeProperty.enumValueIndex)
                {
                    case EnemyData.WaveEnemyType.Normal:

                        #region NormalData

                        // 敵のデータ
                        Rect spriteDataRect = new Rect(enemyTypeRect)
                        {
                            y = enemyTypeRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty spriteDateProperty = property.FindPropertyRelative("enemySprite");
                        spriteDateProperty.objectReferenceValue = (EnemySpriteData)EditorGUI.ObjectField(spriteDataRect, "敵のデータ", spriteDateProperty.objectReferenceValue, typeof(EnemySpriteData), false);

                        // 出現時間
                        Rect appearanceTimeRect = new Rect(enemyTypeRect)
                        {
                            y = spriteDataRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty appearanceTimeProperty = property.FindPropertyRelative("appearanceTime");
                        appearanceTimeProperty.floatValue = Mathf.Max(0, EditorGUI.FloatField(appearanceTimeRect, "出現時間", appearanceTimeProperty.floatValue));

                        // 移動パターン
                        Rect movePatternRect = new Rect(enemyTypeRect)
                        {
                            y = appearanceTimeRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty movePatternProperty = property.FindPropertyRelative("movePattern");
                        movePatternProperty.enumValueIndex = EditorGUI.Popup(movePatternRect, "移動パターン", movePatternProperty.enumValueIndex, Enum.GetNames(typeof(EnemyMovePattern.Pattern)));

                        // 速度
                        Rect speedRect = new Rect(enemyTypeRect)
                        {
                            y = movePatternRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty speedProperty = property.FindPropertyRelative("speed");
                        speedProperty.floatValue = Mathf.Max(0, EditorGUI.FloatField(speedRect, "移動速度", speedProperty.floatValue));

                        // 出現位置のX座標
                        Rect appearancePosXRect = new Rect(enemyTypeRect)
                        {
                            y = speedRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty appearancePosXProperty = property.FindPropertyRelative("appearancePositionX");
                        appearancePosXProperty.floatValue = EditorGUI.IntSlider(appearancePosXRect, "出現位置のX座標", (int)appearancePosXProperty.floatValue,
                            (int)appearancePosXProperty.floatValue - 3,
                            (int)appearancePosXProperty.floatValue + 3);

                        // 出現位置のY座標
                        Rect appearancePosYRect = new Rect(enemyTypeRect)
                        {
                            y = appearancePosXRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty appearancePosYProperty = property.FindPropertyRelative("appearancePositionY");
                        appearancePosYProperty.floatValue = EditorGUI.IntSlider(appearancePosYRect, "出現位置のY座標", (int)appearancePosYProperty.floatValue,
                            (int)appearancePosYProperty.floatValue - 5,
                             (int)appearancePosYProperty.floatValue + 5);

                        // 射撃パターン
                        Rect shotDateRect = new Rect(enemyTypeRect)
                        {
                            y = appearancePosYRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty shotDateProperty = property.FindPropertyRelative("shotData");
                        shotDateProperty.objectReferenceValue = (EnemyShotData)EditorGUI.ObjectField(shotDateRect, "射撃の種類", shotDateProperty.objectReferenceValue, typeof(EnemyShotData), false);

                        // 体力
                        Rect hpRect = new Rect(enemyTypeRect)
                        {
                            y = shotDateRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty hpProperty = property.FindPropertyRelative("hp");
                        hpProperty.intValue = Mathf.Max(0, EditorGUI.IntField(hpRect, "体力", hpProperty.intValue));

                        // Pのドロップ量
                        Rect powerItemDropRect = new Rect(enemyTypeRect)
                        {
                            y = hpRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty powerItemDropProperty = property.FindPropertyRelative("powerItemDropValue");
                        powerItemDropProperty.intValue = Mathf.Max(0, EditorGUI.IntField(powerItemDropRect, "Pのドロップ量", powerItemDropProperty.intValue));



                        // 点のドロップ量
                        Rect scoreItemDropRect = new Rect(enemyTypeRect)
                        {
                            y = powerItemDropRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty scoreItemDropProperty = property.FindPropertyRelative("powerItemDropValue");
                        scoreItemDropProperty.intValue = Mathf.Max(0, EditorGUI.IntField(scoreItemDropRect, "点のドロップ量", scoreItemDropProperty.intValue));
                        #endregion
                        break;

                    default:

                        // 敵のデータ
                        Rect bossDataRect = new Rect(enemyTypeRect)
                        {
                            y = enemyTypeRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty bossDataProperty = property.FindPropertyRelative("bossWave");
                        bossDataProperty.objectReferenceValue = (BossData)EditorGUI.ObjectField(bossDataRect, "ボスのデータ", bossDataProperty.objectReferenceValue, typeof(BossData), false);

                        // 出現時間
                        Rect appearanceBossTimeRect = new Rect(enemyTypeRect)
                        {
                            y = bossDataRect.y + EditorGUIUtility.singleLineHeight + 2f
                        };

                        SerializedProperty appearanceBossTimeProperty = property.FindPropertyRelative("appearanceTime");
                        appearanceBossTimeProperty.floatValue = Mathf.Max(0, EditorGUI.FloatField(appearanceBossTimeRect, "出現時間", appearanceBossTimeProperty.floatValue));

                        break;
                }
            }
        }
    }

    /// <summary>
    /// 要素の高さの調節
    /// </summary>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height;

        SerializedProperty enemyTypeProperty = property.FindPropertyRelative("enemyType");

        switch ((EnemyData.WaveEnemyType)enemyTypeProperty.enumValueIndex)
        {
            case EnemyData.WaveEnemyType.Normal:

                // ザコ敵の場合、そのままの高さにする
                height = EditorGUI.GetPropertyHeight(property);
                break;

            default:

                // ボス敵の場合、拡張時は高さを低くし非拡張時はそのまま
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