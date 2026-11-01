namespace Hanoi.Maui.Models.Graphics
{
    public class HanoiDrawable : IDrawable
    {
        public JuegoHanoi EstadoJuego { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (EstadoJuego == null) return;

            // 1. Configurar dimensiones básicas basándonos en el tamaño de la pantalla
            float anchoPantalla = dirtyRect.Width;
            float altoPantalla = dirtyRect.Height;
            float baseY = altoPantalla * 0.8f; // Línea del suelo para las torres

            // Coordenadas X para el centro de cada una de las 3 torres
            float[] torresX = [anchoPantalla * 0.25f, anchoPantalla * 0.5f, anchoPantalla * 0.75f];

            // 2. Dibujar la plataforma/suelo
            canvas.StrokeColor = Colors.SaddleBrown;
            canvas.StrokeSize = 8;
            canvas.DrawLine(dirtyRect.Left + 20, baseY, dirtyRect.Right - 20, baseY);

            // 3. Dibujar los 3 postes verticales
            canvas.StrokeSize = 6;
            float altoPoste = 150;
            foreach (float x in torresX)
            {
                canvas.DrawLine(x, baseY, x, baseY - altoPoste);
            }

            // 4. Dibujar los discos de cada torre
            DibujarDiscosDeTorre(canvas, EstadoJuego.Torres[0], torresX[0], baseY);
            DibujarDiscosDeTorre(canvas, EstadoJuego.Torres[1], torresX[1], baseY);
            DibujarDiscosDeTorre(canvas, EstadoJuego.Torres[2], torresX[2], baseY);
        }

        private void DibujarDiscosDeTorre(ICanvas canvas, List<int> discos, float centroX, float baseY)
        {
            float altoDisco = 25;
            float factorAncho = 30; // Multiplicador para diferenciar los tamaños

            for (int i = 0; i < discos.Count; i++)
            {
                int tamanoDisco = discos[i];
                float anchoDisco = tamanoDisco * factorAncho;

                // Calcular posición (los discos se apilan de abajo hacia arriba)
                float x = centroX - (anchoDisco / 2);
                float y = baseY - ((i + 1) * altoDisco);

                // Asignar color según el tamaño del disco
                canvas.FillColor = tamanoDisco switch
                {
                    5 => Colors.Green,
                    4 => Colors.Violet,
                    3 => Colors.Crimson,
                    2 => Colors.DarkOrange,
                    _ => Colors.DodgerBlue
                };

                // Dibujar el rectángulo redondeado del disco
                canvas.FillRoundedRectangle(x, y, anchoDisco, altoDisco - 2, 5);
            }
        }
    }
}
