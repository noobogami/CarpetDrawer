using Garaj.Zoom;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Cell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SpriteRenderer ren;

        internal Vector2Int Pos;
        internal Color Color;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.dragging || GZoom.Ins.Dragging) return;
            
            ren.color = Manager.CurrentColor;
        }

        public void Init(Vector2Int position, Color color)
        {
            Color = color;
            Pos = position;
            ren.color = Color;
            transform.position = new Vector3(Pos.x, Pos.y, -5);
        }
    }
}