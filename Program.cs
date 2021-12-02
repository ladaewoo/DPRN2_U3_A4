using System;
using System.Collections.Generic;
class Imprimir
{
    public static void AlCentro(string texto, int techo)
    {
        Console.SetCursorPosition((Console.WindowWidth - texto.Length) / 2, techo);
        Console.WriteLine(texto);
    }

    public static void AlCentro(string texto)
    {
        AlCentro(texto, (Console.WindowHeight / 2) - 4);
    }
    
    public Imprimir()
    {

    }
}

class Jugadores 
{
    private static int vector = 0;
    private static string[,] jugadores = new string[vector + 1, 2];
    public Jugadores()
    {}

    public int Count
    {
        get {
            return vector;
        }
    }

    private void actualizar()
    {
        string [,] jugadores_aux = new string[vector + 2, 2];

        for (int i = 0; i < vector; i++)
        {
            jugadores_aux[i, 0] = jugadores[i, 0];
            jugadores_aux[i, 1] = jugadores[i, 1];
        }

        jugadores = jugadores_aux;
    }

    public void AgregarJugador(string nombre, string boleto)
    {
        
        actualizar();
        jugadores[vector, 0] = nombre;
        jugadores[vector, 1] = boleto.ToString();
        vector++;
    }

    public void MostrarJugadores()
    {
        int top = Console.WindowHeight / 2 - 4;

        try {

            for (int i = 0; i < vector; i++)
            {
                Imprimir.AlCentro("Nombre: " + jugadores[i, 0] + " Boleto: " + jugadores[i, 1], top + i + 1);
            }
        } catch (IndexOutOfRangeException) {
            Imprimir.AlCentro("No hay jugadores", top);
        }

    }
}

class Juego 
{
    // Implementaremos la clase List, que serán las colecciones de Premios y Boletos
    public static List<string> Premios = new List<string>();
    public static List<string> Boletos = new List<string>();

    private static Jugadores jugadores = new Jugadores();

    public Juego()
    {

    }

    // Cargamos los premios
    public void CargarPremios()
    {
        Premios.Add("Batidora Oster");
        Premios.Add("Tesla Model S");
        Premios.Add("$1,000.00");
        Premios.Add("$1,000.00");
        Premios.Add("$10,000.00");
        Premios.Add("Macbook pro M1 Max");
        Premios.Add("$100,000.00");
        Premios.Add("$100,000.00");
        Premios.Add("Tarjeta de regalo Amazon $500");
        Premios.Add("$1,000,000.00");
    }

    public void GenerarBoleto()
    {
        Random rnd = new Random();
        int numero = rnd.Next(1, 100);
        
        if (numero % 3 == 0)
        {
            numero = rnd.Next(0, Premios.Count-1);

                Boletos.Add(Premios[Premios.Count - 1]);
                Premios.RemoveAt(Premios.Count - 1);
            
        } else {
            Boletos.Add("Sin premio");
        }
        
    }

    // Aquí hacemos uso de la encapsulación para ocultar la función que interactúa con el usuario
    public int ElegirNumero()
    {
        return ElegirNumero(0);
    }

    public void Resultados()
    {
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.Clear();
        int top = (Console.WindowHeight / 2) - 4;
        Imprimir.AlCentro("Lista de jugadores", top);
        Imprimir.AlCentro("-----------------", top + 1);
        Console.WriteLine("jugadores.legth:", jugadores.Count);
        jugadores.MostrarJugadores();
        Console.ReadKey();
    }

    static private int ElegirNumero(int numero)
    {
        string[] jugador = new string[2];
        Console.Clear();
        Imprimir.AlCentro("¿Como te llamas?");
        Console.SetCursorPosition((Console.WindowWidth / 2) - 8, (Console.WindowHeight / 2));
        jugador[0] = Console.ReadLine();
        Console.Clear();
        Imprimir.AlCentro("💬 Elige un número entre el 1 y el 30");
        Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2));

        // Prevenimos errores en el ingreso de datos
        try {
            numero = Convert.ToInt32(Console.ReadLine());
        } catch (Exception e) {
            Console.WriteLine("🙈 " + e.Message);
            numero = 0;
        }
        
        Console.Clear();

        if (numero > 0 && numero <= 100) {
            try {
                jugador[1] = Boletos[numero - 1];
            } catch (Exception e) {
                jugador[1] = "🙈 No hay boleto para ese número";
            }

            Imprimir.AlCentro(jugador[1]);

            jugadores.AgregarJugador(jugador[0], jugador[1]);

            Console.ReadKey();

        }

        return 2;
    }

    int top = (Console.WindowHeight / 2) - 5;

    public void Inicio(int estado)
    {
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.Clear();

        Imprimir.AlCentro("Bienvenido al juego de la suerte 💫", top - 1);
        Imprimir.AlCentro("Hay 30 boletos y 6 premios que van desde $1 hasta $1,000,000.00", top);
        Imprimir.AlCentro("¿Quieres jugar?", top + 1);

        if(estado == 1){
            Console.BackgroundColor = ConsoleColor.DarkRed;
        }
        
        Imprimir.AlCentro("Si", top + 3);
        Console.BackgroundColor = ConsoleColor.DarkGray;

        if(estado == 0){
            Console.BackgroundColor = ConsoleColor.DarkRed;
        }

        Imprimir.AlCentro("No", top + 5);
    }
}


namespace DPRN2_U3_A4
{
    class Program
    {
        static private int estado = 0;
        static private int capitulo = 0;
        static int top = (Console.WindowHeight / 2) - 5;

        static int contador = 0;

        static Juego juego = new Juego();

        static void Main(string[] args)
        {
            Console.Title = "DPRN2_U3_A4";

            while (true)
            {
                switch(capitulo){
                    case 0:
                        juego.CargarPremios();
                        juego.Inicio(estado);
                        break;
                    case 1:
                        juego.ElegirNumero();
                        juego.Resultados();
                        capitulo = 0;
                        break;
                }
                
                ConsoleKeyInfo tecla;
                
                tecla = Console.ReadKey();
            
                if (tecla.Key == ConsoleKey.UpArrow || tecla.Key == ConsoleKey.LeftArrow)
                {
                    estado = 1;
                }
                else if (tecla.Key == ConsoleKey.DownArrow || tecla.Key == ConsoleKey.RightArrow)
                {
                    estado = 0;
                }
                else if(tecla.Key == ConsoleKey.Enter){
                    if(estado == 1){
                        capitulo = 1;
                    }
                    else {
                        capitulo = 0;
                        Console.Clear();
                        Imprimir.AlCentro("Hasta luego!", top);
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
                else if(tecla.Key == ConsoleKey.Escape){
                    capitulo = 0;
                }
                
            } 
        }
    }
}