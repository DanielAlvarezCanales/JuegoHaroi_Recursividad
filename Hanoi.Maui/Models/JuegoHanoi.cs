using System;
using System.Collections.Generic;
using System.Text;

namespace Hanoi.Maui.Models
{
    public class JuegoHanoi
    {
        //Cambiamos a un diccionario indexado por el número de la torre (0 = A, 1 = B, 2 = C)
        public Dictionary<int, List<int>> Torres { get; set; } = new()
    {
        { 0, new List<int> { 5, 4, 3, 2, 1 } }, // Estado inicial: 3 discos en la Torre 0
        { 1, new List<int>() },
        { 2, new List<int>() }
    };

    }
}
