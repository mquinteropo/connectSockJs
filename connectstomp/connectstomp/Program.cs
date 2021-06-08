
using Microsoft.Build.Logging;
using Newtonsoft.Json;
using syp.biz.SockJS.NET.Client;
using syp.biz.SockJS.NET.Client.Event;
using syp.biz.SockJS.NET.Common.Interfaces;
using System;
using System.Net;
using WebSocketSharp;

namespace connectstomp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //var config = Configuration.Factory.BuildDefault("http://localhost:9999/echo");
                //config.Logger = new ConsoleLogger();
                //config.DefaultHeaders = new WebHeaderCollection
                //{
                //    {HttpRequestHeader.UserAgent, "Custom User Agent"},
                //    {"X-Extension", "14201"}
                //};

                //// create a default configuration file (default values)
                //var config = Configuration.Factory.BuildDefault("http://localhost:9999/echo");
                //var sockJs = new SockJs(config);

                // SockJS.SetLogger(new ConsoleLogger()); // sets the global logger
                var sockJs = new SockJS("https://192.168.146.239:8443/cti-websocket"); // creates a client and points it to the local node.js server
                sockJs.AddEventListener("open", (sender, e) =>
                {
                    Console.WriteLine("Connection opened");
                    Console.WriteLine(e.ToString());
                });
                // Listen for messages
                sockJs.AddEventListener("message", (sender, e) =>
                {
                    Console.WriteLine("incoming message " + e);
                });
                sockJs.AddEventListener("/user/events", (sender, e) =>
                {
                    Console.WriteLine(e);

                });
                sockJs.AddEventListener("/user/errors", (sender, e) =>
                {

                    Console.WriteLine(e);
                });
                sockJs.AddEventListener("/user/responses", (sender, e) =>
                {
                    Console.WriteLine(e);
                    //var stringifiedArgs = string.Join(",", null);
                    //Console.WriteLine($"Message received: {stringifiedArgs}");
                    //if (e[0] is TransportMessageEvent msg)
                    //{
                    //    var dataString = msg.Data?.ToString();
                    //    Console.WriteLine($"Message received: Data = {dataString}");

                    //    if (dataString == "foo")
                    //    {
                    //        Console.WriteLine("Echo successful -> closing connection");
                    //        sockJs.Close();
                    //    }
                    //}
                });
                sockJs.AddEventListener("close", (sender, e) =>
                {
                    Console.WriteLine("Connection closed");
                });

                //using (var ws = new WebSocket("wss://192.168.146.239:8443/","cti-websocket"))
                //{
                //    ws.OnOpen += (sender,e) =>Console.WriteLine("OnOpen: "+e);
                //    ws.OnMessage += (sender,e) =>Console.WriteLine("OnMessage "+e);
                //    ws.OnError += (sender,e) =>Console.WriteLine("OnError "+e);
                //    ws.OnClose += (sender,e) =>Console.WriteLine("OnClose "+e);
                //    ws.Connect();
                //}
            }
            catch (Exception ex)
            {

            }
            Console.ReadKey();
        }

    }
    

}
