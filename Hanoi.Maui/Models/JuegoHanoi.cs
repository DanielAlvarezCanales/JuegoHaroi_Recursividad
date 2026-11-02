using System;
using System.Collections.Generic;
using System.Text;

namespace Hanoi.Maui.Models
{
    public class JuegoHanoi
    {
        //Cambiamos a un diccionario indexado por el número de la torre (0 = A, 1 = B, 2 = C)
        //Cada torre contiene una lista de enteros que representan los discos, donde el entero más grande representa el disco más grande
        //y el entero más pequeño representa el disco más pequeño.
        public Dictionary<int, List<int>> Torres { get; set; } = new()//new() es una forma de inicializar un diccionario vacío en C# 9.0 y versiones posteriores
    {
        { 0, new List<int> { 4, 3, 2, 1 } },// La torre A (índice 0) comienza con todos los discos, del más grande al más pequeño
        { 1, new List<int>() },
        { 2, new List<int>() }
    };

    }
}
