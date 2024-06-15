using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstaladorDeBajopresupuesto
{
    class Program
    {
        private static readonly string LinkDeDescarga = "https://vpxcoqufjvzbalnplopi.supabase.co/storage/v1/object/public/MODS/mods2.2.zip?t=2024-06-14T01%3A41%3A35.112Z";
        private static readonly string DescargarMods = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "Roaming", ".minecraft", "mods");
        private static readonly string ExtraerMods = DescargarMods;

        static async Task Main(string[] args)
        {

            bool Continuar = true;

            while (Continuar)
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
                        await Descargar();
                        break;
                    case "2":
                        await Actualizar();
                        break;
                    case "3":
                        EliminarMods();
                        break;
                    case "4":
                        Continuar = false;
                        Console.WriteLine("XD");
                        break;
                    default: Console.WriteLine("No quieres unos nachos tambien?, en la pantalla pone claramente 4 opciones disponibles no sea wey.");
                        break;
                }

                if (Continuar) ;

                Console.WriteLine("Preciona cualquir tecla para regresar al menú");
                Console.ReadKey();
            }
        }
        private static async Task Descargar()
        {
            string rutadearchzip = Path.Combine(DescargarMods, "mods.zip");
            bool exio = await DescargarArchivo(rutadearchzip);
            if (exio)
            {
                ExtraerArchivos(rutadearchzip, ExtraerMods);
                File.Delete(rutadearchzip);
                Console.WriteLine(" Mods Instalados");
            }

        }

        private static async Task Actualizar()
        {
            EliminarMods();
            await Descargar();

        }
        private static void EliminarMods()
        {
            if (Directory.Exists(DescargarMods))
            {
                DirectoryInfo di = new DirectoryInfo(DescargarMods);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                Console.WriteLine("Mods eliminados corrctamente");
            }

            else
            {
                Console.WriteLine("La carpeta no existe");
            }
        }
    
        private static async Task<bool> DescargarArchivo(string rutadeachivo)
        {
            using (WebClient client2 = new WebClient()) 
            {
                client2.DownloadProgressChanged += (s, e) =>
                {
                    Console.Write($"\rDescargando mods... {e.ProgressPercentage}%");
                };

                client2.DownloadFileCompleted += (s, e) =>
                {
                    Console.Write("Descarga Completa");
                };

                try
                {

                    await client2.DownloadFileTaskAsync(new Uri(LinkDeDescarga), rutadeachivo);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error al descargar mods: {e.Message}");
                    return false;
                }
            }

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(5);
                int maxIntentos = 3;
                for (int i = 0; i < maxIntentos; i++)
                {
                    try
                    {
                        Console.WriteLine("Descargando Mods...");
                        HttpResponseMessage response = await client.GetAsync(LinkDeDescarga);
                        response.EnsureSuccessStatusCode();
                        using (FileStream fs = new FileStream(rutadeachivo, FileMode.Create))
                        {
                            await response.Content.CopyToAsync(fs);
                        }

                        Console.WriteLine("Descarga completa");
                        return true;
                    }

                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($"Error al descargar mods: {e.Message}");
                        if (i == maxIntentos - 1)
                        {
                            Console.WriteLine("No se puede completar la descarga despues de varios intentos, podria deberse a tu internet XD");
                            return false;
                        }

                        else
                        {
                            Console.WriteLine("Reintentando...");
                        }
                    }
                }
            }

            return false;
        }

        private static void ExtraerArchivos(string Zipchafa, string Modsxd)
        {
            ZipFile.ExtractToDirectory(Zipchafa, Modsxd);

        }
    }
}