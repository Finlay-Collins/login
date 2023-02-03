using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the login server.");

            // Create a HTTP server that listens on port 8080
            const int PORT = 8080;
            string html = "";
            string css = "";
            string prefix = $"http://localhost:{PORT}/";

            Console.WriteLine($"Listening on {prefix}...");
            HttpListener server = new HttpListener();
            server.Prefixes.Add(prefix);
            server.Start();
            while (true) {
                HttpListenerContext context = server.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                switch(request.RawUrl)
                {
                    case "/":
                        html = File.ReadAllText("../../static/index.html");
                        break;
                    
                    default:
                        string path = "../../static" + request.RawUrl;
                        if (File.Exists(path))
                        {
                            html = File.ReadAllText(path);
                        } else
                        {
                            response.StatusCode = 404;
                            html="404";
                            Console.WriteLine("BREAK");
                        }
                        break;
                }
                
                byte[] buffer = Encoding.UTF8.GetBytes(html);
                Console.WriteLine($"Sending {buffer.Length} bytes...");
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
            server.Stop();
        }
    }
}
