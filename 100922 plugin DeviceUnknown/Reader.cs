using System;
using System.Threading;
using System.Collections.Generic;
using SoftwareDevelopmentKit.Services;
using SoftwareDevelopmentKit.Synchronization;

using Platform;

namespace DeviceUnknown
{
    /// <summary>
    /// Реализует чтение наддых с устройства
    /// </summary>
    public class ReaderWriter : Service
    {
        // ---- данные класса ----

        private AutoResetEvent mevent;                  // определяем наличие ответа
        private Binder binder = null;                   // осуществляет взаимодействие с основной программой

        private List<string> datas;                     // храним поступившие пакеты
        private string data = string.Empty;             // необходим для проверок поступивших данных

        private _Flag flag = null;                      // определяет ожидает ли служба ответ или нет

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public ReaderWriter()
            : base()
        {
            datas = new List<string>();
            mevent = new AutoResetEvent(false);

            flag = new _Flag();
        }

        /// <summary>
        /// Инициализируемся
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override bool Initialize(object param)
        {
            if (param is Binder)
            {
                binder = param as Binder;
                if (binder != null)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        private void ReadOperation()
        {
        }
        /// <summary>
        /// Работаем
        /// </summary>
        protected override void Start()
        {
            string sending = string.Empty;
            if (binder.OperationType == OperationType.Read)
            {
                sending = "@JOB#000#3F0722101000$";
            }
            else
            {
                string d_byte = "FF";
                string _data = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8:X2}{9:X2}{10}{11}{12}{13:X2}{14}{15}",
                    d_byte, d_byte, d_byte, d_byte, d_byte, d_byte, d_byte, d_byte, binder.DeviceNumber, 
                    binder.AnswerDeviceNumber, d_byte, d_byte, d_byte, binder.AnswerTimeout, d_byte, d_byte);

                sending = binder.Proto.CreateCommand(Device.D3F, Command.ReadWrite, PageNumber.P1, 0x10, 0x10, _data);
                
                if (binder.Options.Algorithm)
                {
                    // --- разрешить запись в устройство ---

                    Packet pa = new Packet("@JOB#000#3F09012002A00100$", DateTime.Now, null);
                    binder.App.SendPacket(pa);                    
                }
            }

            binder.onPacket += new BinderPacketHandler(binder_onPacket);
            binder.LastOperation = ResultOperation.Default;

            for (int check = 0; check <= binder.Options.CountDataCheck; check++)
            {
                switch (ChekerToReadEprom(sending))
                {
                    case ResultOperation.Succes:

                        datas.Add(binder.Proto.GetData(data));
                        data = CheckDatas(datas);
                        
                        if (data != string.Empty)
                        {
                            datas.Clear();

                            binder.LastOperation = ResultOperation.Succes;
                            binder.ResultString = data;

                            if (binder.OperationType == OperationType.Write)
                            {
                                if (binder.Options.Algorithm)
                                {
                                    // ---- рестарт устройуства ----

                                    Packet pa = new Packet("@JOB#000#3F0705100100$", DateTime.Now, null);
                                    binder.App.SendPacket(pa);                    
                                }
                            }
                            return;
                        }
                        break;                        

                    case ResultOperation.Timeout:

                        return;

                    case ResultOperation.MorePopit:

                        return;
                }                
            }
        }

        /// <summary>
        /// Функция приема поступившего пакета
        /// </summary>
        /// <param name="packet">Поступивший пакет</param>
        private void binder_onPacket(string packet)
        {
            if (flag.Flag)
            {
                flag.Flag = false;

                data = packet;
                mevent.Set();
            }
        }

        /// <summary>
        /// Читаем конфигурацию
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResultOperation ChekerToReadEprom(string packet)
        {

            Packet pack = new Packet(packet, DateTime.Now, null);
            for (int count = 0; count < binder.Options.CountAttemptIo; count++)
            {                
                binder.App.SendPacket(pack);
                flag.Flag = true;                

                if (mevent.WaitOne(binder.Options.DeviceAnswerTimeout))
                {
                    return ResultOperation.Succes;
                }

                Thread.Sleep(binder.Options.TimeoutBeetwenQuestions);
            }
            return ResultOperation.MorePopit;
        }

        /// <summary>
        /// проверка данных
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        private string CheckDatas(List<string> datas)
        {
            string result = string.Empty;
            if (binder.Options.CountDataCheck == 0) result = datas[0];

            if (datas.Count > 1)
            {
                List<string> bytes = new List<string>();
                for (int index = 0; index < 16; index++)
                {
                    bytes.Clear();
                    foreach (string data in datas)
                    {
                        bytes.Add(data.Substring(index * 2, 2));
                    }

                    bool fl = false;
                    for (int i = 0; i < bytes.Count; i++)
                    {
                        if (GetCountEqual(bytes[i], bytes, 2) == 2)
                        {
                            fl = true;
                            result += bytes[i];
                            break;
                        }
                    }
                    if (!fl) return string.Empty;
                }
            }
            return result;
        }

        private int GetCountEqual(string item, List<string> items, int maxCount)
        {
            int count = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (item == items[i])
                {
                    count += 1;
                    if (count == maxCount) return count;
                }
            }
            return count;
        }


        /// <summary>
        /// Приостанавливаемся
        /// </summary>
        protected override void Suspend()
        {
            throw new InvalidOperationException("Данная служба не поддерживает приостановку работы");
        }

        /// <summary>
        /// Возобновляем работу
        /// </summary>
        protected override void Resume()
        {
            throw new InvalidOperationException("Данная служба не поддерживает возобновление работы");
        }

        /// <summary>
        /// Завершаем работу
        /// </summary>
        protected override void Abort()
        {            
        }
    }

    /// <summary>
    /// Определяет результат операции чтения данных
    /// </summary>
    public enum ResultOperation 
    { 
        /// <summary>
        /// Успех
        /// </summary>
        Succes, 

        /// <summary>
        /// Таймаут
        /// </summary>
        Timeout, 

        /// <summary>
        /// Количество попыток исчерпано
        /// </summary>
        MorePopit, 

        /// <summary>
        /// По умолчанию.
        /// </summary>
        Default,

        /// <summary>
        /// Не благоприятное окружение для работы
        /// </summary>
        OneMoreAnswers
    }
}