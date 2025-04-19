using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Messaging.ServiceBus;
using System.Globalization;
using Newtonsoft.Json;
using BarcodeStandard;

namespace fnGeradorBoletos
{
    public class GeradorCodigoBarras
    {

        private readonly ILogger<GeradorCodigoBarras> _logger;
        private readonly string _servicebusConnectionString;
        private readonly string _queueName = "gerador-codigo-barras";

        public GeradorCodigoBarras(ILogger<GeradorCodigoBarras> logger)
        {
            _logger = logger;
            _servicebusConnectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");
        }

        [Function("barcode-generate")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);

                string valor = data?.valor;
                string dataVencimento = data?.dataVencimento;

                string barcodeData;

                //Validação dados


                if (string.IsNullOrEmpty(valor) || string.IsNullOrEmpty(dataVencimento))
                {

                    return new BadRequestObjectResult("Os camos valor e datavencimento são obrigatórios");


                }

                //validar formato da data de vencimento YYYY-MM-DD

                if (!DateTime.TryParseExact(dataVencimento, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime dateObj))
                {
                    return new BadRequestObjectResult("Data de vencimento inválida");
                }

                string dateStr = dateObj.ToString("yyyyMMdd");

                //conversão de valor para decimal até 8 digitos

                if (!decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valorDecimal))
                {
                    return new BadRequestObjectResult("Valor inválido");
                }

                int valoCentavos = (int)(valorDecimal * 10);
                string valorStr = valoCentavos.ToString("D8");

                string bankCode = "008";
                string baseCode = string.Concat(bankCode, valorStr, dateStr);

                //Preenchimento do barCode para ter 44 caracteres
                barcodeData = baseCode.Length < 44 ? baseCode.PadRight(44, '0') : baseCode.Substring(0, 44);
                Barcode barcode = new Barcode();
                var skImage = barcode.Encode(BarcodeStandard.Type.Code128, barcodeData);



                _logger.LogInformation($"Barcode gerado: {barcodeData}");

                using (var encodeData = skImage.Encode(SkiaSharp.SKEncodedImageFormat.Png, 100))
                {
                    var imageBytes = encodeData.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);

                    var resultObject = new
                    {
                        barcode = "1234567890",
                        valorOriginal = "100.00",
                        DataVencimento = DateTime.Now.AddDays(5),
                        ImagemBase64 = base64String
                    };


                    // Send the resultObject to Service Bus Queue
                    await SendFileFallback(resultObject, _servicebusConnectionString, _queueName);
                    return new OkObjectResult(resultObject);

                }
            }


            catch (Exception ex)
            {

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            }


            
        }

        private async Task SendFileFallback(object resultObject, string servicebusConnectionString, string queueName)
        {
            await using var client = new ServiceBusClient(servicebusConnectionString);
            ServiceBusSender sender = client.CreateSender(queueName);
            string messageBody = JsonConvert.SerializeObject(resultObject);
            ServiceBusMessage message = new ServiceBusMessage(messageBody);
            await sender.SendMessageAsync(message);
            await sender.DisposeAsync();
            _logger.LogInformation($"Mensagem enviada para fila: {queueName}");

        }

    }
}
