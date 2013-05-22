using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace Platform
{
    class SocketClient
    {
        // ------ Данные класса --------

        private Socket socket = null;
        private SocketAsyncEventArgs async = null;

        private int _port;
        private string _host;

        byte[] buffer;
        private Int64 m_totalBytesRead = 0;

        // ------ свойства ---------

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

        public bool Connected
        {
            get 
            {
                if (socket != null)
                {
                    return socket.Connected;
                }
                return false;
            }
        }

        public int SendTimeout { get { return 3000; } }

        // -------- События ---------------

        public event EventHandler OnConnect;
        public event EventHandler OnDisconnect;

        public event ReceiveEventHandler OnReceive; 

        // -------- Конструктор -------

        public SocketClient()
        {
            async = null;
            socket = null;

            _port = 56000;
            _host = "127.0.0.1";

            buffer = new byte[10240];
        }

        // -------- подключиться к серверу --------

        public void Connect()
        {
            try
            {
                IPEndPoint ePoint = new IPEndPoint(IPAddress.Parse(_host), _port);

                async = new SocketAsyncEventArgs();
                async.RemoteEndPoint = ePoint;

                async.Completed += new EventHandler<SocketAsyncEventArgs>(async_Completed);

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.ConnectAsync(async);
            }
            catch (Exception ex)
            {
                socket = null;
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        // --------- Завершенно асинхронное событие --------

        void async_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:

                    ProcessReceive(e);
                    break;

                case SocketAsyncOperation.Connect:

                    if (socket.Connected)
                    {
                        e.SetBuffer(buffer, 0, buffer.Length);
                        if (OnConnect != null) OnConnect(this, null);

                        socket.SendTimeout = SendTimeout;
                        socket.ReceiveAsync(e);
                    }
                    else
                        socket.ConnectAsync(e);

                    break;

                default:

                    break;
            }
        }

        // ----------- Вычитывание данных ---------

        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            switch (e.SocketError)
            {
                case SocketError.Success:

                    if (e.BytesTransferred > 0)
                    {
                        Interlocked.Add(ref m_totalBytesRead, e.BytesTransferred);

                        // ------ сообщаем наружу --------

                        if (OnReceive != null)
                        {
                            byte[] data = new byte[e.BytesTransferred];
                            Array.Copy(e.Buffer, e.Offset, data, 0, e.BytesTransferred);

                            OnReceive(this, data);                            
                        }
                        if (socket.Connected) socket.ReceiveAsync(e);
                        else CloseSocket();
                    }
                    else
                    {
                        CloseSocket();
                    }
                    break;

                default:

                    CloseSocket();
                    break;
            }
        }

        // ------------ Закрытие соединения с клиентом -------

        private void CloseSocket()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception) { }
            
            socket.Close();
            socket = null;

            // ----- Сообщаем наружу ----------

            if (OnDisconnect != null) OnDisconnect(this, null);
        }  
      
        // ------ Закрыть --------

        public void Disconnect()
        {
            socket.Disconnect(false);
        }

        // ------ Отправить данные --------

        public void Send(byte[] data)
        {
            try
            {
                socket.Send(data);
            }
            catch (Exception ex)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                }
                catch (Exception) { }
                socket.Close();
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }

    public delegate void ReceiveEventHandler(object sender, byte[] data);
}