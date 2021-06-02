using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using CefSharp.WinForms;
using CefSharp;
using Newtonsoft.Json;
using System.Threading;

namespace Stop_Cheating
{
    class OAuthlogin
    {
        protected Task mktask;
        string ssurl = "www.naver.com";
        static string redirectUri;
        static string state;
        static string codeVerifier;
        static string codeChallenge;
        protected ChromiumWebBrowser chrome;
        static string oauthoutput;
        const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";

        MetroFramework.Controls.MetroTabPage temppage;
        MetroFramework.Controls.MetroLabel datalabel;
        

        public void Login_start(MetroFramework.Controls.MetroTabPage temppage, MetroFramework.Controls.MetroLabel datalabel)
        {
            try
            {
                this.temppage = temppage;
                this.datalabel = datalabel;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            
            string[] client_info = { };
            Mainfunc(client_info);
        }

        async Task<int> Mainfunc(string[] args)
        {

            if (args.Length != 2)
            {
                Console.WriteLine("Required command line arguments: client-id client-secret");
                return 1;
            }
            string clientId = args[0];
            string clientSecret = args[1];

            Console.WriteLine("+-----------------------+");
            Console.WriteLine("|  Sign in with Google  |");
            Console.WriteLine("+-----------------------+");
            Console.WriteLine("");
            Console.WriteLine("Press any key to sign in...");
            //Console.ReadKey();


            createauthorization(clientId, clientSecret);
            OAuthlogin p = new OAuthlogin();

            await p.DoOAuthAsync(clientId, clientSecret, redirectUri);
           
            
            try
            {
                temppage.Controls.Remove(chrome);//제거

                datalabel.Text = oauthoutput;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            
            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();

            return 0;
        }

        private void InitializeChromeBrowser()
        {
            CefSettings cefSettings = new CefSettings();
            Cef.Initialize(cefSettings);
            chrome = new ChromiumWebBrowser(ssurl);
            
            chrome.Dock = DockStyle.Fill;
            temppage.Controls.Add(chrome);
            chrome.BringToFront();

         
        }

        public static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        private void createauthorization(string clientId, string clientSecret)
        {
            // Generates state and PKCE values.
            state = GenerateRandomDataBase64url(32);
            codeVerifier = GenerateRandomDataBase64url(32);
            codeChallenge = Base64UrlEncodeNoPadding(Sha256Ascii(codeVerifier));
            const string codeChallengeMethod = "S256";

            // Creates a redirect URI using an available port on the loopback address.
            redirectUri = $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";
            Log("redirect URI입니당: " + redirectUri);

            Console.WriteLine(redirectUri);

            string authorizationRequest = string.Format("{0}?response_type=code&scope=email%20profile&redirect_uri={1}&client_id={2}&state={3}&code_challenge={4}&code_challenge_method={5}",
             AuthorizationEndpoint,
             Uri.EscapeDataString(redirectUri),
             clientId,
             state,
             codeChallenge,
             codeChallengeMethod);


            // Opens request in the browser.
            Console.WriteLine(authorizationRequest);

            ssurl = authorizationRequest;
            try
            {
                InitializeChromeBrowser();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }


        private async Task DoOAuthAsync(string clientId, string clientSecret, string redirectUri)
        {

            // Creates an HttpListener to listen for requests on that redirect URI.
            var http = new HttpListener();
            http.Prefixes.Add(redirectUri);
            Log("Listening..");
            http.Start();

            //Process.Start(authorizationRequest);

            // Waits for the OAuth authorization response.
            var context = await http.GetContextAsync();

            // Brings the Console to Focus.
            BringConsoleToFront();

            // Sends an HTTP response to the browser.
            var response = context.Response;
            string responseString = "<html><head><meta http-equiv='refresh' content='10;url=https://google.com'></head><body> </body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            await responseOutput.WriteAsync(buffer, 0, buffer.Length);
            responseOutput.Close();
            http.Stop();
            Log("HTTP server stopped.");

            // Checks for errors.
            string error = context.Request.QueryString.Get("error");
            if (error is object)
            {
                Log($"OAuth authorization error: {error}.");
                return;
            }
            if (context.Request.QueryString.Get("code") is null
                || context.Request.QueryString.Get("state") is null)
            {
                Log($"Malformed authorization response. {context.Request.QueryString}");
                return;
            }

            // extracts the code
            var code = context.Request.QueryString.Get("code");
            var incomingState = context.Request.QueryString.Get("state");

            // Compares the receieved state to the expected value, to ensure that
            // this app made the request which resulted in authorization.
            if (incomingState != state)
            {
                Log($"Received request with invalid state ({incomingState})");
                return;
            }
            Log("Authorization code: " + code);

            // Starts the code exchange at the Token Endpoint.
            await ExchangeCodeForTokensAsync(code, codeVerifier, redirectUri, clientId, clientSecret);
        }

        async Task ExchangeCodeForTokensAsync(string code, string codeVerifier, string redirectUri, string clientId, string clientSecret)
        {
            Log("Exchanging code for tokens...");

            // builds the  request
            string tokenRequestUri = "https://www.googleapis.com/oauth2/v4/token";
            string tokenRequestBody = string.Format("code={0}&redirect_uri={1}&client_id={2}&code_verifier={3}&client_secret={4}&scope=&grant_type=authorization_code",
                code,
                Uri.EscapeDataString(redirectUri),
                clientId,
                codeVerifier,
                clientSecret
                );

            // sends the request
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(tokenRequestUri);
            tokenRequest.Method = "POST";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            byte[] tokenRequestBodyBytes = Encoding.ASCII.GetBytes(tokenRequestBody);
            tokenRequest.ContentLength = tokenRequestBodyBytes.Length;
            using (Stream requestStream = tokenRequest.GetRequestStream())
            {
                await requestStream.WriteAsync(tokenRequestBodyBytes, 0, tokenRequestBodyBytes.Length);
            }

            try
            {
                // gets the response
                WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
                using (StreamReader reader = new StreamReader(tokenResponse.GetResponseStream()))
                {
                    // reads response body
                    string responseText = await reader.ReadToEndAsync();
                    Console.WriteLine(responseText);

                    // converts to dictionary
                    Dictionary<string, string> tokenEndpointDecoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);

                    string accessToken = tokenEndpointDecoded["access_token"];
                    await RequestUserInfoAsync(accessToken);
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        Log("HTTP: " + response.StatusCode);
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            // reads response body
                            string responseText = await reader.ReadToEndAsync();
                            Log(responseText);
                        }
                    }

                }
            }
        }

