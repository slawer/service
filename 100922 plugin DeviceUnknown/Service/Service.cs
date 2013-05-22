using System;
using System.Threading;

using SoftwareDevelopmentKit.Services.Types;

namespace SoftwareDevelopmentKit.Services
{
    /// <summary>
    /// Реализует службу
    /// </summary>
    public abstract class Service : IService
    {
        // ---- данные класса ----

        private Thread __staThread = null;                  // поток в котором выполняется служба
        private ServiceStatus state = null;                 // определяет состояние службы

        // ---- события класса ----

        public event ServiceMessageEventHandler onError;
        public event ServiceMessageEventHandler onExit;

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="param">Передаваемый параметр для процедуры инизиализации службы</param>
        protected Service()
        {
            state = new ServiceStatus();
        }

        /// <summary>
        /// Основная функция службы
        /// </summary>
        /// <param name="param">передаваемый параметр для службы</param>
        private void __staThreadProcedure(Object param)
        {
            bool initResult = false;
            try
            {
                initResult = Initialize(param);
                if (initResult)
                {
                    Start();        // запускаем службу
                }
                else
                    if (onExit != null)
                    {
                        onExit(this, new ServiceEventArgs("Службе не удалось инициализироваться", EventType.Information));
                    }                
            }
            catch (Exception ex)
            {
                if (onError != null)
                {
                    onError(this, new ServiceEventArgs(ex.Message, EventType.FatalError));
                }
            }
            finally
            {
                __staThread = null;
                if (initResult)
                {
                    if (onExit != null)
                    {
                        onExit(this, new ServiceEventArgs("Служба завершила свою работу", EventType.Information));
                    }
                }
            }
        }

        /// <summary>
        /// Определяет состоние службы
        /// </summary>
        public ServiceState State
        {
            get { return state.State; }
        }

        // ----- методы класса ----

        /// <summary>
        /// Запустить службу на выполнение
        /// </summary>
        /// <param name="param"></param>
        public void ServiceStart(object param)
        {
            switch (State)
            {
                case ServiceState.Default:
                case ServiceState.Aborted:

                    try
                    {
                        if (__staThread == null)
                        {
                            __staThread = new Thread(__staThreadProcedure);
                            __staThread.IsBackground = true;

                            __staThread.Start(param);
                        }
                        else
                            throw new InvalidOperationException("Объект __staThread не равен null");
                    }
                    catch (Exception ex)
                    {
                        if (onError != null)
                        {
                            onError(this, new ServiceEventArgs(ex.Message, EventType.FatalError));
                        }

                        __staThread = null;
                    }
                    break;

                default:

                    break;
            }
        }

        /// <summary>
        /// Приостановить выполнение службы
        /// </summary>
        public void SuspentService()
        {
            switch (State)
            {
                case ServiceState.Running:

                    try
                    {
                        Suspend();
                        state.State = ServiceState.Suspend;
                    }
                    catch (Exception ex)
                    {
                        if (onError != null)
                        {
                            onError(this, new ServiceEventArgs(ex.Message, EventType.FatalError));
                        }
                    }

                    break;

                default:

                    break;
            }
        }

        /// <summary>
        /// Возобновить выполнение службы
        /// </summary>
        public void ResumeService()
        {
            switch (State)
            {
                case ServiceState.Suspend:

                    try
                    {
                        Resume();
                        state.State = ServiceState.Running;
                    }
                    catch (Exception ex)
                    {
                        if (onError != null)
                        {
                            onError(this, new ServiceEventArgs(ex.Message, EventType.FatalError));
                        }
                    }
                    break;

                default:

                    break;
            }
        }

        /// <summary>
        /// Прервать выполнение службы
        /// </summary>
        public void AbortService()
        {
            switch (State)
            {
                case ServiceState.Running:
                case ServiceState.Suspend:

                    try
                    {
                        Abort();
                        state.State = ServiceState.Aborted;
                    }
                    catch (Exception ex)
                    {
                        if (onError != null)
                        {
                            onError(this, new ServiceEventArgs(ex.Message, EventType.FatalError));
                        }
                    }
                    break;

                default:

                    break;
            }
        }     
  
        // ---- процедуры которые необходимо будем реализоваь в класса потомке ----

        /// <summary>
        /// Вызывается перед вызовом процедуры Start.
        /// </summary>
        /// <param name="param">Передаваемый объек для инициализации</param>
        /// <returns>true - все нормально. false - не удалось инициализироваться, необходимо завершить работу службы</returns>
        protected abstract bool Initialize(Object param);
        
        /// <summary>
        /// Вызывается после успешной инициализации
        /// </summary>
        protected abstract void Start();

        /// <summary>
        /// Вызывается если необходимо завершить работу службы
        /// </summary>
        protected abstract void Abort();

        /// <summary>
        /// Вызывается если необходимо приостановить работу службы
        /// </summary>
        protected abstract void Resume();

        /// <summary>
        /// Вызываетя если необходимо возобновить работу службы
        /// </summary>
        protected abstract void Suspend();
    }
}