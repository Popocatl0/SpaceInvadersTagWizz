using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShipEnemy;

[CreateAssetMenu(fileName = "LevelData", menuName = "Space Invaders/LevelData", order = 0)]
public class LevelData : ScriptableObject {
    [System.Serializable]
    public struct LineBlock{
        public ShipEnemy enemy;
        public float horizontalSpacing;
        public float offset;
    }

    [SerializeField] int rowCount;
    [SerializeField] float verticalSpacing;
    [SerializeField] float speed;
    [SerializeField] float limitMove;
    [SerializeField] LineBlock[] pattern;


    public float Speed { get { return speed;}}
    public float Limit { get { return limitMove;}}
    public float VerticalSpacing { get { return verticalSpacing;}}
    
    public ShipEnemy[] Display(Transform parent, OnDestroyDelegate onDestroy){
        List<ShipEnemy> list = new List<ShipEnemy>();
        for (int j = 0; j < pattern.Length; j++) {
            for (int i = 0; i < rowCount; i++) {
                ShipEnemy enemy = Instantiate(pattern[j].enemy, parent);
                enemy.ResetObject();
                enemy.OnDestroy += onDestroy;
                if(i%2 == 0){
                    enemy.transform.localPosition = new Vector3( (i/2 * pattern[j].horizontalSpacing) + pattern[j].offset, j * verticalSpacing * -1, 0 );
                }
                else{
                    enemy.transform.localPosition = new Vector3( (-1 * Mathf.CeilToInt(i/2.0f) * pattern[j].horizontalSpacing) + pattern[j].offset, j * verticalSpacing * -1, 0 );
                }
                list.Add(enemy);
            }
        }
        return list.ToArray();
    }

    
}