        private async Task RequestUserInfoAsync(string accessToken)
        {
            Log("Making API Call to Userinfo...");

            // builds the  request
            string userinfoRequestUri = "https://www.googleapis.com/oauth2/v3/userinfo";


            // sends the request
            HttpWebRequest userinfoRequest = (HttpWebRequest)WebRequest.Create(userinfoRequestUri);
            userinfoRequest.Method = "GET";
            userinfoRequest.Headers.Add(string.Format("Authorization: Bearer {0}", accessToken));
            userinfoRequest.ContentType = "application/x-www-form-urlencoded";
            userinfoRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            // gets the response
            WebResponse userinfoResponse = await userinfoRequest.GetResponseAsync();
            using (StreamReader userinfoResponseReader = new StreamReader(userinfoResponse.GetResponseStream()))
            {
                // reads response body
                string userinfoResponseText = await userinfoResponseReader.ReadToEndAsync();
                Log(userinfoResponseText);

                OAuthjson jparsing;
                jparsing = JsonConvert.DeserializeObject<OAuthjson>(userinfoResponseText);

                oauthoutput = jparsing.sub + " " + jparsing.name + " " + jparsing.email + "+" + jparsing.hd;
            }


        }

        /// <summary>
        /// Appends the given string to the on-screen log, and the debug console.
        /// </summary>
        /// <param name="output">String to be logged</param>
        private void Log(string output)
        {
            Console.WriteLine(output);
        }

        /// <summary>
        /// Returns URI-safe data with a given input length.
        /// </summary>
        /// <param name="length">Input length (nb. output will be longer)</param>
        /// <returns></returns>
        private static string GenerateRandomDataBase64url(uint length)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[length];
            rng.GetBytes(bytes);
            return Base64UrlEncodeNoPadding(bytes);
        }

        /// <summary>
        /// Returns the SHA256 hash of the input string, which is assumed to be ASCII.
        /// </summary>
        private static byte[] Sha256Ascii(string text)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            using (SHA256Managed sha256 = new SHA256Managed())
            {
                return sha256.ComputeHash(bytes);
            }
        }
        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        /// <summary>
        /// Base64url no-padding encodes the given input buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static string Base64UrlEncodeNoPadding(byte[] buffer)
        {
            string base64 = Convert.ToBase64String(buffer);

            // Converts base64 to base64url.
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");
            // Strips padding.
            base64 = base64.Replace("=", "");

            return base64;
        }
        public void BringConsoleToFront()
        {
            SetForegroundWindow(GetConsoleWindow());
        }


    }
}
