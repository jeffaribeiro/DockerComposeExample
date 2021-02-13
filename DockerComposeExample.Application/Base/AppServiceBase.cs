using System;
using System.Collections.Generic;
using System.Text;
using DockerComposeExample.Domain.Notificacoes;
using DockerComposeExample.Domain.Repository;

namespace DockerComposeExample.Application.Base
{
    public class AppServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly INotificador _notificador;

        public AppServiceBase(IUnitOfWork unitOfWork, INotificador notificador)
        {
            _unitOfWork = unitOfWork;
            _notificador = notificador;
        }

        public bool Commit()
        {
            if (_notificador.TemNotificacao())
                return false;

            _unitOfWork.Commit();
            return true;
        }
    }
}
