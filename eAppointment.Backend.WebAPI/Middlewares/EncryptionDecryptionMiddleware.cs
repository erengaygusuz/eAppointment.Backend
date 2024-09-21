using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace eAppointment.Backend.WebAPI.Middlewares
{
    public class EncryptionDecryptionMiddleware
    {
        private readonly RequestDelegate _next;

        public EncryptionDecryptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            List<string> includedRequestURL = GetIncludedRequestURLList();

            if (includedRequestURL.Contains(httpContext.Request.Path.Value))
            {
                httpContext.Request.Body = DecryptStream(httpContext.Request.Body);

                if (httpContext.Request.QueryString.HasValue)
                {
                    string decryptedString = DecryptString(httpContext.Request.QueryString.Value.Substring(1));

                    httpContext.Request.QueryString = new QueryString($"?{decryptedString}");
                }
            }

            List<string> includedResponseURL = GetIncludedResponseURLList();

            if (includedResponseURL.Contains(httpContext.Request.Path.Value))
            {
                var originalBodyStream = httpContext.Response.Body;

                using (var responseBody = new MemoryStream())
                {
                    httpContext.Response.Body = responseBody;

                    await _next(httpContext);

                    httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

                    var responseBodyContent = await ReadResponseBodyAsync(httpContext.Response);

                    var encryptedText = Encrypt(responseBodyContent);

                    httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

                    var jsonResponse = new { data = encryptedText };

                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.StatusCode = 200;

                    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(jsonResponse));

                    httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }

            else
            {
                await _next(httpContext);
            }
        }

        public string Encrypt(string plainText)
        {
            using (var aes = GetEncryptionAlgorithm())
            {
                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cs))
                    {
                        writer.Write(plainText);
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        private async Task<string> ReadResponseBodyAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            var text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }

        private Stream DecryptStream(Stream cipherStream)
        {
            Aes aes = GetEncryptionAlgorithm();

            FromBase64Transform base64Transform = new FromBase64Transform(FromBase64TransformMode.IgnoreWhiteSpaces);

            CryptoStream base64DecodedStream = new CryptoStream(cipherStream, base64Transform, CryptoStreamMode.Read);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            CryptoStream decryptedStream = new CryptoStream(base64DecodedStream, decryptor, CryptoStreamMode.Read);

            return decryptedStream;
        }

        private string DecryptString(string cipherText)
        {
            Aes aes = GetEncryptionAlgorithm();

            byte[] buffer = Convert.FromBase64String(cipherText);

            MemoryStream memoryStream = new MemoryStream(buffer);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            StreamReader streamReader = new StreamReader(cryptoStream);

            return streamReader.ReadToEnd();
        }

        private Aes GetEncryptionAlgorithm()
        {
            Aes aes = Aes.Create();

            DotNetEnv.Env.Load();

            var secret_key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("EncryptDecryptKey"));

            var initialization_vector = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("EncryptDecryptIV"));

            aes.Key = secret_key;

            aes.IV = initialization_vector;

            return aes;
        }

        private List<string> GetIncludedRequestURLList()
        {
            List<string> includedURL = new List<string>();

            includedURL.Add("/api/users/getall");
            includedURL.Add("/api/roles/getmenuitems");

            return includedURL;
        }

        private List<string> GetIncludedResponseURLList()
        {
            List<string> includedURL = new List<string>();

            includedURL.Add("/api/users/getall");
            includedURL.Add("/api/roles/getmenuitems");

            return includedURL;
        }
    }
}
