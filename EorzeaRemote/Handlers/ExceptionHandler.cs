using EorzeaRemote.Data;
using EorzeaRemote.Exceptions;
using Riptide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EorzeaRemote.Handlers
{
    internal class ExceptionHandler
    {
        private readonly Server Server;

        public ExceptionHandler(Server Server)
        {
            this.Server = Server;
        }

        public void Handle(Exception Exception, ushort SenderId)
        {
            var Resposne = Message.Create(MessageSendMode.Reliable, MessageSendType.GenericError);
            Server.Send(Resposne, SenderId);

            switch(Exception)
            {
                case InvalidInputException: HandleInvalidInput(); break;
                case NotAuthorizedException: HandleNotAuthorized(); break;
                case PlayerNotConnectedException: HandlePlayerNotConnected(); break;
                case PlayerNotFoundException: HandlePlayerNotFound(); break;
            }
        }

        private void HandleInvalidInput()
        {

        }

        private void HandleNotAuthorized()
        {

        }

        private void HandlePlayerNotConnected()
        {

        }

        private void HandlePlayerNotFound()
        {

        }
    }
}
