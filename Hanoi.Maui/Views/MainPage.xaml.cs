using Hanoi.Maui.Models;
using Hanoi.Maui.Models.Graphics;

namespace Hanoi.Maui.Views;

public partial class MainPage : ContentPage
{
    // Declaramos los objetos que representan el estado del juego y el pintor geométrico
    // _juego es una instancia de la clase JuegoHanoi que contiene la lógica del juego y el estado de las torres y discos
    // _hanoiDrawable es una instancia de la clase HanoiDrawable que implementa la interfaz IDrawable y se encarga de dibujar el estado del juego en un canvas
    // Ambos objetos son privados y solo se usan dentro de esta clase MainPage
    private JuegoHanoi _juego;
    private HanoiDrawable _hanoiDrawable;

    public MainPage()
    {
        InitializeComponent();

        // Inicializamos los datos y el pintor geométrico
        _juego = new JuegoHanoi();// Creamos una nueva instancia de JuegoHanoi que representa el estado del juego
        _hanoiDrawable = new HanoiDrawable { EstadoJuego = _juego };// Creamos una nueva instancia de HanoiDrawable y le asignamos el estado del juego

        // Asignamos el pintor al objeto visual de la pantalla
        // MiGrafico es un objeto de tipo GraphicsView que representa el área donde se dibuja el juego
        // Drawable es una propiedad de GraphicsView que recibe un objeto que implementa la interfaz IDrawable
        MiGrafico.Drawable = _hanoiDrawable;
    }



    //Esta es la función que se llama desde el botón "Mover Disco" en la interfaz de usuario
    //Recibe como parámetros los índices de las torres de origen y destino (0, 1 o 2)
    //Llama a la función MoverDiscoVisual para realizar el movimiento y actualizar la vista
    private async Task MoverDiscoVisual(int origen, int destino)
    {
        // 1. Obtener las torres de origen y destino del estado del juego
        //Por ejemplo, si origen = 0 y destino = 2, torreOrigen será la lista de discos de la torre A y torreDestino será la lista de discos de la torre C
        var torreOrigen = _juego.Torres[origen];
        var torreDestino = _juego.Torres[destino];

        // 2. Verificar que la torre de origen no esté vacía
        // Si está vacía, no se puede mover ningún disco y se sale de la función
        if (torreOrigen.Count > 0)
        {
            // 1. Tomar el disco superior de la torre origen
            // Se obtiene el último elemento de la lista torreOrigen, que representa el disco más pequeño en la parte superior
            int disco = torreOrigen[^1];// Se usa el índice ^1 para acceder al último elemento de la lista
            torreOrigen.RemoveAt(torreOrigen.Count - 1);// Se elimina el disco de la torre origen

            // 2. Colocarlo en la torre destino
            torreDestino.Add(disco);// Se agrega el disco a la torre destino, que ahora tiene un disco más

            // 3. Forzar a .NET MAUI a borrar y redibujar el lienzo
            // Se llama al método Invalidate() del objeto MiGrafico, que indica que el área de dibujo necesita ser redibujada
            MiGrafico.Invalidate();

            // 4. PAUSA CLAVE: Detiene la ejecución por 800ms para que el ojo humano note el cambio
            await Task.Delay(800);
        }
    }


    //Esta es la función recursiva que implementa el algoritmo de resolución de las Torres de Hanoi
    private async Task ResolverHanoiRecursivo(int n, int origen, int destino, int auxiliar)
    {
        // Caso base: Si no hay discos que mover, terminamos
        if (n == 0) return;

        // Llamada recursiva: Mover n-1 discos desde la torre de origen a la torre auxiliar usando la torre de destino como auxiliar
        await ResolverHanoiRecursivo(n - 1, origen, auxiliar, destino);

        // Mover el disco restante desde la torre de origen a la torre de destino
        await MoverDiscoVisual(origen, destino);


        // Llamada recursiva: Mover los n-1 discos desde la torre auxiliar a la torre de destino usando la torre de origen como auxiliar
        await ResolverHanoiRecursivo(n - 1, auxiliar, destino, origen);
    }

    //Esta es la funcion que inicia el proceso de resolución automática cuando el usuario presiona el botón correspondiente

    private async void OnResolverAutomaticoClicked(object sender, EventArgs e)
    {
        // Deshabilitar el botón temporalmente para que el usuario no lo presione dos veces
        if (sender is Button boton) boton.IsEnabled = false;

        int numeroDeDiscos = _juego.Torres[0].Count; // En este ejemplo son 5

        // Inicia la recursividad: Mover 'n' discos desde la Torre 0 (A) a la Torre 2 (C) usando la 1 (B) como auxiliar
        // Aqui se inicia el metodo
        await ResolverHanoiRecursivo(numeroDeDiscos, 0, 2, 1);

        // Opcional: Avisar que terminó
        await DisplayAlert("¡Completado!", "El algoritmo resolvió las torres con éxito.", "OK");

        //
        if (sender is Button botonHabilitar) botonHabilitar.IsEnabled = true;
    }
}

