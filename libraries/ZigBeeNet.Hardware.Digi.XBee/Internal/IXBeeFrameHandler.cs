using System;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.Digi.XBee.Internal
{
    public interface IXBeeFrameHandler
    {
        void AddEventListener(IXBeeEventListener listener);
        void Close();
        IXBeeEvent EventWait(Type eventClass);
        bool IsAlive();
        void RemoveEventListener(IXBeeEventListener listener);
        IXBeeResponse SendRequest(IXBeeCommand command);
        Task<IXBeeResponse> SendRequestAsync(IXBeeCommand command);
        void SetClosing();
        void SetCommandTimeout(int commandTimeOut);
        void SetTransactionTimeout(int transactionTimeOut);
        void Start(IZigBeePort serialPort);
    }
}