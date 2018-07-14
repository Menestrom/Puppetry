﻿using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using static PuppetContracts.Contracts;

namespace PuppetDriver.Editor
{
    internal class UnityEditor : IEditorHandler
    {
        private Dictionary<string, string> _request;
        private Dictionary<string, string> _response;

        public string Session { get; }
        public Socket Socket { get; set; }

        public UnityEditor(Socket socket, string session)
        {
            Session = session;
            Socket = socket;
            _request = new Dictionary<string, string>();
        }

        public EditorResponse SendKeys(string value, string name, string parent)
        {
            _request.Add(Parameters.Method, Methods.SendKeys);
            _request.Add(Parameters.Value, value);
            _request.Add(Parameters.Name, name);
            _request.Add(Parameters.Parent, parent);

            _response = SocketHelper.SendMessage(Socket, _request);
            _request.Clear();
            EditorResponse result;
            if (!_response.ContainsKey(Parameters.Method) && _response[Parameters.Method] != Methods.SendKeys)
                result = new EditorResponse {StatusCode = ErrorCodes.UnexpectedResponse, IsSuccess = false, ErrorMessage = "Unexpected request was received" };
            else
                result = new EditorResponse { StatusCode = ErrorCodes.Success, IsSuccess = true, Result = _response[Parameters.Result] };

            return result;
        }       

        public EditorResponse Click(string name, string parent)
        {
            _request.Add(Parameters.Method, Methods.Click);
            _request.Add(Parameters.Name, name);
            _request.Add(Parameters.Parent, parent);

            _response = SocketHelper.SendMessage(Socket, _request);
            _request.Clear();
            EditorResponse result;
            if (!_response.ContainsKey(Parameters.Method) && _response[Parameters.Method] != Methods.Click)
                result = new EditorResponse { StatusCode = ErrorCodes.UnexpectedResponse, IsSuccess = false, ErrorMessage = "Unexpected request was received" };
            else
                result = new EditorResponse { StatusCode = ErrorCodes.Success, IsSuccess = true, Result = _response[Parameters.Result] };

            return result;
        }

        public EditorResponse Exists(string name, string parent)
        {
            _request.Add(Parameters.Method, Methods.Exist);
            _request.Add(Parameters.Name, name);
            _request.Add(Parameters.Parent, parent);

            _response = SocketHelper.SendMessage(Socket, _request);
            _request.Clear();
            EditorResponse result;
            if (!_response.ContainsKey(Parameters.Method) && _response[Parameters.Method] != Methods.Exist)
                result = new EditorResponse { StatusCode = ErrorCodes.UnexpectedResponse, IsSuccess = false, ErrorMessage = "Unexpected request was received" };
            else
                result = new EditorResponse { StatusCode = ErrorCodes.Success, IsSuccess = true, Result = _response[Parameters.Result] };

            return result;
        }

        public EditorResponse Active(string name, string parent)
        {
            _request.Add(Parameters.Method, Methods.Active);
            _request.Add(Parameters.Name, name);
            _request.Add(Parameters.Parent, parent);

            _response = SocketHelper.SendMessage(Socket, _request);
            _request.Clear();
            EditorResponse result;
            if (!_response.ContainsKey(Parameters.Method) && _response[Parameters.Method] != Methods.Active)
                result = new EditorResponse { StatusCode = ErrorCodes.UnexpectedResponse, IsSuccess = false, ErrorMessage = "Unexpected request was received" };
            else
                result = new EditorResponse { StatusCode = ErrorCodes.Success, IsSuccess = true, Result = _response[Parameters.Result] };

            return result;
        }

        public EditorResponse StartPlayMode()
        {
            _request.Add(Parameters.Method, Methods.StartPlayMode);

            _response = SocketHelper.SendMessage(Socket, _request);
            _request.Clear();
            EditorResponse result;
            if (!_response.ContainsKey(Parameters.Method) && _response[Parameters.Method] != Methods.StartPlayMode)
                result = new EditorResponse { StatusCode = ErrorCodes.UnexpectedResponse, IsSuccess = false, ErrorMessage = "Unexpected request was received" };
            else
                result = new EditorResponse { StatusCode = ErrorCodes.Success, IsSuccess = true, Result = _response[Parameters.Result] };

            for (var i = 0; i < 3; i++)
            {
                lock (Socket)
                {
                    if (SocketHelper.IsSocketConnected(Socket))
                        break;
                }
                Thread.Sleep(500);
            }

            return result;
        }

        public EditorResponse StopPlayMode()
        {
            _request.Add(Parameters.Method, Methods.StopPlayMode);

            _response = SocketHelper.SendMessage(Socket, _request);
            _request.Clear();
            EditorResponse result;
            if (!_response.ContainsKey(Parameters.Method) && _response[Parameters.Method] != Methods.StopPlayMode)
                result = new EditorResponse { StatusCode = ErrorCodes.UnexpectedResponse, IsSuccess = false, ErrorMessage = "Unexpected request was received" };
            else
                result = new EditorResponse { StatusCode = ErrorCodes.Success, IsSuccess = true, Result = _response[Parameters.Result] };

            return result;
        }

        public EditorResponse MakeScreenshot(string fullPath)
        {
            _request.Add(Parameters.Method, Methods.TakeScreenshot);
            _request.Add(Parameters.Value, fullPath);

            _response = SocketHelper.SendMessage(Socket, _request);
            _request.Clear();
            EditorResponse result;
            if (!_response.ContainsKey(Parameters.Method) && _response[Parameters.Method] != Methods.TakeScreenshot)
                result = new EditorResponse { StatusCode = ErrorCodes.UnexpectedResponse, IsSuccess = false, ErrorMessage = "Unexpected request was received" };
            else
                result = new EditorResponse { StatusCode = ErrorCodes.Success, IsSuccess = true, Result = _response[Parameters.Result] };

            return result;
        }
    }
}