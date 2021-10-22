using Newtonsoft.Json;
using Spider.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{
    class Program
    {
        static void Main(string[] args)
        {


            var op = true;
            while (op == true)
            {
                Console.Clear();
                Console.WriteLine(" ♦ Escolha uma das opções: \n \n ► Buscar CEP(digite 1) \n \n ► Buscar CNPJ(digite 2) \n \n ► Digite exit parar sair \n");
                string Opcao1;
                Opcao1 = Console.ReadLine();

                switch (Opcao1)
                {
                    case "1":
                        string CEP = "";

                        Console.WriteLine(" \n ♦ Digite o CEP: \n");

                        CEP = Console.ReadLine();

                        string url = $"http://viacep.com.br/ws/" + CEP + "/json";

                        var request = WebRequest.CreateHttp(url);
                        request.Method = "GET";
                        request.UserAgent = "RequisicaoWebDemo";

                        WebResponse response = request.GetResponse();


                        using (Stream dataStream = response.GetResponseStream())
                        {

                            StreamReader reader = new StreamReader(dataStream);

                            string responseFromServer = reader.ReadToEnd();

                            CEPs cepConvert = JsonConvert.DeserializeObject<CEPs>(responseFromServer);

                            var caminho1 = $@"C:\api\cep\{CEP}-{DateTime.Now.Ticks}.txt";

                            Console.WriteLine("\n ▬▬▬▬▬▬▬▬▬▬▬ ► Resultado ◄ ▬▬▬▬▬▬▬▬▬▬▬ \n \n Rua: " + cepConvert.logradouro + "\n Bairro: " + cepConvert.bairro + "\n Localidade: " + cepConvert.localidade + "\n Estado: " + cepConvert.uf + "\n CEP: " + cepConvert.cep + "\n");
                            Console.WriteLine($@"► Os dados foram salvos como arquivo em {caminho1} ! " + "\n");
                            Console.WriteLine("♦ Pressione qualquer tecla para retornar ao menu \n");
                            using (var writer = new StreamWriter(caminho1))
                            {
                                writer.WriteLine(responseFromServer + Environment.NewLine);
                                writer.Close();
                            }
                        }
                        Console.ReadKey();

                        break;

                    case "2":

                        Console.WriteLine("\n ♦ Digite o CNPJ: \n");


                        string CNPJ = "";
                        CNPJ = Console.ReadLine();
                        string url2 = $"http://www.receitaws.com.br/v1/cnpj/" + CNPJ + "";
                        var requisitaCNPJ = WebRequest.CreateHttp(url2);
                        requisitaCNPJ.Method = "GET";
                        requisitaCNPJ.UserAgent = "RequisicaoWebDemo";

                        WebResponse responseCNPJ = requisitaCNPJ.GetResponse();


                        using (Stream dataStream = responseCNPJ.GetResponseStream())
                        {

                            StreamReader reader = new StreamReader(dataStream);

                            string responseFromServer = reader.ReadToEnd();


                            CNPJs cnpjConvert = JsonConvert.DeserializeObject<CNPJs>(responseFromServer);

                            var caminho2 = $@"C:\api\cnpj\{CNPJ}-{DateTime.Now.Ticks}.txt";

                            Console.WriteLine("\n ▬▬▬▬▬▬▬▬▬▬▬ ► Resultado ◄ ▬▬▬▬▬▬▬▬▬▬▬ \n \n Nome: " + cnpjConvert.nome + "\n E-mail: " + cnpjConvert.email + "\n Telefone: " + cnpjConvert.telefone + "\n Capital Social: " + cnpjConvert.capital_social + "\n Municipio: " + cnpjConvert.municipio + "\n Estado: " + cnpjConvert.uf + "\n");
                            Console.WriteLine($@"► Os dados foram salvos como arquivo em {caminho2} ! " + "\n");
                            Console.WriteLine("♦ Pressione qualquer tecla para retornar ao menu \n");
                            using (var writer = new StreamWriter(caminho2))
                            {
                                writer.WriteLine(responseFromServer + Environment.NewLine);
                                writer.Close();
                            }
                        }
                        Console.ReadKey();

                        break;
                    case "exit":
                        op = false;
                        break;

                    default:
                        break;
                }
            }
        }

    }
}
