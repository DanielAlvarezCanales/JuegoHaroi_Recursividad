namespace Hanoi.Maui.Models.Graphics
{

    //Esta es la declaracion de la clase que implementa la interfaz IDrawable para dibujar el estado del juego de las Torres de Hanoi en un canvas.
    public class HanoiDrawable : IDrawable
    {

        //Esta propiedad representa el estado actual del juego de las Torres de Hanoi.
        //Con sus get y set, permite obtener y establecer el estado del juego desde otras partes del código.
        public JuegoHanoi EstadoJuego { get; set; }


        //Este método es el encargado de dibujar el estado del juego en un canvas.
        //Recibe como parámetros un objeto ICanvas para dibujar y un RectF que representa el área sucia (dirtyRect) donde se realizará el dibujo.
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // Si el estado del juego es nulo, no se realiza ningún dibujo.
            if (EstadoJuego == null) return;

            // 1. Configurar dimensiones básicas basándonos en el tamaño de la pantalla
            // Se obtiene el ancho y alto de la pantalla a partir del dirtyRect
            float anchoPantalla = dirtyRect.Width;
            float altoPantalla = dirtyRect.Height;
            // Se define la línea del suelo para las torres, que se encuentra a un 80% de la altura de la pantalla
            float baseY = altoPantalla * 0.8f; // Línea del suelo para las torres

            // Coordenadas X para el centro de cada una de las 3 torres
            // Se colocan a un 25%, 50% y 75% del ancho de la pantalla
            // Se utiliza un array de float para almacenar las coordenadas X de las torres
            // *******Torre1******Torre2******Torre3******
            float[] torresX = [anchoPantalla * 0.25f, anchoPantalla * 0.5f, anchoPantalla * 0.75f];

            // 2. Dibujar la plataforma/suelo
            // Se establece el color de la línea del suelo y su grosor
            canvas.StrokeColor = Colors.SaddleBrown;// Color de la línea del suelo
            canvas.StrokeSize = 8;// Grosor de la línea del suelo
            canvas.DrawLine(dirtyRect.Left + 20, baseY, dirtyRect.Right - 20, baseY);
            // Se dibuja la línea del suelo desde el borde izquierdo hasta el borde derecho del dirtyRect, con un margen de 20 píxeles a cada lado


            // 3. Dibujar los 3 postes verticales
            canvas.StrokeSize = 6;// Grosor de los postes
            float altoPoste = 150;// Altura de los postes
            // Se dibujan los postes verticales en las coordenadas X de cada torre, desde la línea del suelo hasta la altura definida
            foreach (float x in torresX)
            {
                // Se dibuja una línea vertical para cada poste, desde la baseY hasta baseY - altoPoste
                //x es la coordenada X del poste, baseY es la coordenada Y de la línea del suelo, y altoPoste es la altura del poste
                // Se dibuja el poste vertical en la posición (x, baseY) hasta (x, baseY - altoPoste)
                canvas.DrawLine(x, baseY, x, baseY - altoPoste);
            }


            //En .NET MAUI, canvas es una superficie de dibujo sobre la que puedes representar elementos gráficos, como círculos, rectángulos, líneas, imágenes y figuras.
            //Imagínalo como una hoja en blanco sobre la que puedes dibujar utilizando código C#.
            //4. Dibujar los discos de cada torre
            //Se llama al método DibujarDiscosDeTorre para cada torre, pasando el canvas, la lista de discos de la torre, la coordenada X del centro de la torre y la coordenada Y de la base
            //canvas es el objeto ICanvas para dibujar, EstadoJuego.Torres[i] es la lista de discos de la torre i, torresX[i] es la coordenada X del centro de la torre i, y baseY es la coordenada Y de la base
            DibujarDiscosDeTorre(canvas, EstadoJuego.Torres[0], torresX[0], baseY);
            DibujarDiscosDeTorre(canvas, EstadoJuego.Torres[1], torresX[1], baseY);
            DibujarDiscosDeTorre(canvas, EstadoJuego.Torres[2], torresX[2], baseY);
        }


        // Este método privado se encarga de dibujar los discos de una torre específica en el canvas.
        //ICanvas: es el tipo de dato. Es una interfaz de .NET MAUI que proporciona las operaciones de dibujo.
        //canvas: es el nombre de la variable que recibirá el objeto de dibujo.
        //List<int> discos: es una lista de enteros que representa los discos en la torre. Cada entero indica el tamaño del disco.
        //List<int> discos = new List<int> { 5, 4, 3, 2, 1 };
        //centrox: almacena la coordenada horizontal donde se encuentra el centro de la torre.
        //baseY: almacena la coordenada vertical de referencia para dibujar los discos.
        private void DibujarDiscosDeTorre(ICanvas canvas, List<int> discos, float centroX, float baseY)
        {
            //alto de los discos es 25
            float altoDisco = 25;
            //El factor de los anchos de los discos es 30
            float factorAncho = 30; // Multiplicador para diferenciar los tamaños


            // Se recorre la lista de discos de la torre, y para cada disco se calcula su ancho y posición,
            // se asigna un color según su tamaño y se dibuja un rectángulo redondeado en el canvas.
            for (int i = 0; i < discos.Count; i++)
            {
                // Obtener el tamaño del disco y calcular su ancho
                int tamanoDisco = discos[i];// Por ejemplo, si el disco es 5, su ancho será 5 * factorAncho = 150
                // El ancho del disco se calcula multiplicando su tamaño por el factor de ancho
                float anchoDisco = tamanoDisco * factorAncho; //Si es 2 , su ancho será 2 * 30 = 60

                // Calcular posición (los discos se apilan de abajo hacia arriba)

                // La coordenada X se calcula restando la mitad del ancho del disco al centro de la torre,
                // para que el disco quede centrado.
                //Por ejemplo, si centroX es 200 y anchoDisco es 150, entonces x será 200 - (150 / 2) = 125
                //Osea x es el borde izquierdo del disco, y centroX es el centro del disco.
                float x = centroX - (anchoDisco / 2);

                // La coordenada Y se calcula restando al baseY el alto del disco multiplicado por (i + 1),
                //Por ejemplo, si baseY es 400, altoDisco es 25 y i es 0 (el primer disco), entonces y será 400 - ((0 + 1) * 25) = 375
                //y es la coordenada vertical del borde superior del disco, y baseY es la coordenada vertical de la base de la torre.
                float y = baseY - ((i + 1) * altoDisco);

                // Asignar color según el tamaño del disco
                //Funcion switch: Se utiliza un switch para asignar un color diferente a cada tamaño de disco.
                canvas.FillColor = tamanoDisco switch//Se utiliza un switch para asignar un color diferente a cada tamaño de disco
                {
                    5 => Colors.Green,
                    4 => Colors.Violet,
                    3 => Colors.Crimson,
                    2 => Colors.DarkOrange,
                    _ => Colors.DodgerBlue
                };

                // Dibujar el rectángulo redondeado del disco
                //Se utiliza el método FillRoundedRectangle para dibujar un rectángulo redondeado en el canvas.
                //x: coordenada horizontal del borde izquierdo del disco
                //y: coordenada vertical del borde superior del disco
                //anchoDisco: ancho del disco calculado previamente
                //altoDisco - 2: alto del disco menos 2 píxeles para que no se superponga con el disco de arriba
                canvas.FillRoundedRectangle(x, y, anchoDisco, altoDisco - 2, 5);
            }
        }
    }
}
