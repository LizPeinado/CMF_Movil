using System;
using UnityEngine;

public enum TipoItem{
    consumible,
    golpazo
}

public interface InteractuableComportamiento{
    TipoItem tipo {get; set;}
    string nombre {get; set;}

    void colocar_en(Transform ubicacion);

    void accion_consumible();
    void accion_golpazo();
    void soltar();
}