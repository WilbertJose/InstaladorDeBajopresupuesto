using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    private const string LinkDeDescarga = "Insertar Link";
    private static readonly string DescargarMods = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "Mods.zip");
    private static readonly string ExtraerMods = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

    static async Task Main(string[] args)
    {

        while (true)
        {
            Console.WriteLine("Selecciona una de estas 3 madres");
            Console.WriteLine("1. instalar Mods");
            Console.WriteLine("2. Actualizar Mods");
            Console.WriteLine("3. Eliminar Mods");
            Console.WriteLine("Salir XD");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await DownloadAndExtract();
                    break;
                case "2":
                    await UpdateAndExtract();
                    break;
                case "3":
                    DeleteFiles();
                    break;
                case "4":
                    return;
                default: Console.WriteLine("No quieres unos nachos tambien?, en la pantalla pone claramente 4 opciones disponibles no sea wey.");
                    break;
            }
        }
    }
}
