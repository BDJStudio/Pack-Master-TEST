#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(DataBase))]
public class GMEditor : Editor
{
    private DataBase db;

    public void OnEnable()
    {
        db = (DataBase)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (db.items.Count > 0)
        {
            foreach (Items items in db.items)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.BeginHorizontal();

                items.imgFlag = EditorGUILayout.ObjectField(items.imgFlag, typeof(Sprite), false, GUILayout.Width(60), GUILayout.Height(60)) as Sprite;
                
                EditorGUILayout.BeginVertical();

                items.id = EditorGUILayout.IntField("ID", items.id);
                items.country = EditorGUILayout.TextField("Страна", items.country);
                
                items.plusPrice = EditorGUILayout.IntField("+$", items.plusPrice);
                
                EditorGUILayout.EndVertical();
                
                if (GUILayout.Button("Удалить", GUILayout.Width(60), GUILayout.Height(60)))
                {
                    db.items.Remove(items);
                    break;
                }
                                
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();

                /*EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("Болванки для объектов");
                for (int i = 0; i < items.itemsPrefabs.Length; i++)
                {
                    items.itemsPrefabs[i] = (GameObject)EditorGUILayout.ObjectField(items.itemsPrefabs[i], typeof(GameObject),true);
                }

                EditorGUILayout.LabelField("Sprite 1x1");
                for (int i = 0; i < items.items1x1Sprite.Length; i++)
                {
                    items.items1x1Sprite[i] = (Sprite)EditorGUILayout.ObjectField(items.items1x1Sprite[i], typeof(Sprite), true);
                }

                EditorGUILayout.LabelField("Sprite 1x2");
                for (int i = 0; i < items.items1x2Sprite.Length; i++)
                {
                    items.items1x2Sprite[i] = (Sprite)EditorGUILayout.ObjectField(items.items1x2Sprite[i], typeof(Sprite), true);
                }

                EditorGUILayout.LabelField("Sprite 1x3");
                for (int i = 0; i < items.items1x3Sprite.Length; i++)
                {
                    items.items1x3Sprite[i] = (Sprite)EditorGUILayout.ObjectField(items.items1x3Sprite[i], typeof(Sprite), true);
                }

                EditorGUILayout.LabelField("Sprite 2x2");
                for (int i = 0; i < items.items2x2Sprite.Length; i++)
                {
                    items.items2x2Sprite[i] = (Sprite)EditorGUILayout.ObjectField(items.items2x2Sprite[i], typeof(Sprite), true);
                }
                EditorGUILayout.EndVertical();*/

                //EditorGUILayout.EndVertical();
            }
        }
        else EditorGUILayout.LabelField("no elements");

        if (GUILayout.Button("Добавить", GUILayout.Height(30)))
            db.items.Add(new Items());

        if (GUI.changed)
            SetObjectDirty(db.gameObject);
    }
    
    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}
#endif