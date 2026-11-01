using Hanoi.Maui.Models;
using Hanoi.Maui.Models.Graphics;

namespace Hanoi.Maui.Views;

public partial class MainPage : ContentPage
{
    private JuegoHanoi _juego;
    private HanoiDrawable _hanoiDrawable;

    public MainPage()
    {
        InitializeComponent();

        // Inicializamos los datos y el pintor geométrico
        _juego = new JuegoHanoi();
        _hanoiDrawable = new HanoiDrawable { EstadoJuego = _juego };

        // Asignamos el pintor al objeto visual de la pantalla
        MiGrafico.Drawable = _hanoiDrawable;
    }



    //Esta es la función que se llama desde el botón "Mover Disco" en la interfaz de usuario
    private async Task MoverDiscoVisual(int origen, int destino)
    {
        var torreOrigen = _juego.Torres[origen];
        var torreDestino = _juego.Torres[destino];

        if (torreOrigen.Count > 0)
        {
            // 1. Tomar el disco superior de la torre origen
            int disco = torreOrigen[^1];
            torreOrigen.RemoveAt(torreOrigen.Count - 1);

            // 2. Colocarlo en la torre destino
            torreDestino.Add(disco);

            // 3. Forzar a .NET MAUI a borrar y redibujar el lienzo
            MiGrafico.Invalidate();

            // 4. PAUSA CLAVE: Detiene la ejecución por 800ms para que el ojo humano note el cambio
            await Task.Delay(800);
        }
    }

    //Esta es la función recursiva que resuelve el problema de las Torres de Hanoi
    private async Task ResolverHanoiRecursivo(int n, int origen, int destino, int auxiliar)
    {
        // Caso base: Si no hay discos que mover, terminamos
        if (n == 0) return;

        // Paso 1: Mover n-1 discos de la torre origen a la torre auxiliar usando la de destino como apoyo
        await ResolverHanoiRecursivo(n - 1, origen, auxiliar, destino);

        // Paso 2: Mover el disco más grande que queda desde origen hasta destino de forma visual
        await MoverDiscoVisual(origen, destino);

        // Paso 3: Mover los n-1 discos que dejamos en la auxiliar hacia el destino final usando origen como apoyo
        await ResolverHanoiRecursivo(n - 1, auxiliar, destino, origen);
    }

    //Esta es la funcion que inicia el proceso de resolución automática cuando el usuario presiona el botón correspondiente

    private async void OnResolverAutomaticoClicked(object sender, EventArgs e)
    {
        // Deshabilitar el botón temporalmente para que el usuario no lo presione dos veces
        if (sender is Button boton) boton.IsEnabled = false;

        int numeroDeDiscos = _juego.Torres[0].Count; // En este ejemplo son 3

        // Inicia la recursividad: Mover 'n' discos desde la Torre 0 (A) a la Torre 2 (C) usando la 1 (B) como auxiliar
        await ResolverHanoiRecursivo(numeroDeDiscos, 0, 2, 1);

        // Opcional: Avisar que terminó
        await DisplayAlert("¡Completado!", "El algoritmo resolvió las torres con éxito.", "OK");

        if (sender is Button botonHabilitar) botonHabilitar.IsEnabled = true;
    }
}

