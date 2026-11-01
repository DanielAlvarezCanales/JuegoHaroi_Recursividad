
using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static Stack<int> TorreA = new Stack<int>();
    static Stack<int> TorreB = new Stack<int>();
    static Stack<int> TorreC = new Stack<int>();

    static void Main()
    {
        // Inicializar los 5 discos
        for (int i = 5; i >= 1; i--)
        {
            TorreA.Push(i);
        }

        Console.WriteLine("TORRES DE HANOI");
        MostrarTorres();

        Console.WriteLine("\nPresiona una tecla para comenzar...");
        Console.ReadKey();

        ResolverHanoi(5, TorreA, TorreC, TorreB);

        Console.WriteLine("\n¡Juego terminado!");
        MostrarTorres();
    }

    // Algoritmo recursivo
    static void ResolverHanoi(
        int n,
        Stack<int> origen,
        Stack<int> destino,
        Stack<int> auxiliar)
    {
        if (n == 1)
        {
            destino.Push(origen.Pop());

            Console.Clear();
            MostrarTorres();

            Thread.Sleep(700);
            return;
        }

        ResolverHanoi(n - 1, origen, auxiliar, destino);

        destino.Push(origen.Pop());

        Console.Clear();
        MostrarTorres();

        Thread.Sleep(700);

        ResolverHanoi(n - 1, auxiliar, destino, origen);
    }

    // Mostrar las tres torres
    static void MostrarTorres()
    {
        Stack<int>[] torres = { TorreA, TorreB, TorreC };

        int altura = 5;

        for (int nivel = altura - 1; nivel >= 0; nivel--)
        {
            foreach (var torre in torres)
            {
                int[] discos = torre.ToArray();

                if (nivel < discos.Length)
                {
                    int disco = discos[nivel];

                    Console.Write(
                        new string(' ', 6 - disco) +
                        new string('*', disco * 2 - 1) +
                        new string(' ', 6 - disco)
                    );
                }
                else
                {
                    Console.Write("     |     ");
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine("     A          B          C");
        Console.WriteLine("--------------------------------");
    }
}

