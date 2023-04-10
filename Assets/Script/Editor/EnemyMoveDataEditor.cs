using UnityEditor;
using UnityEditorInternal;
/// <summary>
/// 敵のウェーブデータのScriptableObjectのエディターを拡張するクラス
/// </summary>
[CustomEditor(typeof(EnemyWaveData))]
public class EnemyWaveDateEditor : Editor
{
    private ReorderableList waveReorderbleList;
    private SerializedProperty enemyDataList;

    private void OnEnable()
    {
        enemyDataList = serializedObject.FindProperty("enemyData");

        waveReorderbleList = new ReorderableList(serializedObject, enemyDataList);

        waveReorderbleList.drawElementCallback = (rect, index, active, focused) =>
        {
            SerializedProperty actionData = enemyDataList.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, actionData);
        };

        waveReorderbleList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "EnemyWaveData");
        waveReorderbleList.elementHeightCallback = index => EditorGUI.GetPropertyHeight(enemyDataList.GetArrayElementAtIndex(index));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        waveReorderbleList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}