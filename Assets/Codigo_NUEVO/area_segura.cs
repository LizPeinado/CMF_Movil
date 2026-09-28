using UnityEngine;


public class AreaSegura: MonoBehaviour{
    private Canvas canvas;

    [SerializeField] RectTransform panel_seguro;
    public int padding;

    private Rect area = Rect.zero;

    private void Awake(){
        canvas = GetComponent<Canvas>();
        
        area = Screen.safeArea;

        ajustar_area();
    }

    private void LateUpdate(){
        if(area != Screen.safeArea){
            area = Screen.safeArea;
        }
    }

    private void ajustar_area(){
        if(panel_seguro != null){
            var area_segura = Screen.safeArea;

            var ancla_minima = area_segura.position;
            var ancla_maxima = area_segura.position + area_segura.size;

            ancla_maxima.x -= padding / 2;
            ancla_maxima.y -= padding / 2;

            ancla_minima.x += padding / 2;
            ancla_minima.y += padding / 2;



            // Se normaliza a resolucion del canvas
            ancla_minima.x /= Screen.width;
            ancla_minima.y /= Screen.height;

            ancla_maxima.x /= Screen.width;
            ancla_maxima.y /= Screen.height;

            panel_seguro.anchorMax = ancla_maxima;
            panel_seguro.anchorMin = ancla_minima;

        }
    }
}
