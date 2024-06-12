using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    private const string LinkDeDescarga = "";
    private static readonly string DescargarMods = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData","Roaming", ".minecraft", "Mods", "Mods.zip");
    private static readonly string ExtraerMods = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "Roaming", ".minecraft", "Mods");

    static async Task Main(string[] args)
    {

        while (true)
        {
            Console.WriteLine("Selecciona una de estas 4 opciones");
            Console.WriteLine("1. instalar Mods");
            Console.WriteLine("2. Actualizar Mods");
            Console.WriteLine("3. Eliminar Mods");
            Console.WriteLine("4. Salir XD");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await DescargarYExtraer();
                    break;
                case "2":
                    await ActualizarYExtraer();
                    break;
                case "3":
                    EliminarArchivos();
                    break;
                case "4":
                    return;
                default: Console.WriteLine("No quieres unos nachos tambien?, en la pantalla pone claramente 4 opciones disponibles no sea wey.");
                    break;
            }
        }
    }
    private static async Task DescargarYExtraer()
    {
        try
        {
            Console.WriteLine("Descargando...");
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(LinkDeDescarga);
                response.EnsureSuccessStatusCode();

                await using (var fs = new FileStream(DescargarMods, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fs);
                }
            }

            Console.WriteLine("Instalando mods...");
            ZipFile.ExtractToDirectory(DescargarMods, ExtraerMods, true);
            Console.WriteLine("Desacarga E Instalación Completadas.");
        }

        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error de descarga: {e.Message}");
        }

        catch (InvalidDataException e)
        {
            Console.WriteLine($"Error de instalación: {e.Message}");
        }

    }

    private static async Task ActualizarYExtraer()
    {
        Console.WriteLine("Preparando Acrualización");
        EliminarArchivos();

        Console.WriteLine("Descargando Actualización...");
        await DescargarYExtraer();
    }

    private static void EliminarArchivos()
    {
        if (Directory.Exists(ExtraerMods))
        {
            foreach (var file in Directory.GetFiles(ExtraerMods))
            { 
                File.Delete(file);
            }

            foreach (var dir in Directory.GetDirectories(ExtraerMods))
            {
                Directory.Delete(dir, true);
            }
            Console.WriteLine("Los mods fueron borrados");
        }

    }
}